using System;
using System.Collections.Generic;

public class RustRaidCalculations
{
    struct RaidCost
    {
        public int Rocket;
        public int C4;
        public int Satchel;
        public int bobovka;
        public int propane;
        public int Molotov;

        public RaidCost(int r, int c, int s, int b, int p, int m)
        {
            Rocket = r;
            C4 = c;
            Satchel = s;
            bobovka = b;
            propane = p;
            Molotov = m;
        }
    }

    public void fortytwo()
    {
        Dictionary<string, RaidCost> targetCosts = new Dictionary<string, RaidCost>(StringComparer.OrdinalIgnoreCase)
        {
            { "Деревянная дверь", new RaidCost(1, 1, 2, 6, 1, 2) },
            { "Железная дверь", new RaidCost(2, 1, 4, 18, 2, 0) },
            { "Гаражная дверь", new RaidCost(3, 2, 9, 42, 5, 0) },
            { "МВК дверь", new RaidCost(5, 3, 15, 69, 8, 0) },
            { "Деревянная стена", new RaidCost(2, 1, 3, 10, 1, 4) },
            { "Каменная стена", new RaidCost(4, 2, 10, 40, 2, 0) },
            { "Железная стена", new RaidCost(8, 4, 23, 84, 1, 0) },
            { "МВК стена", new RaidCost(15, 8, 46, 167, 1, 0) }
        };

        Console.WriteLine("Выбор рейда: \nдверь: деревянна; железная; Гаражная; МВК.\nстена: Деревянна; Каменная; Железная; МВК.");
        Console.WriteLine("Введите че надо рейдить:");
        string? Text = Console.ReadLine()?.Trim();

        if (Text != null && targetCosts.TryGetValue(Text, out RaidCost cost))
        {
            Console.WriteLine($"Сколько штук '{Text}' надо взорвать?");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                Console.WriteLine($"\nЧтобы разнести {count}x '{Text}', выбери один из вариантов:");
                Console.WriteLine($"• Ракеты: {cost.Rocket * count} шт.");
                Console.WriteLine($"• С4: {cost.C4 * count} шт.");
                Console.WriteLine($"• Сачели: {cost.Satchel * count} шт.");
                Console.WriteLine($"• Бобовые гранаты: {cost.bobovka * count} шт.");
            }
            else
            {
                Console.WriteLine("Неверное количество!");
            }
        }
        else
        {
            Console.WriteLine("Не знаю такой постройки. Пиши как в списке (например: Каменная стена).");
        }
    }
}
