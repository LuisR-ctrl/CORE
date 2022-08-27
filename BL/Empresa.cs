using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar al Usuario";
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
        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa},'{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'");

                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el Usuario";
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
        public static ML.Result Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaDelete {IdEmpresa}");

                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se eliminó al Usuario";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Empresas.FromSqlRaw("EmpresaGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
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
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Telefono = query.Telefono;
                        empresa.Email = query.Email;
                        empresa.DireccionWeb = query.DireccionWeb;
                        empresa.Logo = query.Logo;

                        result.Object = empresa;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar la Empresa";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
