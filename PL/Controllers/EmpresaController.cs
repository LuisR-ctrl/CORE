using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();

            ML.Empresa empresa = new ML.Empresa();

            empresa.Empresas = result.Objects;

            return View(empresa);
        }
        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa != null)
            {
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);
                if(result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;

                    return View(empresa);
                }
                else
                {
                    ViewBag.Message("Error al realizar la consulta" + result.ErrorMessage);
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(empresa);
            }       
        }
        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImage"];

            //valido si tengo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();

            if (empresa.IdEmpresa != 0)
            {
                result = BL.Empresa.Update(empresa);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado el Usuario correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al actualizar el Usuario" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Empresa.Add(empresa);

                if (result.Correct)
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

        }
        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(IdEmpresa);

            if(result.Correct)
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
    }
}
