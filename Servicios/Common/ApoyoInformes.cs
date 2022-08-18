using System;
using System.Collections.Generic;

namespace ContaWeb.Servicios.Dashboards.Informes
{
    public class ApoyoInformes
    {
        public ApoyoInformes()
        {
        }

       

        /// <summary>
        /// Devuelve una lista de DateTimes con todos los días del año
        /// </summary>
        /// <returns></returns>
        public List<DateTime> ObtieneTodosLosPutosDiasDeUnAño(bool aplicacorte)
        {

            //ConstantesSetup constantes = new ConstantesSetup();
            //int añoActual = constantes.DameAñoFiscal();

            int año = 2019;
           

            DateTime startingDate = new DateTime(año, 1, 1);
            DateTime endingDate;

            //if (aplicacorte) {
            //    var lastDayOfMonth = DateTime.DaysInMonth(año, DateTime.Now.Month);
            //    endingDate = new DateTime(añoActual, DateTime.Now.Month, lastDayOfMonth);
            //}
            //else { 
            //    endingDate= new DateTime(añoActual, 12, 31);
            //}


            //endingDate = new DateTime(2022, 12, 31);
            endingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startingDate; date <= endingDate; date = date.AddDays(1))
            {
                if (date < DateTime.Now) {
                    allDates.Add(date);
                }
            }
            return allDates;
        }

    
        public  string FormateaDateTime2StringCanvas(DateTime fechaCursor)
        {

            string año = fechaCursor.Year.ToString().Substring(2,2);



            string fechaF = "DD/MM/YY";
            fechaF = fechaF.Replace("YY",año);

            int dia = fechaCursor.Day;
            if (dia < 10)
            {
                fechaF = fechaF.Replace("DD", "0" + dia.ToString());
            }
            else
            {
                fechaF = fechaF.Replace("DD", dia.ToString());
            }

            int mes = fechaCursor.Month;
            if (mes < 10)
            {
                fechaF = fechaF.Replace("MM", "0" + mes.ToString());
            }
            else
            {
                fechaF = fechaF.Replace("MM", mes.ToString());
            }

            return fechaF;
        }

        public string FormateaDateTime2StringConsulta(DateTime fechaCursor)
        {
            string fechaF = "AAAAMMDD";

            int ano = fechaCursor.Year;
            fechaF = fechaF.Replace("AAAA",ano.ToString()); 


            int dia = fechaCursor.Day;
            if (dia < 10)
            {
                fechaF = fechaF.Replace("DD", "0" + dia.ToString());
            }
            else
            {
                fechaF = fechaF.Replace("DD", dia.ToString());
            }

            int mes = fechaCursor.Month;
            if (mes < 10)
            {
                fechaF = fechaF.Replace("MM", "0" + mes.ToString());
            }
            else
            {
                fechaF = fechaF.Replace("MM", mes.ToString());
            }

            return fechaF;
        }

        public String EstableceFecha(DateTime fechaCursor) {

            string formato = "new Date(YYYY, MM, DD)";

            formato=formato.Replace("YYYY", fechaCursor.Year.ToString());

            int dia = fechaCursor.Day;
            if (dia < 10)
            {
                formato = formato.Replace("DD", "0" + dia.ToString());
            }
            else
            {
                formato = formato.Replace("DD", dia.ToString());
            }

            //https://canvasjs.com/forums/topic/month-problem/
            //en Javascript los meses empiezan en 0 por lo que hay que restar uno al mes
            int mes = fechaCursor.Month-1;
            if (mes < 10)
            {
                formato = formato.Replace("MM", "0" + mes.ToString());
            }
            else
            {
                formato = formato.Replace("MM", mes.ToString());
            }


            return formato;
        }

     
    }
}
