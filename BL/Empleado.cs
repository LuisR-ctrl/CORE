using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}', '{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}, {empleado.Poliza.IdPoliza}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar al Empleado";
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
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}', '{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}, {empleado.Poliza.IdPoliza} ");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el Empleado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Delete(string NumeroEmpleado)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{NumeroEmpleado}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó al Empleado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleado.Nombre}' ,'{empleado.ApellidoPaterno}', {empleado.Empresa.IdEmpresa}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            empleado = new ML.Empleado();

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
            }
            return result;
        }
        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = query.NumeroEmpleado;
                        empleado.RFC = query.Rfc;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.Telefono;
                        empleado.FechaNacimiento = query.FechaNacimiento?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        empleado.NSS = query.Nss;
                        empleado.FechaIngreso = query.FechaIngreso?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        empleado.Foto = query.Foto;

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = query.IdEmpresa.Value;
                        empleado.Empresa.Nombre = query.EmpresaNombre;

                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = query.IdPoliza.Value;
                        empleado.Poliza.Nombre = query.PolizaNombre;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar al Empleado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }
        public static ML.Result AddUpdate(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAddUpdate '{empleado.NumeroEmpleado}', '{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}, {empleado.Poliza.IdPoliza}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el Empleado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                //Representa una conexión abierta a un origen de datos.
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja 1$]";
                    //Representa una instrucción SQL o un procedimiento almacenado para ejecutar en un origen de datos
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        //Representa un conjunto de comandos de datos y una conexión de base de datos que se utilizan para completar el DataSet y actualizar el origen de datos.
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        //Obtiene o establece una instrucción SQL o un procedimiento almacenado que se usa para seleccionar registros en el origen de datos.
                        da.SelectCommand = cmd;

                        //Representa una tabla de datos en memoria.
                        DataTable tableEmpleado = new DataTable();

                        //Agrega o actualiza filas en el DataSet para que coincidan con las de un ADO Recordsetu Recordobjeto.
                        da.Fill(tableEmpleado);
                        //si tiene información
                        if (tableEmpleado.Rows.Count > 0)
                        {
                            //creamos la lista de objetos
                            result.Objects = new List<object>();
                            //objeto de tipo dar
                            foreach (DataRow row in tableEmpleado.Rows)
                            {
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.NumeroEmpleado = row[0].ToString();
                                empleado.RFC = row[1].ToString();
                                empleado.Nombre = row[2].ToString();
                                empleado.ApellidoPaterno = row[3].ToString();
                                empleado.ApellidoMaterno = row[4].ToString();
                                empleado.Email = row[5].ToString();
                                empleado.Telefono = row[6].ToString();
                                empleado.FechaNacimiento = row[7].ToString();
                                empleado.NSS = row[8].ToString();
                                empleado.FechaIngreso = row[9].ToString();
                                empleado.Foto = row[10].ToString();

                                empleado.Empresa = new ML.Empresa();
                                empleado.Empresa.IdEmpresa = int.Parse(row[11].ToString());

                                empleado.Poliza = new ML.Poliza();
                                empleado.Poliza.IdPoliza = int.Parse(row[12].ToString());

                                result.Objects.Add(empleado);
                            }

                            result.Correct = true;
                        }

                        result.Object = tableEmpleado;

                        if (tableEmpleado.Rows.Count >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();

                int i = 1;
                foreach (ML.Empleado empleado in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (empleado.NumeroEmpleado == "")
                    {
                        error.Mensaje += "Ingresar el numero de empleado  ";
                    }

                    if (empleado.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }

                    if (empleado.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido paterno ";
                    }

                    if (empleado.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido materno ";
                    }

                    if (empleado.Email == "")
                    {
                        error.Mensaje += "Ingresar el email  ";
                    }

                    if (empleado.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono  ";
                    }

                    if (empleado.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la fecha de nacimiento  ";
                    }

                    if (empleado.NSS == "")
                    {
                        error.Mensaje += "Ingresar el NSS  ";
                    }

                    if (empleado.FechaIngreso == "")
                    {
                        error.Mensaje += "Ingresar la fecha de ingreso  ";
                    }

                    if (empleado.Foto == "")
                    {
                        error.Mensaje += "Ingresar la foto  ";
                    }

                    if (empleado.Empresa.IdEmpresa.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el id empresa  ";
                    }

                    if (empleado.Poliza.IdPoliza.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el id poliza  ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);

                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
