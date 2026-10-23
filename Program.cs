using System;

namespace ConsoleTracker
{
    public class InvalidWorkoutDataException : Exception
    {
        public InvalidWorkoutDataException(string message) : base(message) { }
    }

    public class WorkoutLog
    {
        private string _exerciseName;
        private int _weightKg;
        private DateTime _date;
        private bool _isCompleted;

        public string ExerciseName
        {
            get => _exerciseName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidWorkoutDataException("Название упражнения не может быть пустым!");
                _exerciseName = value;
            }
        }

        public int WeightKg
        {
            get => _weightKg;
            set
            {
                if (value < 0 || value > 1000)
                    throw new InvalidWorkoutDataException("Рабочий вес должен быть от 0 до 1000 кг.");
                _weightKg = value;
            }
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }

        public WorkoutLog(string exerciseName, int weightKg, DateTime date, bool isCompleted)
        {
            ExerciseName = exerciseName;
            WeightKg = weightKg;
            Date = date;
            IsCompleted = isCompleted;
        }

        public static bool operator ==(WorkoutLog left, WorkoutLog right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            return left.ExerciseName == right.ExerciseName &&
                   left.WeightKg == right.WeightKg &&
                   left.Date == right.Date &&
                   left.IsCompleted == right.IsCompleted;
        }

        public static bool operator !=(WorkoutLog left, WorkoutLog right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is WorkoutLog log && this == log;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ExerciseName, WeightKg, Date, IsCompleted);
        }
    }

    public class WorkoutStorage
    {
        private WorkoutLog[] _items;
        private int _count;

        public int Count => _count;

        public WorkoutStorage(int initialCapacity = 4)
        {
            _items = new WorkoutLog[initialCapacity];
            _count = 0;
        }

        public WorkoutLog this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Запись с таким индексом не существует.");
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Запись с таким индексом не существует.");
                _items[index] = value;
            }
        }

        public void Add(WorkoutLog item)
        {
            if (_count == _items.Length)
            {
                WorkoutLog[] newArray = new WorkoutLog[_items.Length * 2];
                for (int i = 0; i < _items.Length; i++)
                {
                    newArray[i] = _items[i];
                }
                _items = newArray;
            }
            _items[_count] = item;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Неверный индекс для удаления.");

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _items[_count - 1] = null;
            _count--;
        }

        public static WorkoutStorage operator +(WorkoutStorage left, WorkoutStorage right)
        {
            WorkoutStorage result = new WorkoutStorage(left.Count + right.Count);
            for (int i = 0; i < left.Count; i++) result.Add(left[i]);
            for (int i = 0; i < right.Count; i++) result.Add(right[i]);
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WorkoutStorage tracker = new WorkoutStorage();

            tracker.Add(new WorkoutLog("Жим лёжа", 70, DateTime.Now.AddDays(0), true));
            tracker.Add(new WorkoutLog("Жим в наклоне", 18, DateTime.Now.AddDays(0), true));
            tracker.Add(new WorkoutLog("бабочка", 54, DateTime.Now.AddDays(0), true));

            tracker.Add(new WorkoutLog("сгиб штанг бицепс", 30, DateTime.Now.AddDays(0), true));
            tracker.Add(new WorkoutLog("скамья скотта", 30, DateTime.Now.AddDays(0), true));
            tracker.Add(new WorkoutLog("молотки", 12, DateTime.Now.AddDays(0), true));

            tracker.Add(new WorkoutLog("шраги", 18, DateTime.Now.AddDays(0), true));
            
            while (true)
            {
                try
                {
                    Console.WriteLine("\n=== МЕНЮ ТРЕНИРОВОЧНОГО ТРЕКЕРА ===");
                    Console.WriteLine("1. Вывести все подходы");
                    Console.WriteLine("2. Добавить новый подход");
                    Console.WriteLine("3. Найти по названию упражнения");
                    Console.WriteLine("4. Показать средний рабочий вес");
                    Console.WriteLine("5. Исправить запись");
                    Console.WriteLine("6. Удалить запись");
                    Console.WriteLine("7. Выход");
                    Console.Write("Выберите пункт: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            PrintTable(tracker);
                            break;
                        case "2":
                            AddNewRecord(tracker);
                            break;
                        case "3":
                            SearchByExercise(tracker);
                            break;
                        case "4":
                            ShowAverageWeight(tracker);
                            break;
                        case "5":
                            EditRecord(tracker);
                            break;
                        case "6":
                            DeleteRecord(tracker);
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Нет такой пунк дурачок, пиши то че надо.");
                            break;
                    }
                }
                catch (InvalidWorkoutDataException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[ОШИБКА ВАЛИДАЦИИ ДАННЫХ]: {ex.Message}");
                    Console.ResetColor();
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[ОШИБКА ИНДЕКСА]: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n[СИСТЕМНАЯ ОШИБКА]: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        static void PrintTable(WorkoutStorage storage)
        {
            if (storage.Count == 0)
            {
                Console.WriteLine("База данных тренировок пуста.");
                return;
            }

            Console.WriteLine($"\n{"№",-3} | {"Упражнение",-24} | {"кг",-9} | {"Дата",-10} | {"Выполнено?",-10}");
            Console.WriteLine(new string('-', 68));

            for (int i = 0; i < storage.Count; i++)
            {
                WorkoutLog log = storage[i];
                string status = log.IsCompleted ? "Да" : "Нет";
                Console.WriteLine($"{i + 1,-3} | {log.ExerciseName,-24} | {log.WeightKg,-9} | {log.Date:dd.MM.yyyy} | {status,-10}");
            }
        }

        static void AddNewRecord(WorkoutStorage storage)
        {
            Console.Write("Введите название упражнения: ");
            string name = Console.ReadLine();

            Console.Write("Введите рабочий вес в кг: ");
            if (!int.TryParse(Console.ReadLine(), out int weight))
            {
                Console.WriteLine("Jopa вес должен быть чилом!!!");
                return;
            }

            Console.Write("Подход выполнен полностью? (да/нет): ");
            string statusInput = Console.ReadLine()?.Trim().ToLower();
            bool isCompleted = (statusInput == "да" || statusInput == "yes");

            WorkoutLog newLog = new WorkoutLog(name, weight, DateTime.Now, isCompleted);
            storage.Add(newLog);
            Console.WriteLine("Запись успешно добавлена!");
        }

        static void EditRecord(WorkoutStorage storage)
        {
            if (storage.Count == 0)
            {
                Console.WriteLine("Нет записей для изменения.");
                return;
            }
            Console.Write($"Введите порядковый номер записи для изменения (1-{storage.Count}): ");
            if (!int.TryParse(Console.ReadLine(), out int num) || num < 1 || num > storage.Count)
            {
                Console.WriteLine("эээ ты че леее нет такой номер.");
                return;
            }
            int index = num - 1;
            WorkoutLog log = storage[index];
            Console.WriteLine($"Редактируем: {log.ExerciseName} ({log.WeightKg} кг)");
            Console.Write("Введите новое название (или оставьте пустым, чтобы не менять): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                log.ExerciseName = newName;
            }
            Console.Write("Введите новый вес (или оставьте пустым, чтобы не менять): ");
            string weightInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(weightInput))
            {
                if (int.TryParse(weightInput, out int newWeight))
                    log.WeightKg = newWeight;
                else
                    Console.WriteLine("Некорректный вес. Изменения веса отменены.");
            }
            Console.Write("Подход выполнен полностью? (да/нет, или пустой ввод, чтобы не менять): ");
            string statusInput = Console.ReadLine()?.Trim().ToLower();
            if (!string.IsNullOrEmpty(statusInput))
            {
                log.IsCompleted = (statusInput == "да" || statusInput == "yes");
            }
            Console.WriteLine("Запись успешно изменена!");
        }

        static void DeleteRecord(WorkoutStorage storage)
        {
            if (storage.Count == 0)
            {
                Console.WriteLine("Нет записей для удаления.");
                return;
            }
            Console.Write($"Введите номер записи для удаления (1-{storage.Count}): ");
            if (!int.TryParse(Console.ReadLine(), out int num) || num < 1 || num > storage.Count)
            {
                Console.WriteLine("Ошибка: неверный номер записи.");
                return;
            }
            int index = num - 1;
            storage.RemoveAt(index);
            Console.WriteLine("Запись успешно удалена!");
        }
        static void SearchByExercise(WorkoutStorage storage)
        {
            Console.Write("Введите текст для поиска упражнения: ");
            string query = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(query)) return;
            Console.WriteLine("\nРезультаты поиска:");
            bool found = false;
            for (int i = 0; i < storage.Count; i++)
            {
                if (storage[i].ExerciseName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string status = storage[i].IsCompleted ? "Да" : "Нет";
                    Console.WriteLine($"- {storage[i].ExerciseName} ({storage[i].WeightKg} кг), Выполнено: {status}");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Ничего не найдено.");
        }
        static void ShowAverageWeight(WorkoutStorage storage)
        {
            if (storage.Count == 0)
            {
                Console.WriteLine("Нет данных для расчета.");
                return;
            }
            int totalWeight = 0;
            for (int i = 0; i < storage.Count; i++)
            {
                totalWeight += storage[i].WeightKg;
            }
            double average = (double)totalWeight / storage.Count;
            Console.WriteLine($"\nСредний рабочий вес во всех упражнениях: {average:F2} кг.");
        }
    }
}