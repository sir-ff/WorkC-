using System;

public class BaitConvertor
{
    public void jopa()
    {
        Console.WriteLine("Введите размер в байтах: ");
        string? Bait = Console.ReadLine();

        if (Bait == null) Bait = "";

        Bait = Bait.Replace(" ", "");

        long bytesCount = Convert.ToInt64(Bait);

        if (bytesCount < 0)
        {
            Console.WriteLine("Ошибка: нельзя отрицательное Вась!");
            return;
        }

        double kilobyte1000 = (double)bytesCount / 1000;
        double megabyte1000 = kilobyte1000 / 1000;
        double gigabyte1000 = megabyte1000 / 1000;

        double kilobyte1024 = (double)bytesCount / 1024;
        double megabyte1024 = kilobyte1024 / 1024;
        double gigabyte1024 = megabyte1024 / 1024;

        Console.WriteLine();
        Console.WriteLine("Единица         | Деление на косарь | Деление на косарь24");

        Console.WriteLine("Килобайты   | " + Math.Round(kilobyte1000, 2) + "                 |    " + Math.Round(kilobyte1024, 2));
        Console.WriteLine("Мегабайты   | " + Math.Round(megabyte1000, 2) + "                 |    " + Math.Round(megabyte1024, 2));
        Console.WriteLine("Гигабайты   | " + Math.Round(gigabyte1000, 2) + "                 |    " + Math.Round(gigabyte1024, 2));
    }
}
