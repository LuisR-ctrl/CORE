using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        public ActionResult GetAll()
        {

            ML.Empleado empleado = new ML.Empleado();

            ML.Result result = BL.EmpleadoDependiente.GetAll();

            empleado.Empleados = result.Objects;
            return View(empleado);
        }

        public ActionResult GetByNumeroEmpleado(string NumeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();

            ML.Result result = BL.EmpleadoDependiente.GetByIdEmpleado(NumeroEmpleado);

            dependiente.Dependientes = result.Objects;

            return View(dependiente);

        }

        public ActionResult Form(int IdDependiente , string NumeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();

            //ML.Result resultEmpleado = BL.EmpleadoDependiente.GetAll();
            ML.Result resultEmpleadoTipo = BL.DependienteTipo.GetAll();

            //dependiente.Empleado = new ML.Empleado();
            dependiente.DependienteTipo = new ML.DependienteTipo();

            dependiente.Empleado = new ML.Empleado();

            dependiente.Empleado.NumeroEmpleado = NumeroEmpleado;

            if (IdDependiente != 0)
            {
                ML.Result result = BL.EmpleadoDependiente.GetById(IdDependiente, NumeroEmpleado);
                if (result.Correct)
                {

                    dependiente = (ML.Dependiente)result.Object;


                    dependiente.DependienteTipo.DependienteTipos = resultEmpleadoTipo.Objects.ToList();

                    return View(dependiente);
                }
                else
                {
                    ViewBag.Message("Error al realizar la consulta" + result.ErrorMessage);
                    return PartialView("Modal");
                }
            }
            else
            {

                //dependiente.Empleado.Empleados = resultEmpleado.Objects.ToList();
                dependiente.DependienteTipo.DependienteTipos = resultEmpleadoTipo.Objects.ToList();
                return View(dependiente);
            }

        }
        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            if(dependiente.IdDependiente != 0)
            {
                result = BL.EmpleadoDependiente.Update(dependiente);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado el Dependiente correctamente";
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
                result = BL.EmpleadoDependiente.Add(dependiente);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado el Dependiete";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha podido registrar al Dependiente";
                    return PartialView("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int IdDependiente)
        {
            ML.Result result = BL.EmpleadoDependiente.Delete(IdDependiente);

            if (result.Correct)
            {
                ViewBag.Message = "Se eliminó el Dependiente correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo eliminar el dependiente" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }
}
