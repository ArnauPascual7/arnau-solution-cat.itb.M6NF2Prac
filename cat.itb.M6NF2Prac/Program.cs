using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;

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
            List<Client> clies = new List<Client>()
            {
                new Client() { Code = 2998, Name = "Sun Systems", Credit = 45000 },
                new Client() { Code = 2677, Name = "Roxy Stars", Credit = 45000 },
                new Client() { Code = 2865, Name = "Clen Ferrant", Credit = 45000 },
                new Client() { Code = 2873, Name = "Roast Coast", Credit = 45000 }
            };
            ClientCRUD clientCRUD = new ClientCRUD();

            clies.ForEach(x => clientCRUD.InsertADO(x));
        }
        public static void Exercise2()
        {

        }
        public static void Exercise3()
        {

        }
        public static void Exercise4()
        {

        }
        public static void Exercise5()
        {

        }
        public static void Exercise6()
        {

        }
        public static void Exercise7()
        {

        }
        public static void Exercise8()
        {

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
