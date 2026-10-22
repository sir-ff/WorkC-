using System;
using System.Security.Cryptography;
using System.Text;

PasswordUtility util = new PasswordUtility();

bool running = true;
while (running)
{
    Console.ResetColor();
    Console.WriteLine("\n=== ЖОСКО ПАРОЛЬЧИКИ ВАСЬ ===");
    Console.WriteLine("1. Проверь пароль Вась");
    Console.WriteLine("2. Сгенерируй случайный пароль Вась");
    Console.WriteLine("3. Сгенерируй парольную фразу Вась");
    Console.WriteLine("4. Выход Вась");
    Console.WriteLine();
    Console.Write("Выбери действие Вась 1-4: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            string password = util.ReadPasswordHidden("Введи пароль для проверки Вась: ");
            if (!string.IsNullOrEmpty(password))
            {
                util.ProcessPasswordCheck(password);
            }
            break;

        case "2":
            Console.Write("Введи длину пароля Вась от 8 до 64: ");
            if (!int.TryParse(Console.ReadLine(), out int length) || length < 8 || length > 64)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ля Вась длина должна быть числом Вась от 8 до 64!");
            }
            else
            {
                string generated = util.GeneratePassword(length);
                Console.WriteLine($"\nЭто Вась сгенерированный пароль: {generated}");
                util.ProcessPasswordCheck(generated);
            }
            break;

        case "3":
            Console.Write("Введи количество слов во фразе Вась: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ля Вась введи корректное число слов!");
            }
            else
            {
                string phrase = util.GeneratePassphrase(count);
                Console.WriteLine($"\nСгенерированная фраза: {phrase}");

                double entropy = count * Math.Log2(util.GetDictionarySize());
                Console.WriteLine($"Вась это стойкость фразы: {entropy:F1} бит");
                util.PrintScoreEvaluation(entropy);
            }
            break;

        case "4":
            Console.WriteLine("Программа завершена Вась!");
            running = false;
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ля Вась Неверный ввод! Пробуй снова.");
            break;
    }
}

class PasswordUtility
{
    public void ProcessPasswordCheck(string password)
    {
        Console.WriteLine($"Длина пароля Вась: {password.Length} символов");

        if (IsInPopularList(password))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Вась ну ты лопух? Пароль находится в списке самых популярных! Он подберётся на изи.");
            return;
        }

        if (HasSimplePattern(password))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Вась так не делается. Пароль содержит простой шаблон!");
        }

        int alphabetSize = GetAlphabetSize(password);
        Console.WriteLine($"Вась это размер используемого алфавита: {alphabetSize}");

        double bits = GetPasswordEntropy(password);
        Console.Write("Вась это оценка стойкости: ");
        PrintScoreEvaluation(bits);

        PrintBruteForceTime(bits);
    }

    public int GetAlphabetSize(string password)
    {
        bool hasLower = false;
        bool hasUpper = false;
        bool hasDigits = false;
        bool hasSpecial = false;
        bool hasRussianLower = false;
        bool hasRussianUpper = false;

        foreach (char c in password)
        {
            if (c >= 'a' && c <= 'z') hasLower = true;
            else if (c >= 'A' && c <= 'Z') hasUpper = true;
            else if (c >= '0' && c <= '9') hasDigits = true;
            else if (c >= ' ' && c <= '~') hasSpecial = true;
            else if (c >= 'а' && c <= 'я' || c == 'ё') hasRussianLower = true;
            else if (c >= 'А' && c <= 'Я' || c == 'Ё') hasRussianUpper = true;
        }

        int size = 0;
        if (hasLower) size += 26;
        if (hasUpper) size += 26;
        if (hasDigits) size += 10;
        if (hasSpecial) size += 33;
        if (hasRussianLower) size += 33;
        if (hasRussianUpper) size += 33;

        return size == 0 ? 10 : size;
    }

    public double GetPasswordEntropy(string password)
    {
        int N = GetAlphabetSize(password);
        int L = password.Length;
        double entropy = L * Math.Log2(N);

        if (HasSimplePattern(password))
        {
            entropy -= 15;
            if (entropy < 0) entropy = 0;
        }

        return entropy;
    }

    public void PrintScoreEvaluation(double bits)
    {
        Console.Write($"{bits:F1} бит -> ");
        if (bits < 28)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Очень слабо Вась Тебя могнут");
        }
        else if (bits < 36)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Слабый Вась могнут через время");
        }
        else if (bits < 60)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Средний Вась могут со временем");
        }
        else if (bits < 128)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Сильный Вась почти несмогут могнуть");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Очень сильный Вась ты тру Адам");
        }
        Console.ResetColor();
    }

    public void PrintBruteForceTime(double bits)
    {
        double totalVariants = Math.Pow(2, bits);
        double speedPerSecond = 10_000_000_000;
        double seconds = (totalVariants / 2) / speedPerSecond;

        Console.WriteLine($"Время подбора Вась при 10 млрд/сек: {FormatTime(seconds)}");

        double slowSpeed = speedPerSecond / 1_000_000;
        double slowSeconds = (totalVariants / 2) / slowSpeed;
        Console.WriteLine($"Время подбора при медленном хешировании Вась: {FormatTime(slowSeconds)}");
    }

    private string FormatTime(double seconds)
    {
        const double AgeOfUniverse = 13.8e9 * 365.25 * 24 * 3600;
        if (seconds > AgeOfUniverse) return "Нифига Вась этодольше возраста Вселенной Вась";

        double minutes = seconds / 60;
        if (seconds < 60) return $"{seconds:F2} сек.";
        if (minutes < 60) return $"{minutes:F2} мин.";

        double hours = minutes / 60;
        if (hours < 24) return $"{hours:F2} час.";

        double days = hours / 24;
        if (days < 365) return $"{days:F1} дн.";

        double years = days / 365.25;
        if (years < 1000) return $"{years:F1} лет";
        if (years < 1e6) return $"{years / 1000:F1} тыс. лет";
        if (years < 1e9) return $"{years / 1e6:F1} млн. лет";

        return $"{years / 1e9:F1} млрд. лет";
    }

    public bool IsInPopularList(string password)
    {
        string[] popularPasswords = {
            "123456", "password", "123456789", "qwerty", "12345",
            "12345678", "111111", "123123", "1234567", "password123",
            "iloveyou", "princess", "admin", "welcome", "letmein",
            "secret", "trustnoone", "monkey", "charlie", "football",
            "67676767","qwerty123","jopa123","Вась425267","Серега хакер",
        };

        foreach (string p in popularPasswords)
        {
            if (p.Equals(password, StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }

    public bool HasSimplePattern(string password)
    {
        if (password.Length < 2) return false;

        bool allSame = true;
        for (int i = 1; i < password.Length; i++)
        {
            if (password[i] != password[0]) { allSame = false; break; }
        }
        if (allSame) return true;

        string lower = password.ToLower();
        for (int i = 0; i < lower.Length - 2; i++)
        {
            if (lower[i + 1] == lower[i] + 1 && lower[i + 2] == lower[i] + 2) return true;
        }

        return false;
    }

    public string GeneratePassword(int length)
    {
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string digits = "0123456789";
        string specials = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

        StringBuilder sb = new StringBuilder();
        sb.Append(lowers[RandomNumberGenerator.GetInt32(lowers.Length)]);
        sb.Append(uppers[RandomNumberGenerator.GetInt32(uppers.Length)]);
        sb.Append(digits[RandomNumberGenerator.GetInt32(digits.Length)]);
        sb.Append(specials[RandomNumberGenerator.GetInt32(specials.Length)]);

        string allChars = lowers + uppers + digits + specials;
        while (sb.Length < length)
        {
            int index = RandomNumberGenerator.GetInt32(allChars.Length);
            sb.Append(allChars[index]);
        }

        char[] passwordArray = sb.ToString().ToCharArray();
        for (int i = passwordArray.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (passwordArray[i], passwordArray[j]) = (passwordArray[j], passwordArray[i]);
        }
        return new string(passwordArray);
    }

    public int GetDictionarySize() => 100;
    public string GeneratePassphrase(int wordCount)
    {
        string[] dictionary = {
            "космос", "звезда", "планета", "галактика", "орбита", "спутник", "ракета", "комета", "астероид", "метеор",
            "телескоп", "вакуум", "луна", "солнце", "марс", "юпитер", "сатурн", "венера", "меркурий", "нептун",
            "плутон", "уран", "земля", "чернота", "дыра", "туманность", "пульсар", "квазар", "вспышка", "затмение",
            "гравитация", "свет", "скорость", "время", "атом", "квант", "физика", "химия", "вектор", "матрица",
            "лазер", "энергия", "плазма", "магнит", "фотон", "электрон", "протон", "нейтрон", "ядро", "орбиталь",
            "молекула", "элемент", "система", "модуль", "станция", "корабль", "шаттл", "скафандр", "шлюз", "отсек",
            "экипаж", "пилот", "запуск", "старт", "полет", "посадка", "стыковка", "связь", "сигнал", "антенна",
            "радар", "лаборатория", "модель", "анализ", "данные", "индекс", "код", "программа", "сервер", "база",
            "поток", "экран", "пульт", "кнопка", "датчик", "камера", "зонд", "ровер", "луноход", "марсоход",
            "робот", "дроид", "киборг", "интеллект", "логика", "разум", "мудрость", "память", "проект", "вечность"
        };
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < wordCount; i++)
        {
            int index = RandomNumberGenerator.GetInt32(dictionary.Length);
            sb.Append(dictionary[index]);
            if (i < wordCount - 1) sb.Append('-');
        }
        return sb.ToString();
    }

    public string ReadPasswordHidden(string prompt)
    {
        Console.Write(prompt);
        StringBuilder sb = new StringBuilder();
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (sb.Length > 0)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }
            }
            else if (key.KeyChar != '\u0000')
            {
                sb.Append(key.KeyChar);
                Console.Write('*');
            }
        }
        return sb.ToString();
    }
}