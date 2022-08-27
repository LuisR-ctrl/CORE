using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        public ActionResult GetAll()
        {
            
            ML.Result result = BL.Aseguradora.GetAll();

            ML.Aseguradora aseguradora = new ML.Aseguradora();

            aseguradora.Aseguradoras = result.Objects;

            return View(aseguradora);
        }
        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            //le pasamos el objeto a usuario para que nos traiga a los usuarios
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);

            aseguradora.Usuario = new ML.Usuario();

            if (IdAseguradora != null)
            {
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects.ToList();
                    return View(aseguradora);
                }
                else
                {
                    ViewBag.Message("Error al realizar la consulta" + result.ErrorMessage);
                    return PartialView("Modal");
                }
            }
            else
            {
                aseguradora.Usuario.Usuarios = resultUsuario.Objects.ToList();
                return View(aseguradora);
            }

            

        }
        [HttpPost]

        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImage"];

            //valido si tengo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                aseguradora.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            ML.Result result = new ML.Result();

            if (aseguradora.IdAseguradora != 0)
            {
                result = BL.Aseguradora.Update(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado la Aseguradora correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al actualizar" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Aseguradora.Add(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado la Aseguradora correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al registrar" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(aseguradora);

            if (result.Correct)
            {
                ViewBag.Message = "Se eliminó la Aseguradora correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo eliminar la aseguradora" + result.ErrorMessage;
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
    }
}
