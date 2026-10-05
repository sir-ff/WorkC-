using System;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Представтесь, пожалуйста:");
        string? answer = Console.ReadLine();
        answer = (answer ?? "").Trim();
        string name = string.IsNullOrWhiteSpace(answer) ? "Гость" : answer.Trim();
        Console.WriteLine($"Здравствуйте, {name}!");
        Console.WriteLine(DateTime.Today.ToString("dd.MM.yyyy"));
        int count = answer.Length;
        Console.WriteLine($"Кол-во символов: {count}.");

        BaitConvertor baitConvertor = new BaitConvertor();
        VolumeConvertor volumeConvertor = new VolumeConvertor();
        SpeedTester speedTester = new SpeedTester();
        ByteText byteText = new ByteText();
        RustRaidCalculations rustCalculations = new RustRaidCalculations();

        bool isRunnig = true;

        while (isRunnig)
        {
            Console.WriteLine("                Меню                    ");
            Console.WriteLine("Введите 1 чтобы войти в BaitConvertor.");
            Console.WriteLine("Введите 2 чтобы войти в VolumeConvertor.");
            Console.WriteLine("Введите 3 чтобы войти в SpeedTester.");
            Console.WriteLine("Введите 4 чтобы войти в ByteText.");
            Console.WriteLine("Введите 5 чтобы выйти в RustRaidCalculations.");
            Console.WriteLine("Введите 6 чтобы выйти.");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    baitConvertor.jopa();
                    break;

                case "2":
                    volumeConvertor.Vasya();
                    break;

                case "3":
                    speedTester.Petya();
                    break;

                case "4":
                    byteText.ZH();
                    break;

                case "5":
                    rustCalculations.fortytwo();
                    break;
                case "6":
                    isRunnig = false;
                    Console.WriteLine("спс за ипользование вась! покеда, гвоздь мне в кеды.");
                    break;
                case "67":
                    Console.WriteLine("6-7, 6-7 (Yeah)\r\n\r\n6-7, 6-7, uh-uh\r\n\r\n6-7, 6-7 (Yeah)\r\n\r\n6-7, 6-7, uh-uh\r\n\r\n6-7, 6-7 (Yeah)\r\n\r\n6-7, 6-7, uh-uh\r\n\r\n6-7, 6-7 (Yeah)\r\n\r\n6-7, 6-7");
                    break;
            }











        }
    }
}