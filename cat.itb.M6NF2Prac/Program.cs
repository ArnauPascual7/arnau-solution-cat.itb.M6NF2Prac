using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;
using NHibernate.Util;
using System.Diagnostics;

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
                "\n[1] Prac Exercici 1" +
                "\n[2] Prac Exercici 2" +
                "\n[3] Prac Exercici 3" +
                "\n[4] Prac Exercici 4" +
                "\n[5] Prac Exercici 5" +
                "\n[6] Prac Exercici 6" +
                "\n[7] Prac Exercici 7" +
                "\n[8] Prac Exercici 8" +
                "\n[9] Prac Exercici 9" +
                "\n[10] Prac Exercici 10" +
                "\n[11] Prac Exercici 11" +
                "\n[12] Prac Exercici 12" +
                "\n[13] Prac Exercici 13" +
                "\n[14] Prac Exercici 14" +
                "\n[e1] Exam Exercici 1" +
                "\n[e2] Exam Exercici 2" +
                "\n[e3] Exam Exercici 3" +
                "\n[e4] Exam Exercici 4" +
                "\n[e5] Exam Exercici 5" +
                "\n[e6] Exam Exercici 6" +
                "\n[e7] Exam Exercici 7" +
                "\n[e8] Exam Exercici 8" +
                "\n[e9] Exam Exercici 9" +
                "\n[e10] Exam Exercici 10" +
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
                case "e1":
                    ExamExercise1();
                    break;
                case "e2":
                    ExamExercise2();
                    break;
                case "e3":
                    ExamExercise3();
                    break;
                case "e4":
                    ExamExercise4();
                    break;
                case "e5":
                    ExamExercise5();
                    break;
                case "e6":
                    ExamExercise6();
                    break;
                case "e7":
                    ExamExercise7();
                    break;
                case "e8":
                    ExamExercise8();
                    break;
                case "e9":
                    ExamExercise9();
                    break;
                case "e10":
                    ExamExercise10();
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
            const string sperSurname = "YOUNG";

            Console.WriteLine($"Exercici 7: Mostrar els proveïdors dels productes que gestiona el venedor {sperSurname}");

            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            Salesperson? sper = salespersonCRUD.SelectBySurname(sperSurname);

            if ( sper != null )
            {
                if (sper.Products.Any())
                {
                    foreach (Product p in sper.Products)
                    {
                        Console.WriteLine($"Producte: {p.Code}, Proveïdor: (Nom:{p.Provider.Name}, Ciutat{p.Provider.City}, Codi postal: {p.Provider.ZipCode}, Telèfon: {p.Provider.Phone})");
                    }
                }
                else
                {
                    Console.WriteLine($"El Venedor {sperSurname} no gestiona cap producte");
                }
            }
        }
        public static void Exercise8()
        {
            Console.WriteLine("Exercici 8: Mostrar les comandes amb cost superior a 12000 i quantitat igual a 100");
            // Totes les dades de cada comanda i la descripció i el preu del producte

            OrderCRUD orderCRUD = new OrderCRUD();
            List<Order> orders = orderCRUD.SelectByCostHigherThan(12000, 100).ToList();

            foreach (Order o in orders )
            {
                Console.WriteLine($"Comanda - Id: {o.Id}, Data: {o.OrderDate}, Quantitat: {o.Amount}, Data d'entrega: {o.DeliveryDate}, Cost: {o.Cost}" +
                    $"\nProducte - Descripció: {o.Product.Description}, Preu: {o.Product.Price}");
            }
        }
        public static void Exercise9()
        {
            Console.WriteLine("Exercici 9: Mostrar el proveïdor amb la quantitat mínima");
            // Nom i quantitat del Proveïdor i descripció i stock del producte.

            ProviderCRUD providerCRUD = new ProviderCRUD();
            Provider? prov = providerCRUD.SelectLowestAmount();

            if (prov != null)
            {
                Console.WriteLine($"Proveïdor: (Nom: {prov.Name}, Quntitat: {prov.Amount}), Producte: (Descripció: {prov.Product.Description}, Stock actual: {prov.Product.CurrentStock})");
            }
        }
        public static void Exercise10()
        {
            Console.WriteLine("Exercici 10: Insertar dos nous Productes i Proveïdors");

            Product prod1 = new Product() { Code = 900001, Description = "Producte 900001", CurrentStock = 10, MinStock = 10, Price = 1.99f, Salesperson = new SalespersonCRUD().SelectById(7) };
            Product prod2 = new Product() { Code = 900002, Description = "Producte 900002", CurrentStock = 10, MinStock = 10, Price = 3.99f, Salesperson = new SalespersonCRUD().SelectById(7) };

            Provider prov1 = new Provider() { Name = "Proveïdor 1", Address = "C4", City = "BARC", StCode = "CB", ZipCode = "12321", Area = 1, Phone = "637-7361", Product = prod1, Amount = 1, Credit = 10, Remark = "None" };
            Provider prov2 = new Provider() { Name = "Proveïdor 2", Address = "MP5", City = "BARC", StCode = "CB", ZipCode = "12321", Area = 2, Phone = "748-8472", Product = prod2, Amount = 2, Credit = 11, Remark = "None" };

            new ProductCRUD().Insert(prod1);
            new ProductCRUD().Insert(prod2);
            new ProviderCRUD().Insert(prov1);
            new ProviderCRUD().Insert(prov2);
        }
        public static void Exercise11()
        {
            Console.WriteLine("Exercici 11: Mostrar tots els clients");

            ClientCRUD clientCRUD = new ClientCRUD();
            List<Client> clies = clientCRUD.SelectAll();

            foreach (Client c in clies)
            {
                Console.WriteLine($"Client - Id: {c.Id}, Codi: {c.Code}, Nom: {c.Name}, Crèdit: {c.Credit}");
            }
        }
        public static void Exercise12()
        {
            Console.WriteLine("Exercici 12: Actualitzar el crèdit dels proveïdors de BELMONT");

            ProviderCRUD providerCRUD = new ProviderCRUD();
            List<Provider> provs = providerCRUD.SelectByCity("BELMONT").ToList();

            foreach (Provider p in provs)
            {
                p.Credit = 25000;
                providerCRUD.Update(p);
            }

            Console.WriteLine("Tots els Proveïdors han estat actualitzats correctament");
        }
        public static void Exercise13()
        {
            Console.WriteLine("Exercici 13: Mostrar NOMÉS descripció i preu dels productes amb preu superior a 100");

            ProductCRUD productCRUD = new ProductCRUD();
            
            foreach (var item in productCRUD.SelectByPriceHigherThan(100))
            {
                Console.WriteLine($"Producte - Descripció: {item[0]}, Preu: {item[1]}");
            }
        }
        public static void Exercise14()
        {
            Console.WriteLine("Exercici 14: Mostrar nom i crèdit dels clients amb crèdit superior a 50000");

            ClientCRUD clientCRUD = new ClientCRUD();
            List<Client> clies = clientCRUD.SelectByCreditHigherThan(50000).ToList();

            foreach(Client c in clies)
            {
                Console.WriteLine($"Client - Nom: {c.Name}, Crèdit: {c.Credit}");
            }
        }
        public static void ExamExercise1()
        {
            ProductCRUD productCRUD = new ProductCRUD();
            List<Product> prods = productCRUD.SelectBySalespersonIdADO(2).ToList();

            foreach (Product p in prods)
            {
                Console.WriteLine($"Producte: (Id: {p.Id}, Codi: {p.Code}, Descripció: {p.Description}, Stock Actual: {p.CurrentStock}" +
                    $", Stock Mínim: {p.MinStock}, Preu: {p.Price}), Venedor: {p.Salesperson.Surname}, Proveïdor: {p.Provider.Name}");
            }
        }
        public static void ExamExercise2()
        {
            Order ord = new Order()
            {
                Product = new ProductCRUD().SelectByDescriptionADO("Hammer"),
                Client = new ClientCRUD().SelectByNameADO("Orion Corp") ?? new Client(),
                OrderDate = DateTime.Now,
                Amount = 100,
                DeliveryDate = DateTime.Now,
                Cost = 500
            };
            new OrderCRUD().InsertADO(ord);
        }
        public static void ExamExercise3()
        {
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            List<Salesperson> spers = salespersonCRUD.SelectByJobADO("MANAGER").ToList();

            foreach(Salesperson sp in spers)
            {
                Console.WriteLine($"Venedor: (Id: {sp.Id}, Cognom: {sp.Surname}, Ofici: {sp.Job}, Data d'inici: {sp.StartDate}, Salari: {sp.Salary}, Comissió: {sp.Commission}, Dep: {sp.Dep})");
                sp.Salary = 444000;
                sp.Commission = 80000;
            }
            spers.ForEach(sp => salespersonCRUD.UpdateADO(sp));
        }
        public static void ExamExercise4()
        {
            ClientCRUD clientCRUD = new ClientCRUD();
            List<int> ids = clientCRUD.SelectProductIdsByName("Carter & Sons");

            if (ids.Count == 0)
            {
                Console.WriteLine("El client no ha fet cap comanda");
            }
            else
            {
                Console.WriteLine("Productes Comprats:");
                ids.ForEach(id => Console.WriteLine($"Producte: {id}"));
            }
        }
        public static void ExamExercise5()
        {
            ProviderCRUD providerCRUD = new ProviderCRUD();
            List<object[]> objects = providerCRUD.SelectByStCode("CA").ToList();

            foreach(object[] obj in objects)
            {
                Console.WriteLine($"Nom: {obj[0]}, Ciutat: {obj[1]}, Crèdit: {obj[2]}, Area: {obj[3]}");
            }
        }
        public static void ExamExercise6()
        {
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            List<Salesperson> spers = salespersonCRUD.SelectByJob("MANAGER").ToList();

            foreach (Salesperson sp in spers)
            {
                Console.WriteLine($"Venedor: (Id: {sp.Id}, Cognom: {sp.Surname}, Ofici: {sp.Job}, Data d'inici: {sp.StartDate}, Salari: {sp.Salary}, Comissió: {sp.Commission}, Dep: {sp.Dep})");
                foreach (Product p in sp.Products)
                {
                    Console.WriteLine($"Producte: (Descripció: {p.Description}, Stock Actual: {p.CurrentStock}, Preu: {p.Price})");
                }
            }
        }
        public static void ExamExercise7()
        {
            Order ord = new Order()
            {
                Product = new ProductCRUD().SelectByDescription("Hammer") ?? new Product(),
                Client = new ClientCRUD().SelectByName("Orion Corp") ?? new Client(),
                OrderDate = DateTime.Now,
                Amount = 100,
                DeliveryDate = DateTime.Now,
                Cost = 1999
            };
            new OrderCRUD().Insert(ord);
        }
        public static void ExamExercise8()
        {
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            List<Salesperson> spers = salespersonCRUD.SelectByJobSalaryLowerThan(150000, "WOOD").ToList();

            foreach(Salesperson sp in spers)
            {
                Console.WriteLine($"Venedor: (Nom: {sp.Surname}, Salary: {sp.Salary}, Ofici: {sp.Job}, Dep: {sp.Dep})");
            }
        }
        public static void ExamExercise9()
        {
            ProviderCRUD providerCRUD = new ProviderCRUD();
            List<Provider> provs = providerCRUD.SelectByStAndCreditBetween("CA", 5000, 8000).ToList();

            foreach(Provider p in provs)
            {
                Console.WriteLine($"Proveïdor: (Nom: {p.Name}, Adreça: {p.Address}, Ciutat: {p.City}, StCode: {p.StCode}, Codi Postal: {p.ZipCode}, Area: {p.Area}, Telèfon: {p.Phone}, Quntitat: {p.Amount}, Credit: {p.Credit}, Observació: {p.Remark})");
            }
        }
        public static void ExamExercise10()
        {
            OrderCRUD orderCRUD = new OrderCRUD();
            Order ord = orderCRUD.SelectLowestCost() ?? new Order();

            Console.WriteLine($"Comanda - Nom del Producte: {ord.Product.Description}, Quantitat: {ord.Amount}, Despesa: {ord.Cost}");
        }
    }
}
