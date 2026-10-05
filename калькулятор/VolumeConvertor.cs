using System;

public class VolumeConvertor
{
    public void Vasya()
    {
        Console.WriteLine("Введите объем накопителя в гигабайтах: ");
        string? Volume = Console.ReadLine();

        if (Volume == null) Volume = " ";

        Volume = Volume.Replace(" ", "").Replace('.', ',');

        if (!Double.TryParse(Volume, out double VolumeCount))
        {
            Console.WriteLine("Ошибка Вась, надо число Вась: ");
            return;
        }

        if ( VolumeCount <= 0 )
        {
            Console.WriteLine("Вась объем должен быть больше нуля Вась!");
            return;
        }

        double totalBytes = VolumeCount * 1_000_000_000;
        double windowsGb = totalBytes / (1024 * 1024 * 1024);

        double lossGb = VolumeCount - windowsGb;
        double lossPercent = (lossGb / VolumeCount) * 100;

        long photoSizeBytes = 4 * 1024 * 1024;
        long totalBytesLong = (long)totalBytes;

        long photoCount = totalBytesLong / photoSizeBytes;
        long remainingBytes = totalBytesLong % photoSizeBytes;
        double remainingMb = (double)remainingBytes / (1024 * 1024);

        Console.WriteLine("Матвей красавчик, в пинтерст аниме тёть смотрит а не работает :(");
        Console.WriteLine("РЕЗУЛЬТ РАСЧЕТОВ!!!");
        Console.WriteLine("ГИГИ ЗА ШАГИ!!!!");
        Console.WriteLine($"Объём на коробке: {VolumeCount:F2} ГБ");
        Console.WriteLine($"Windows покажет: {windowsGb:F2} ГБ");
        Console.WriteLine($"Пропало пространства: {lossGb:F2} ГБ ({lossPercent:F2}%)");
        Console.WriteLine($"Поместится фотографий (по 4 МБ): {photoCount} штучек.");
        Console.WriteLine($"Останется свободного места: {remainingMb:F2} МБ");
        // я устал нехосу делоть едрит мадрит

    }
}

