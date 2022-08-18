using TheTrader.Modelo;


namespace TheTrader.Controles.InvestingIntegracion
{
    class ConstantesParseoInvesting
    {

        public static string cabecera1 = "<span class=\"arial_26 inlineblock pid-XXX-last\" id=\"last_last\" dir=\"ltr\">";

        public static string cabecera2 = "<span class=\"arial_20   pid-XXX-pc redFont\" dir=\"ltr\">";
        public static string cabecera2_1 = "<span class=\"arial_20 redFont   pid-XXX-pc\" dir=\"ltr\">";
        public static string cabecera2_2 = "<span class=\"arial_20   pid-XXX-pc greenFont\" dir=\"ltr\">";
        public static string cabecera2_3 = "<span class=\"arial_20 greenFont   pid-XXX-pc\" dir=\"ltr\">";

        public static string cabecera3 = "<span class=\"arial_20  pid-XXX-pcp parentheses redFont\" dir=\"ltr\" >";
        public static string cabecera3_1 = "<span class=\"arial_20 redFont  pid-XXX-pcp parentheses\" dir=\"ltr\">";
        public static string cabecera3_2 = "<span class=\"arial_20  pid-XXX-pcp parentheses greenFont\" dir=\"ltr\" >";
        public static string cabecera3_3 = "<span class=\"arial_20 greenFont  pid-XXX-pcp parentheses\" dir=\"ltr\">";

        public static string cabecera_ID_inicio = "<link rel=\"alternate\" href=\"android-app://com.fusionmedia.investing/app/investing.com/quotes/";
        //public static string cabecera_ID_fin = "<meta name=\"google-site-verification\" content";
        public static string cabecera_ID_fin = "<meta property=\"al:android:url\" content=\"android-app://com.fusionmedia.investing/app/www.investing.com";

        public static string DameCabecera(AccionInvesting accion, int numeroCabecera)
        {

            if (numeroCabecera == 1)
            {
                return cabecera1.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 2)
            {
                return cabecera2.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 21)
            {
                return cabecera2_1.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 22)
            {
                return cabecera2_2.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 23)
            {
                return cabecera2_3.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 3)
            {
                return cabecera3.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 31)
            {
                return cabecera3_1.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 32)
            {
                return cabecera3_2.Replace("XXX", accion.IdAccionInvesting);
            }
            if (numeroCabecera == 33)
            {
                return cabecera3_3.Replace("XXX", accion.IdAccionInvesting);
            }

            return "ERROR";

        }
    }
}
