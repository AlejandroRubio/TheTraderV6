using TheTrader.Modelo.Scrapping;
using TheTrader.Servicios.Scrapping;

namespace TheTraderV6.UnitTesting
{
    public class UnitTestService
    {

        public void ExecuteUnitTests()
        {
            string ejemplo_fichero_investing_path = @"C:\MyTFS\TheTraderV6\UnitTesting\20230626_html.html";
            string contenidoHTML = System.IO.File.ReadAllText(ejemplo_fichero_investing_path);
            InstrumentPrice valores = ScrappingServicio.ProcesaHTMLInvesting(contenidoHTML);
            Console.WriteLine("FIN DE LA EJECUCIÓN");

        }
    }
}
