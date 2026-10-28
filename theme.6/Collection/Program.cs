using System;
using System.Collections.Generic;

    class Program
{
    static void Main(string[] args)
    {
        List<CollectionItem> myGames = new List<CollectionItem>
            {
                new SurvivalAndShooterGame("Rust", 2018, 10, "PVP / Выживание", 1964),
                new SurvivalAndShooterGame("Stalzone", 2022, 10, "PVPVE / Сталкер-стайл", 269),
                new SurvivalAndShooterGame("Counter-Strike 2", 2023, 7, "Командный тактический шутер", 6),

                new SimulatorGame("BeamNG.drive", 2015, 10, "Физика мягких тел", "Краш-тесты и езда"),
                new SimulatorGame("Euro Truck Simulator 2", 2012, 10, "Дальнобойщики", "Грузоперевозки по Европе"),
                new SimulatorGame("Car Mechanic Simulator 2021", 2021, 10, "Автомеханик", "Ремонт и тюнинг машин"),
                new SimulatorGame("House Flipper", 2018, 10, "Ремонт домов", "Уборка, дизайн и продажа"),

                new StoryAndVisualNovel("Зайчик", 2021, 10, "Черно-белый хоррор, мистика", 5)
            };

        foreach (var game in myGames)
        {
            game.Describe();
        }

        while (true)
        {
            try
            {
                Console.WriteLine("\n--- МЕНЮ ИГРОВОЙ КОЛЛЕКЦИИ ---");
                Console.WriteLine("1. Показать всю коллекцию таблицей");
                Console.WriteLine("2. Добавить новую игру");
                Console.WriteLine("3. Поиск игры по названию (без учета регистра)");
                Console.WriteLine("4. Посчитать среднюю оценку коллекции");
                Console.WriteLine("5. Выйти из программы");
                Console.Write("Выберите действие (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintGameTable(myGames);
                        break;
                    case "2":
                        AddGameMenu(myGames);
                        break;
                    case "3":
                        SearchGame(myGames);
                        break;
                    case "4":
                        CalculateAverageScore(myGames);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод! Выберите пункт от 1 до 5.");
                        break;
                }
            }
            catch (InvalidCollectionDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ОШИБКА ДАННЫХ]: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденное исключение: {ex.Message}");
            }
        }
    }

    static void PrintGameTable(List<CollectionItem> games)
    {
        Console.WriteLine($"\n{"Категория класса",-23} | {"Название игры",-28} | {"Год",-6} | {"Оценка",-6}");
        Console.WriteLine(new string('-', 71));
        foreach (var game in games)
        {
            Console.WriteLine($"{game.GetType().Name,-23} | {game.Title,-28} | {game.Year,-6} | {game.Score,-6}");
        }
    }
    static void AddGameMenu(List<CollectionItem> games)
    {
        Console.WriteLine("\nВыберите класс игры: 1 - Выживание/Шутер, 2 - Симулятор, 3 - Сюжет/Новелла");
        string type = Console.ReadLine();

        Console.Write("Введите название игры: ");
        string title = Console.ReadLine();

        Console.Write("Введите год релиза: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
            throw new InvalidCollectionDataException("Ошибка ввода! Год должен быть целым числом.");

        Console.Write("Введите вашу оценку (1-10): ");
        if (!int.TryParse(Console.ReadLine(), out int score))
            throw new InvalidCollectionDataException("Ошибка ввода! Оценка должна быть целым числом.");

        if (type == "1")
        {
            Console.Write("Режим игры (PVP/PVE): ");
            string mode = Console.ReadLine();
            Console.Write("Наиграно часов: ");
            if (!int.TryParse(Console.ReadLine(), out int hours)) hours = 0;

            games.Add(new SurvivalAndShooterGame(title, year, score, mode, hours));
        }
        else if (type == "2")
        {
            Console.Write("Поджанр симулятора (Авто/Ремонт): ");
            string sub = Console.ReadLine();
            Console.Write("Главная механика: ");
            string mech = Console.ReadLine();

            games.Add(new SimulatorGame(title, year, score, sub, mech));
        }
        else if (type == "3")
        {
            Console.Write("Атмосфера игры: ");
            string atm = Console.ReadLine();
            Console.Write("Количество концовок: ");
            if (!int.TryParse(Console.ReadLine(), out int endings)) endings = 1;

            games.Add(new StoryAndVisualNovel(title, year, score, atm, endings));
        }
        else
        {
            Console.WriteLine("Отмена. Неизвестный класс игры.");
            return;
        }

        Console.WriteLine("Игра успешно занесена в коллекцию!");
    }

    static void SearchGame(List<CollectionItem> games)
    {
        Console.Write("\nВведите часть названия игры для поиска: ");
        string query = Console.ReadLine().ToLower();

        Console.WriteLine("\nРезультаты поиска (вызов ToString):");
        bool found = false;
        foreach (var game in games)
        {
            if (game.Title.ToLower().Contains(query))
            {
                Console.WriteLine(game);
                found = true;
            }
        }
        if (!found) Console.WriteLine("Игр с таким названием не найдено.");
    }

    static void CalculateAverageScore(List<CollectionItem> games)
    {
        if (games.Count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }

        int sum = 0;
        foreach (var game in games)
        {
            sum += game.Score;
        }

        double average = (double)sum / games.Count;
        Console.WriteLine($"\nСредняя оценка вашей коллекции: {average:F2} из 10");
    }
}
