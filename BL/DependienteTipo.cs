using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw($"DependienteTipoGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();

                            dependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependienteTipo.Nombre = obj.Nombre;

                            result.Objects.Add(dependienteTipo);
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
    }
}
