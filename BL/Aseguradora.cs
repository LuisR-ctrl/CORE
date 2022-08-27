using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Imagen = obj.Imagen;

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.UserName = obj.UserName;
                            
                            result.Objects.Add(aseguradora);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Aseguradoras";

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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = query.IdAseguradora;
                        aseguradora.Nombre = query.Nombre;
                        aseguradora.FechaCreacion = query.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = query.FechaModificacion.ToString();
                        aseguradora.Imagen = query.Imagen;

                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = query.IdUsuario.Value;
                        aseguradora.Usuario.UserName = query.UserName;

                        result.Object = aseguradora;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar la Aseguradora";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}, '{aseguradora.Imagen}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar la Aseguradora";
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
        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}, '{aseguradora.Imagen}'");

                    if (query >= 1)
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
        public static ML.Result Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradora.IdAseguradora}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
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
    }
}
