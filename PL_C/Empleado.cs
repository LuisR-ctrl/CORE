using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Empleado
    {
        public static ML.Result CargarMasiva()
        {
            ML.Result result = new ML.Result();

            string file = @"C:\Users\digis\Documents\LUIS_ANGEL_ROMAN_TOVAR\LRomanProgramacionNCapasCore\LayOutEmpleado.txt";

            //instancia             //entra al archivo para extraer la información
            StreamReader archivo = new StreamReader(file);

            //trae la linea
            string line;
            bool isFirstLine = true;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();
            while ((line = archivo.ReadLine()) != null) 
            {
                if(isFirstLine)
                {
                    isFirstLine = false;
                    line = archivo.ReadLine();
                }

                Console.WriteLine(line);
                string[] datos = line.Split('|');

                ML.Empleado empleado = new ML.Empleado();

                empleado.NumeroEmpleado = datos[0];
                empleado.RFC = datos[1];
                empleado.Nombre = datos[2];
                empleado.ApellidoPaterno = datos[3];
                empleado.ApellidoMaterno = datos[4];
                empleado.Email = datos[5];
                empleado.Telefono = datos[6];
                empleado.FechaNacimiento = datos[7];
                empleado.NSS = datos[8];
                empleado.FechaIngreso = datos[9];
                empleado.Foto = datos[10];

                empleado.Empresa =  new ML.Empresa();
                empleado.Empresa.IdEmpresa = int.Parse(datos[11]);

                empleado.Poliza = new ML.Poliza();
                empleado.Poliza.IdPoliza = int.Parse(datos[12]);

                result = BL.Empleado.Add(empleado);

                if (!result.Correct) //si el resultado es diferente a correcto
                {
                    resultErrores.Objects.Add(
                        "No se inserto el Numero Empleado " + empleado.NumeroEmpleado +
                        "No se inserto el RFC " + empleado.RFC +
                        "No se inserto el Nombre " + empleado.Nombre +
                        "No se inserto el Apellido Paterno" + empleado.ApellidoPaterno +
                        "No se inserto el Apellido Materno " + empleado.ApellidoMaterno +
                        "No se inserto el Email " + empleado.Email +
                        "No se inserto el Telefono " + empleado.Telefono +
                        "No se inserto la Fecha de Nacimiento " + empleado.FechaNacimiento +
                        "No se inserto el NSS " + empleado.NSS +
                        "No se inserto la Fecha de Ingreso " + empleado.FechaIngreso +
                        "No se inserto la Foto " + empleado.Foto +
                        "No se inserto el IdEmpresa " + empleado.Empresa.IdEmpresa +
                        "No se inserto el IdPoliza " + empleado.Poliza.IdPoliza);


                }


            }
            if (resultErrores.Objects != null)
            {

            }
            TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\LUIS_ANGEL_ROMAN_TOVAR\LRomanProgramacionNCapasCore\Errores.txt");

            foreach (string error in resultErrores.Objects)
            {
                tw.WriteLine(error); //Se le concatenan todos los errores
            }
            tw.Close();

            return result;
        }

        static void Main(string[] args)
        {
            CargarMasiva();
        }
    }
}
