using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {
        public static ML.Result GetAll(){

            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Polizas.FromSqlRaw("PolizaGetAll").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Poliza poliza = new ML.Poliza();

                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;

                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = obj.IdSubPoliza.Value;

                            poliza.NumeroPoliza = obj.NumeroPoliza;
                            poliza.FechaCreacion = obj.FechaCreacion?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            poliza.FechaModificacion = obj.FechaModificacion?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = obj.IdUsuario.Value;

                            result.Objects.Add(poliza);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al consultar la Póliza";
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
