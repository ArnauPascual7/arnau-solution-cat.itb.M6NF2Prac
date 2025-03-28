using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;
using NHibernate.Util;

namespace cat.itb.M6NF2Prac
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DisplayMenu();
        }
        public static void DisplayMenu()
        {
            const string Menu = "Menú:" +
                "\n[0] Netejar la base de dades" +
                "\n[1] Exercici 1" +
                "\n[2] Exercici 2" +
                "\n[3] Exercici 3" +
                "\n[4] Exercici 4" +
                "\n[5] Exercici 5" +
                "\n[6] Exercici 6" +
                "\n[7] Exercici 7" +
                "\n[8] Exercici 8" +
                "\n[8] Exercici 9" +
                "\n[8] Exercici 10" +
                "\n[8] Exercici 11" +
                "\n[8] Exercici 12" +
                "\n[8] Exercici 13" +
                "\n[8] Exercici 14" +
                "\n[ex] Sortir" +
                "\n";

            Console.WriteLine(Menu);

            string? option = Console.ReadLine();
            Console.Clear();

            bool exit = false;
            switch(option)
            {
                case "0":
                    new GeneralCRUD().RestoreDb();
                    break;
                case "1":
                    Exercise1();
                    break;
                case "2":
                    Exercise2();
                    break;
                case "3":
                    Exercise3();
                    break;
                case "4":
                    Exercise4();
                    break;
                case "5":
                    Exercise5();
                    break;
                case "6":
                    Exercise6();
                    break;
                case "7":
                    Exercise7();
                    break;
                case "8":
                    Exercise8();
                    break;
                case "9":
                    Exercise9();
                    break;
                case "10":
                    Exercise10();
                    break;
                case "11":
                    Exercise11();
                    break;
                case "12":
                    Exercise12();
                    break;
                case "13":
                    Exercise13();
                    break;
                case "14":
                    Exercise14();
                    break;
                case "ex" or "EX" or "Ex":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opció no vàlida");
                    break;
            }

            if (!exit)
            {
                Console.Write("\nPrem ENTER per tornar al menú");
                Console.ReadLine();
                Console.Clear();

                DisplayMenu();
            }
            else
            {
                Console.WriteLine("Fi del programa");
            }
        }
        public static void Exercise1()
        {
            Console.WriteLine("Exercici 1: Insertar Clients");

            List<Client> clies = new List<Client>()
            {
                new Client() { Code = 2998, Name = "Sun Systems", Credit = 45000 },
                new Client() { Code = 2677, Name = "Roxy Stars", Credit = 45000 },
                new Client() { Code = 2865, Name = "Clen Ferrant", Credit = 45000 },
                new Client() { Code = 2873, Name = "Roast Coast", Credit = 45000 }
            };
            ClientCRUD clientCRUD = new ClientCRUD();

            clientCRUD.InsertADO(clies);
        }
        public static void Exercise2()
        {
            Console.WriteLine("Exercici 2: Eliminar el client Roast Coast");

            ClientCRUD clientCRUD = new ClientCRUD();
            Client? clie = clientCRUD.SelectByNameADO("Roast Coast");
            if (clie != null)
            {
                clientCRUD.DeleteADO(clie);
            }
        }
        public static void Exercise3()
        {
            Console.WriteLine("Exercici 3: Actualitzar el preu dels productes");

            ProductCRUD productCRUD = new ProductCRUD();
            List<int> codes = new List<int>() { 100890, 200376, 200380, 100861 };
            List<float> newPrices = new List<float>() { 59.05f, 25.56f, 33.12f, 17.34f };

            for (int i = 0; i < 4; i++)
            {
                Product? prod = productCRUD.SelectByCodeADO(codes[i]);
                if (prod != null)
                {
                    prod.Price = newPrices[i];
                    productCRUD.UpdateADO(prod);
                }
            }
        }
        public static void Exercise4()
        {
            Console.WriteLine("Exercici 4: Mostrar els proveïdors amb creadit inferior a 6000");

            ProviderCRUD providerCRUD = new ProviderCRUD();
            List<Provider> provs = providerCRUD.SelectCreditLowerThanADO(6000).ToList();

            provs.ForEach(p => Console.WriteLine(
                $"Id: {p.Id}, Nom: {p.Name}, Adreça: {p.Address}, Ciutat: {p.City}, StCodi: {p.StCode}, Codi postal: {p.ZipCode}, Area: {p.Area}," +
                $"Telèfon: {p.Phone}, Producte: {p.Product.Id}, Quantitat: {p.Amount}, Crèdit: {p.Credit}, Observació: {p.Remark}"
            ));
        }
        public static void Exercise5()
        {
            Console.WriteLine("Exercici 5: Insertar Venedors");

            List<Salesperson> spers = new List<Salesperson>()
            {
                new Salesperson() { Surname = "WASHINGTON", Job = "MANAGER", StartDate = DateTime.Parse("1974-12-01"), Salary = 139000, Commission = 62000, Dep = "REPAIR" },
                new Salesperson() { Surname = "FORD", Job = "ASSISTANT", StartDate = DateTime.Parse("1985-03-25"), Salary = 105000, Commission = 25000, Dep = "REPAIR" },
                new Salesperson() { Surname = "FREEMAN", Job = "ASSISTANT", StartDate = DateTime.Parse("1965-09-12"), Salary = 90000, Dep = "REPAIR" },
                new Salesperson() { Surname = "DAMON", Job = "ASSISTANT", StartDate = DateTime.Parse("1995-11-15"), Salary = 90000, Dep = "WOOD" },
            };
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();

            salespersonCRUD.InsertADO(spers);
        }
        public static void Exercise6()
        {
            Console.WriteLine("Exercici 6: Mostrar número de comandes i cost total del client Carter & Sons");

            ClientCRUD clientCRUD = new ClientCRUD();
            Client? clie = clientCRUD.SelectByName("Carter & Sons");

            if (clie != null )
            {
                float cost = 0;
                foreach (var order in clie.Orders)
                {
                    cost += order.Cost;
                }
                Console.WriteLine($"El client amb id {clie.Id} ha realitzat {clie.Orders.Count} i s’ha gastat en total {cost}");
            }
        }
        public static void Exercise7()
        {
            Console.WriteLine("Exercici 7: Mostrar els proveïdors dels productes que gestiona el venedor YOUNG");

            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            Salesperson? sper = salespersonCRUD.SelectBySurname("YOUNG");

            if ( sper != null )
            {
                if (sper.Products.Any())
                {
                    foreach (Product p in sper.Products)
                    {
                        Console.WriteLine($"Producte: {p.Code}, Proveïdor: {p.Provider.Name} {p.Provider.City} {p.Provider.ZipCode} {p.Provider.Phone}");
                    }
                }
                else
                {
                    Console.WriteLine("El Venedor YOUNG no gestiona cap producte");
                }
            }
        }
        public static void Exercise8()
        {
            Console.WriteLine("Exercici 8: Mostrar les comandes amb cost superior a 12000 i quantitat igual a 100");
        }
        public static void Exercise9()
        {

        }
        public static void Exercise10()
        {

        }
        public static void Exercise11()
        {

        }
        public static void Exercise12()
        {

        }
        public static void Exercise13()
        {

        }
        public static void Exercise14()
        {

        }
    }
}
