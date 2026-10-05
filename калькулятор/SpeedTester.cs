using System;

public class SpeedTester
{
    public void Petya()
    {
        Console.WriteLine("Введите размер файла в гигабайтах: ");
        string? Gigabyte = Console.ReadLine();
        Console.WriteLine("Введите скорось тарифа в мегабитах: ");
        string? Megabyte = Console.ReadLine();

        double fileSizeGB = Convert.ToDouble(Gigabyte, System.Globalization.CultureInfo.InvariantCulture);
        double speedMbps = Convert.ToDouble(Megabyte, System.Globalization.CultureInfo.InvariantCulture);

        double speedMBps = speedMbps / 8.0;
        double fileSizeMB = fileSizeGB * 1000.0;

        double totalSecondsDouble = fileSizeMB / speedMBps;
        int totalSeconds = (int)Math.Ceiling(totalSecondsDouble);

        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        Console.WriteLine($"\nСкорость в мегабайтах в секунду: {speedMBps:F2} МБ/с");
        Console.WriteLine($"Время скачивания: {hours} ч. {minutes} мин. {seconds} сек.");
    }
}