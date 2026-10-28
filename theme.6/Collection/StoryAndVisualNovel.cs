using System;

    public class StoryAndVisualNovel : CollectionItem
{
    public string Atmosphere { get; set; }
    public int EndingsCount { get; set; }

    public StoryAndVisualNovel(string title, int year, int score, string atmosphere, int endingsCount)
        : base(title, year, score)
    {
        Atmosphere = atmosphere;
        EndingsCount = endingsCount;
    }

    public override void Describe()
    {
        base.Describe();
        Console.WriteLine($" | Атмосфера: {Atmosphere}, Вариативность: {EndingsCount} конц.");
    }
}
