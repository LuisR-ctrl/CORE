using Microsoft.AspNetCore.Mvc;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        //Parallel poder usar los datos en appsettings.json
        private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult EmpleadoCargaMasiva()
        {
            ML.Result result = new ML.Result();
            //vista, modulo result a la vista
            return View(result);
        }

        [HttpPost]
        public ActionResult EmpleadoCargaMasiva(ML.Empleado empleado)
        {
            //obtiene la información del input
            IFormFile archivo = Request.Form.Files["FileExcel"];
            //obtiene información de la sesión si no existe la misma, "PathArchivo" nombre de la sesion
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                //si es mayor la longitud del archivo
                if (archivo.Length > 0)
                {
                    //nombre del archivo
                    string fileName = Path.GetFileName(archivo.FileName);
                    //Ruta de la carpeta donde se guardan las copias desde appsettings
                    string folderPath = _configuration["PathFolder:value"];
                    //obtiene extension del arhivo
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    //Desde appsettings se obtiene TipoExcel
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //Empieza a trabajar con copias                            rutaarchivo/nombrearchivo/fechadelmomento/extension
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        //si no existe el archivo en la carpeta
                        if (!System.IO.File.Exists(filePath))
                        {
                            //Crea la copia
                            //Proporciona un Stream para un archivo, que admite operaciones de lectura y escritura sincrónicas y asincrónicas.
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            //convierte el excel en datos que se puedan agregar
                            ML.Result resultEmpleados = BL.Empleado.ConvertirExceltoDataTable(connectionString);

                            if (resultEmpleados.Correct)
                            {
                                //valida datos sin llenar en el excel de la lista
                                ML.Result resultValidacion = BL.Empleado.ValidarExcel(resultEmpleados.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    //asigna valor a la sesión    (nombresesion,rutaarchivo/nombrearchivo/fechadelmomento/extension)
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros" + resultEmpleados.ErrorMessage;
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                    }
                }
            }
            else //obtiene información de la sesión si existe
            {
                //Obtiene la sesion -> rutaarchivo / nombrearchivo / fechadelmomento / extension
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                //conexión
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Empleado.ConvertirExceltoDataTable(connectionString);

                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    //recorremos la lista del método
                    foreach (ML.Empleado empleadoItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Empleado.Add(empleadoItem);

                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó al Empleado: " + empleado.NumeroEmpleado + "Nombre: " + empleadoItem.Nombre + "Apellido Paterno: " + empleadoItem.ApellidoPaterno + "ApellidoMaterno: " + empleadoItem.ApellidoMaterno + "Email: " + empleadoItem.Email + "Telefono: " + empleadoItem.Telefono + "Fecha Nacimiento: "+ empleadoItem.FechaNacimiento + "NSS: " + empleadoItem.NSS + "Fecha Ingreso: " + empleadoItem.FechaIngreso + "Foto: " + empleadoItem.Foto + "Empresa: " + empleadoItem.Empresa.IdEmpresa + "Poliza: " + empleadoItem.Poliza.IdPoliza + "Error: " + resultAdd.ErrorMessage);
                        }

                    }

                    if(resultErrores.Objects.Count > 0)
                    {
                        string folderError = _configuration["PathFolderError:value"];
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, folderError + @"\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }

                            ViewBag.Message = "Los Empleados no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Empleados han sido registrados correctamente";
                    }

                }
            }
            return PartialView("Modal");
        }



    }
}
