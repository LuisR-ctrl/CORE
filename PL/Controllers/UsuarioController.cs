using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {//Parallel poder usar los datos en appsettings.json
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            //usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            //usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            //usuario.UserName = (usuario.UserName == null) ? "" : usuario.UserName;

            //ML.Result result = BL.Usuario.GetAll(usuario);

            //usuario.Usuarios = result.Objects;

            //return View(usuario);


            ML.Result resultUsuario = new ML.Result();
            resultUsuario.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("api/usuario/getall");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Objects.Add(resultItemlist);
                    }
                }
                usuario.Usuarios = resultUsuario.Objects;

                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);

            usuario = new ML.Usuario();

            usuario.Usuarios = result.Objects;

            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRoles = BL.Rol.GetAll();
            ML.Result resultPaises = BL.Pais.GetAll();
            
            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado =  new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (IdUsuario != null)
            {
                //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                ML.Result result = new ML.Result();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.GetAsync("api/usuario/getbyid" + "?" + "IdUsuario=" + IdUsuario);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        result.Correct = true;
                    }

                        if (result.Correct)
                        {
                            usuario = (ML.Usuario)result.Object;
                            usuario.Rol.Roles = resultRoles.Objects.ToList();

                            ML.Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                            ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                            ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);


                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects.ToList();
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects.ToList();
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects.ToList();
                            usuario.Direccion.Colonia.Colonias = resultColonias.Objects.ToList();

                            return View(usuario);
                        }
                        else
                        {
                            ViewBag.Message("Error al realizar la consulta" + result.ErrorMessage);
                            return PartialView("Modal");
                        }
                    }
            }
            else
            {
                usuario.Rol.Roles = resultRoles.Objects.ToList();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects.ToList();
                return View(usuario);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImage"];

            //valido si tengo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();

            if (!ModelState.IsValid)
            {
                ML.Result resultRoles = BL.Rol.GetAll();
                ML.Result resultPaises = BL.Pais.GetAll();

                usuario.Rol.Roles = resultRoles.Objects.ToList();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects.ToList();
                return View(usuario);
            }

            //if (usuario.IdUsuario != 0)
            if (usuario.IdUsuario != null)
            {
                //result = BL.Usuario.Update(usuario);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var putTask = client.PutAsJsonAsync<ML.Usuario>("api/usuario/update", usuario);
                    putTask.Wait();

                    var resultTask = putTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado el Usuario correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar el Usuario";
                        return PartialView("Modal");
                    }

                }

                //if (result.Correct)
                //{
                //    ViewBag.Message = "Se ha actualizado el Usuario correctamente";
                //    return PartialView("Modal");
                //}
                //else
                //{
                //    ViewBag.Message = "Error al actualizar el Usuario" + result.ErrorMessage;
                //    return PartialView("Modal");
                //}
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var postTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/add", usuario);
                    postTask.Wait();

                    var resultTask= postTask.Result;

                    if(resultTask.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha registrado el Usuario correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al registrar al Usuario" + result.ErrorMessage;
                        return PartialView("Modal");
                    }

                }
                //result = BL.Usuario.Add(usuario);

                //if (result.Correct)
                //{
                //    ViewBag.Message = "Se ha registrado el Usuario correctamente";
                //    return PartialView("Modal");
                //}
                //else
                //{
                //    ViewBag.Message = "Error al registrar al Usuario" + result.ErrorMessage;
                //    return PartialView("Modal");
                //}
            }
        }
        [HttpGet]
        public ActionResult UpdateEstatus(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();

                usuario = (ML.Usuario)result.Object;

                usuario.Estatus = (usuario.Estatus) ? false : true;

                ML.Result resultUpdate = BL.Usuario.Update(usuario);

                if (resultUpdate.Correct)
                {
                    ViewBag.Message = "Se ha actualizado el Estatus correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al actualizar el Estatus" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "Error al actualizar el Estatus" + result.ErrorMessage;
                return PartialView("Modal");
            }


        }
        [HttpGet]
        public ActionResult Delete(int IdDireccion, int IdUsuario)
        {

            //ML.Result result = BL.Usuario.Delete(IdDireccion, IdUsuario);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var postTask = client.DeleteAsync("api/usuario/delete" +"?"+ "IdDireccion="+IdDireccion +"&"+ "IdUsuario=" + IdUsuario);
                postTask.Wait();

                var resultTask = postTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se eliminó el Usuario correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo eliminar el Usuario";
                    return PartialView("Modal");
                }

            }

            //if (result.Correct)
            //{
            //    ViewBag.Message = "Se eliminó el Usuario correctamente";
            //    return PartialView("Modal");
            //}
            //else
            //{
            //    ViewBag.Message = "No se pudo eliminar el Usuario" + result.ErrorMessage;
            //    return PartialView("Modal");
            //}
        }

        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
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

    }
}
