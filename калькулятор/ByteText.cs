using System;

public class ByteText
{

    public void ZH()
    {
        Console.WriteLine("Введите че надо: ");
        string? Text = Console.ReadLine();
        Text = (Text ?? "").Trim();

        int count = Text.Length;
        Console.WriteLine($"Кол-во символов: {count}.");

        int ByteCount = System.Text.Encoding.UTF8.GetByteCount( Text );
        Console.WriteLine($"кол-во байт в UTF-8: {ByteCount}");

        int russianCount = Text.Count(c => (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я') || c == 'ё' || c == 'Ё');
        Console.WriteLine($"Кол-во русских букв: {russianCount}");

        int LatinCount = Text.Count(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
        Console.WriteLine($"кол-во латинских букв: {LatinCount}");

        int number = Text.Count(char.IsDigit);
        Console.WriteLine($"кол-во цифр: {number}");

        int space = Text.Length - Text.Replace(" ","").Length;
        Console.WriteLine($"кол-во пробелов: {space}");

        int PunctuationCount = Text.Count(Char.IsPunctuation);
        Console.WriteLine($"кол-во занков препинания: {PunctuationCount}");

        int symbolCount = Text.Count(Char.IsSymbol);
        Console.WriteLine($"кол-во прочи симов: {symbolCount}");

        int otherSymbolsCount = count - russianCount;

        int expectedByteCount = (russianCount * 2) + (otherSymbolsCount * 1);
        Console.WriteLine($"Ожидаемое кол-во байт: {expectedByteCount}");

        if (expectedByteCount == ByteCount)
        {
            Console.WriteLine("Результат: нифига все верно");
        }
        else
        {
            Console.WriteLine("Результат: нефига не сошлось");
            Console.WriteLine($"Разница: {Math.Abs(ByteCount - expectedByteCount)} байт(а).");
        }
    }
}