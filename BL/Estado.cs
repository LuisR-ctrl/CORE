using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())

                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdPais}").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = obj.IdPais.Value;

                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
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
