using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Login(string password, string email)
        {

            ML.Result result = BL.Usuario.GetByEmail(email);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Password == password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta, intente nuevamente";
                    return PartialView("Modal"); ;
                }
            }
            else
            {
                ViewBag.Message = "El Email no existe, intente nuevamente";
                return PartialView("Modal");
            }
        }
    }
}
