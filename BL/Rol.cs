using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = obj.IdRol;
                            rol.Nombre = obj.Nombre;

                            result.Objects.Add(rol);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
