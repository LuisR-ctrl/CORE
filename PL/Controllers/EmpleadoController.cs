using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            empleado.Empresa = new ML.Empresa();

            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == 0) ? 0 : empleado.Empresa.IdEmpresa;

            ML.Result result = BL.Empleado.GetAll(empleado);

            ML.Result resultEmpresas = BL.Empresa.GetAll();
            
            empleado.Empresa.Empresas = resultEmpresas.Objects.ToList();

            empleado.Empleados = result.Objects;

            return View(empleado);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            //empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            //empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            //empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == 0) ? 0 : empleado.Empresa.IdEmpresa;


            ML.Result result = BL.Empleado.GetAll(empleado);
            ML.Result resultEmpresas = BL.Empresa.GetAll();

            empleado.Empresa.Empresas = resultEmpresas.Objects.ToList();

            empleado.Empleados = result.Objects;

            return View(empleado);
        }
        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result resultEmpresas = BL.Empresa.GetAll();
            ML.Result resultPolizas = BL.Poliza.GetAll();

            empleado.Empresa = new ML.Empresa();
            empleado.Poliza = new ML.Poliza();

            if (NumeroEmpleado != null)
            {
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Action = "Update";
                    empleado.Empresa.Empresas = resultEmpresas.Objects.ToList();
                    empleado.Poliza.Polizas = resultPolizas.Objects.ToList();
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message("Error al realizar la consulta" + result.ErrorMessage);
                    return PartialView("Modal");
                }
            }
            else
            {
                empleado.Action = "Add";
                empleado.Empresa.Empresas = resultEmpresas.Objects.ToList();
                empleado.Poliza.Polizas = resultPolizas.Objects.ToList();
                return View(empleado);
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImage"];

            //valido si tengo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();

            //result = BL.Empleado.AddUpdate(empleado);

            //if (result.Correct)
            //{
            //    ViewBag.Message = "Se ha realizado la operación correctamente";
            //    return PartialView("Modal");
            //}
            //else
            //{
            //    ViewBag.Message = "Error al realizar la operación" + result.ErrorMessage;
            //    return PartialView("Modal");
            //}
            if (!ModelState.IsValid)
            {
                ML.Result resultEmpresas = BL.Empresa.GetAll();
                ML.Result resultPolizas = BL.Poliza.GetAll();

                empleado.Empresa.Empresas = resultEmpresas.Objects.ToList();
                empleado.Poliza.Polizas = resultPolizas.Objects.ToList();
                empleado.NumeroEmpleado = null;
                empleado.Action = "Add";
                return View(empleado);
            }
            if (empleado.Action == "Update")
            {
                result = BL.Empleado.Update(empleado);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado el Empleado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al actualizar al Empleado" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Empleado.Add(empleado);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado al Empleado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al registrar al Empleado" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(string NumeroEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(NumeroEmpleado);

            if (result.Correct)
            {
                ViewBag.Message = "Se eliminó la Empresa correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo eliminar la Empresa correctamente" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            //Lee la imagen
            using var fileStream = imagen.OpenReadStream();

            //convierte a una colección de bytes
            //declaro mi objeto-> arreglo de bytes
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }
        [HttpPost]
        public ActionResult CargaMasiva()
        {
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();
            try
            {
                IFormFile archivo = Request.Form.Files["Archivo"];
                using (StreamReader sr = new StreamReader(archivo.OpenReadStream()))
                {
                    string line;
                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] datos = line.Split('|');

                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = datos[0];
                        empleado.RFC = datos[1];
                        empleado.Nombre = datos[2];
                        empleado.ApellidoPaterno = datos[3];
                        empleado.ApellidoMaterno = datos[4];
                        empleado.Email = datos[5];
                        empleado.Telefono = datos[6];
                        empleado.FechaNacimiento = datos[7];
                        empleado.NSS = datos[8];
                        empleado.FechaIngreso = datos[9];
                        empleado.Foto = datos[10];

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = int.Parse(datos[11]);

                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = int.Parse(datos[12]);

                        ML.Result result = BL.Empleado.Add(empleado);

                        if (!result.Correct) //si el resultado es diferente a correcto
                        {
                            resultErrores.Objects.Add(
                                "No se inserto el Numero Empleado " + empleado.NumeroEmpleado +
                                "No se inserto el RFC " + empleado.RFC +
                                "No se inserto el Nombre " + empleado.Nombre +
                                "No se inserto el Apellido Paterno" + empleado.ApellidoPaterno +
                                "No se inserto el Apellido Materno " + empleado.ApellidoMaterno +
                                "No se inserto el Email " + empleado.Email +
                                "No se inserto el Telefono " + empleado.Telefono +
                                "No se inserto la Fecha de Nacimiento " + empleado.FechaNacimiento +
                                "No se inserto el NSS " + empleado.NSS +
                                "No se inserto la Fecha de Ingreso " + empleado.FechaIngreso +
                                "No se inserto la Foto " + empleado.Foto +
                                "No se inserto el IdEmpresa " + empleado.Empresa.IdEmpresa +
                                "No se inserto el IdPoliza " + empleado.Poliza.IdPoliza);


                        } //Se le asigna agrega la lista de errores
                        if (resultErrores.Objects.Count == 0)
                        {
                            ViewBag.Message = $"Se ha insertado a la Base de Datos, la información del archivo '{archivo.FileName}'";
                        }
                        else
                        {
                            TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\LUIS_ANGEL_ROMAN_TOVAR\LRomanProgramacionNCapasCore\Errores.txt");
                            foreach (string error in resultErrores.Objects)
                            {
                                tw.WriteLine(error); //Se le concatenan todos los errores
                            }
                            tw.Close();
                        }

                    }

                    

                    

                    
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = $"No se ha insertado a la Base de Datos, la información del archivo"+e.Message;
            }
            
            return PartialView("Modal");
        }
    }
}
