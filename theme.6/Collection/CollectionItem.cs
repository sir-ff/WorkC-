using System;

    public class CollectionItem
{
    private string title;
    private int score;

    public string Title
    {
        get => title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidCollectionDataException("Название игры не может быть пустым!");
            title = value;
        }
    }

    public int Year { get; set; }

    public int Score
    {
        get => score;
        set
        {
            if (value < 1 || value > 10)
                throw new InvalidCollectionDataException("Оценка игры должна быть в диапазоне от 1 до 10!");
            score = value;
        }
    }

    public CollectionItem(string title, int year, int score)
    {
        Title = title;
        Year = year;
        Score = score;
    }

    public virtual void Describe()
    {
        Console.Write($"[{GetType().Name}] {Title} ({Year} г.) | Моя оценка: {Score}/10");
    }

    public override string ToString()
    {
        return $"{Title} ({Year}) — {Score}/10";
    }
}