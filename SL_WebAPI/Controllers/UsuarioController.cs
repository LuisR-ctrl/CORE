using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebAPI.Controllers
{
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("api/usuario/getall")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPost]
        [Route("api/usuario/add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {

            var result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPut]
        [Route("api/usuario/update")]
        public IActionResult Update( [FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        [Route("api/usuario/delete")]
        public IActionResult Delete(int IdDireccion, int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.IdDireccion = IdDireccion;
            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.Delete(usuario.Direccion.IdDireccion.Value, usuario.IdUsuario.Value);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/usuario/getbyid")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            var result = BL.Usuario.GetById(usuario.IdUsuario.Value);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
