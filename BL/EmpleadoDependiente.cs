using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmpleadoDependiente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoDependiente");

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;

                            empleado.Poliza = new ML.Poliza();
                            empleado.Poliza.IdPoliza = obj.IdPoliza.Value;
                            empleado.Poliza.Nombre = obj.PolizaNombre;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al consultar el Empleado";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int IdDependiente, string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetById {IdDependiente}, '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();

                        dependiente.IdDependiente = query.IdDependiente;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = query.IdEmpleado;
                        dependiente.Nombre = query.Nombre;
                        dependiente.ApellidoPaterno = query.ApellidoPaterno;
                        dependiente.ApellidoMaterno = query.ApellidoPaterno;
                        dependiente.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dependiente.EstadoCivil = query.EstadoCivil;
                        dependiente.Genero = query.Genero;
                        dependiente.Telefono = query.Telefono;
                        dependiente.RFC = query.Rfc;
                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo = query.IdDependienteTipo.Value;
                        dependiente.DependienteTipo.Nombre = query.DependienteTipoNombre.ToString();

                        result.Object = dependiente;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar al Dependiente";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } 
        public static ML.Result GetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado '{NumeroEmpleado}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = obj.IdDependiente;

                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = obj?.IdEmpleado;
                            dependiente.Empleado.Nombre = obj?.EmpleadoNombre;
                            dependiente.Empleado.ApellidoPaterno = obj.EmpleadoApellidoPaterno;
                            dependiente.Empleado.ApellidoMaterno = obj.EmpleadoApellidoMaterno;

                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj?.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj?.FechaNacimiento.ToString("dd/MM/yy", CultureInfo.InvariantCulture);
                            dependiente.EstadoCivil = obj?.EstadoCivil;
                            dependiente.Genero = obj?.Genero;
                            dependiente.Telefono = obj?.Telefono;
                            dependiente.RFC = obj?.Rfc;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = obj.DependienteTipoNombre;

                            result.Objects.Add(dependiente);

                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Empleado.NumeroEmpleado}', '{dependiente.Nombre}', '{dependiente.ApellidoPaterno}', '{dependiente.ApellidoMaterno}', '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}', '{dependiente.Genero}', '{dependiente.Telefono}', '{dependiente.RFC}', {dependiente.DependienteTipo.IdDependienteTipo}");

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido registrar al Dependiente";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($" {dependiente.IdDependiente}, '{dependiente.Nombre}', '{dependiente.ApellidoPaterno}', '{dependiente.ApellidoMaterno}', '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}', '{dependiente.Genero}', '{dependiente.Telefono}', '{dependiente.RFC}', {dependiente.DependienteTipo.IdDependienteTipo}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el Dependiente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Delete(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteDelete {IdDependiente}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido eliminar al Dependiente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
