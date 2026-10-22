using System;
using System.Collections.Generic;
using System.IO;

namespace GamesCatalog
{
    class Program
    {
        const string FILE_NAME = "catalog.txt";

        static List<string> names = new List<string>();
        static List<int> years = new List<int>();
        static List<double> ratings = new List<double>();
        static List<bool> passedList = new List<bool>();
        static List<string> genres = new List<string>();

        static void Main(string[] args)
        {
            LoadData();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ КАТАЛОГА ИГР ===");
                Console.WriteLine("1. Показать все игры");
                Console.WriteLine("2. Добавить новую игру");
                Console.WriteLine("3. Найти игру по названию");
                Console.WriteLine("4. Фильтр: Игры новее определенного года");
                Console.WriteLine("5. Топ-3 игры по рейтингу (0-5)");
                Console.WriteLine("6. Статистика базы данных");
                Console.WriteLine("7. Редактировать игру");
                Console.WriteLine("8. Удалить игру");
                Console.WriteLine("9. Сохранить данные и выйти");
                Console.Write("\nВыберите пункт меню: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllGames();
                        break;
                    case "2":
                        AddNewGame();
                        break;
                    case "3":
                        SearchByName();
                        break;
                    case "4":
                        FilterByYear();
                        break;
                    case "5":
                        ShowTop3();
                        break;
                    case "6":
                        ShowStatistics();
                        break;
                    case "7":
                        EditGame();
                        break;
                    case "8":
                        DeleteGame();
                        break;
                    case "9":
                        SaveData();
                        Console.WriteLine("Данные успешно сохранены. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню. Нажмите любую клавишу для повтора...");
                        break;
                }
                Console.ReadKey();
            }
        }

        static void ShowAllGames()
        {
            if (names.Count == 0)
            {
                Console.WriteLine("Каталог пока пуст.");
                return;
            }

            Console.WriteLine("{0,-4} {1,-25} {2,-6} {3,-8} {4,-10} {5,-15}", "№", "Название", "Год", "Рейтинг", "Пройдено", "Жанр");
            Console.WriteLine(new string('-', 75));

            for (int i = 0; i < names.Count; i++)
            {
                string passedText = passedList[i] ? "Да" : "Нет";
                Console.WriteLine("{0,-4} {1,-25} {2,-6} {3,8:F1} {4,-10} {5,-15}", i + 1, names[i], years[i], ratings[i], passedText, genres[i]);
            }
        }

        static void AddNewGame()
        {
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ИГРЫ ===");

            string name;
            while (true)
            {
                Console.Write("Введите название игры: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && !name.Contains(";")) break;
                Console.WriteLine("Ошибка! Название не может быть пустым и не должно содержать символ ';'.");
            }

            int year;
            while (true)
            {
                Console.Write("Введите год выпуска: ");
                if (int.TryParse(Console.ReadLine(), out year) && year > 1950 && year <= DateTime.Now.Year + 1) break;
                Console.Write("Некорректный год. ");
            }

            double rating;
            while (true)
            {
                Console.Write("Введите рейтинг (от 0 до 5, в РФ через запятую): ");
                if (double.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 5) break;
                Console.Write("Некорректный формат рейтинга. ");
            }

            bool passed;
            while (true)
            {
                Console.Write("Игра пройдена? (да/нет): ");
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "да" || input == "yes") { passed = true; break; }
                if (input == "нет" || input == "no") { passed = false; break; }
                Console.Write("Ответьте 'да' или 'нет'. ");
            }

            Console.Write("Введите жанр игры: ");
            string genre = Console.ReadLine();

            names.Add(name);
            years.Add(year);
            ratings.Add(rating);
            passedList.Add(passed);
            genres.Add(genre);

            Console.WriteLine("\nИгра успешно добавлена в память!");
        }

        static void SearchByName()
        {
            Console.Write("Введите название игры или его часть для поиска: ");
            string searchInput = Console.ReadLine().ToLower();

            bool found = false;
            Console.WriteLine("\nРезультаты поиска:");

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].ToLower().Contains(searchInput))
                {
                    string passedText = passedList[i] ? "Да" : "Нет";
                    Console.WriteLine("- {0} ({1} г., Рейтинг: {2:F1}, Пройдено: {3}, Жанр: {4})", names[i], years[i], ratings[i], passedText, genres[i]);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }

        static void FilterByYear()
        {
            Console.Write("Показать игры, выпущенные строго после какого года? ");
            int filterYear;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out filterYear)) break;
                Console.Write("Это не число. Ещё раз: ");
            }

            bool found = false;
            Console.WriteLine($"\nИгры, выпущенные после {filterYear} года:");
            for (int i = 0; i < names.Count; i++)
            {
                if (years[i] > filterYear)
                {
                    Console.WriteLine($"- {names[i]} ({years[i]} г.)");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Нет таких игр.");
        }

        static void ShowTop3()
        {
            if (names.Count == 0)
            {
                Console.WriteLine("Каталог пуст.");
                return;
            }

            Console.WriteLine("=== ТОП-3 ИГРЫ ПО РЕЙТИНГУ ===");

            List<int> usedIndices = new List<int>();

            int itemsToShow = Math.Min(3, names.Count);
            for (int step = 0; step < itemsToShow; step++)
            {
                double maxRating = -1;
                int bestIndex = -1;

                for (int i = 0; i < names.Count; i++)
                {
                    if (!usedIndices.Contains(i) && ratings[i] > maxRating)
                    {
                        maxRating = ratings[i];
                        bestIndex = i;
                    }
                }

                if (bestIndex != -1)
                {
                    Console.WriteLine("{0}. {1} — Рейтинг: {2:F1}", step + 1, names[bestIndex], ratings[bestIndex]);
                    usedIndices.Add(bestIndex);
                }
            }
        }

        static void ShowStatistics()
        {
            if (names.Count == 0)
            {
                Console.WriteLine("База пуста. Статистика недоступна.");
                return;
            }

            int totalCount = names.Count;
            double ratingSum = 0;
            int passedCount = 0;

            for (int i = 0; i < totalCount; i++)
            {
                ratingSum += ratings[i];
                if (passedList[i]) passedCount++;
            }

            double averageRating = ratingSum / totalCount;
            double passedPercentage = ((double)passedCount / totalCount) * 100;

            Console.WriteLine("=== СТАТИСТИКА КАТАЛОГА ===");
            Console.WriteLine("Всего игр в базе: {0}", totalCount);
            Console.WriteLine("Средний рейтинг всех игр (без формата): {0}", averageRating);
            Console.WriteLine("Средний рейтинг всех игр (формат F2): {0:F2}", averageRating);
            Console.WriteLine("Пройдено игр: {0} из {1} ({2:F1}%)", passedCount, totalCount, passedPercentage);
        }
        static void EditGame()
        {
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИГРЫ ===");
            if (names.Count == 0)
            {
                Console.WriteLine("Каталог пуст.");
                return;
            }
            ShowAllGames();
            Console.Write("\nВведите номер игры для редактирования: ");
            int number;
            if (int.TryParse(Console.ReadLine(), out number) && number > 0 && number <= names.Count)
            {
                int index = number - 1;
                Console.WriteLine($"\nРедактируем: {names[index]}. Оставьте строку пустой, если не хотите менять поле.");

                Console.Write($"Новое название [{names[index]}]: ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName) && !newName.Contains(";")) names[index] = newName;

                Console.Write($"Новый год выпуска [{years[index]}]: ");
                string yearInput = Console.ReadLine();
                int newYear;
                if (int.TryParse(yearInput, out newYear)) years[index] = newYear;

                Console.Write($"Новый рейтинг [{ratings[index]}]: ");
                string ratingInput = Console.ReadLine();
                double newRating;
                if (double.TryParse(ratingInput, out newRating) && newRating >= 0 && newRating <= 5) ratings[index] = newRating;

                Console.Write($"Пройдена? (да/нет) [{(passedList[index] ? "да" : "нет")}]: ");
                string passedInput = Console.ReadLine().ToLower().Trim();
                if (passedInput == "да") passedList[index] = true;
                if (passedInput == "нет") passedList[index] = false;

                Console.Write($"Новый жанр [{genres[index]}]: ");
                string newGenre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newGenre)) genres[index] = newGenre;
                Console.WriteLine("\nДанные успешно обновлены!");
            }
            else
            {
                Console.WriteLine("Неверный номер.");
            }
        }

        static void DeleteGame()
        {
            Console.WriteLine("=== УДАЛЕНИЕ ИГРЫ ===");
            if (names.Count == 0)
            {
                Console.WriteLine("Каталог пуст.");
                return;
            }
            ShowAllGames();
            Console.Write("\nВведите номер игры для удаления: ");
            int number;
            if (int.TryParse(Console.ReadLine(), out number) && number > 0 && number <= names.Count)
            {
                int index = number - 1;
                string nameToRemove = names[index];

                names.RemoveAt(index);
                years.RemoveAt(index);
                ratings.RemoveAt(index);
                passedList.RemoveAt(index);
                genres.RemoveAt(index);
                Console.WriteLine($"\nИгра '{nameToRemove}' успешно удалена.");
            }
            else
            {
                Console.WriteLine("Неверный номер.");
            }
        }

        static void SaveData()
        {
            string[] linesToSave = new string[names.Count];
            for (int i = 0; i < names.Count; i++)
            {
                linesToSave[i] = $"{names[i]};{years[i]};{ratings[i]};{passedList[i]};{genres[i]}";
            }
            File.WriteAllLines(FILE_NAME, linesToSave);
        }

        static void LoadData()
        {
            if (!File.Exists(FILE_NAME)) return;
            string[] loadedLines = File.ReadAllLines(FILE_NAME);
            foreach (string line in loadedLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');
                if (parts.Length < 5) continue;
                try
                {
                    string name = parts[0];
                    int year = int.Parse(parts[1]);
                    double rating = double.Parse(parts[2]);
                    bool passed = bool.Parse(parts[3]);
                    string genre = parts[4];
                    names.Add(name);
                    years.Add(year);
                    ratings.Add(rating);
                    passedList.Add(passed);
                    genres.Add(genre);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
