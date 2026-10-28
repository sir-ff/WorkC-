using System;

    public class SimulatorGame : CollectionItem
{
    public string SubGenre { get; set; }
    public string MainMechanic { get; set; }

    public SimulatorGame(string title, int year, int score, string subGenre, string mainMechanic)
        : base(title, year, score)
    {
        SubGenre = subGenre;
        MainMechanic = mainMechanic;
    }

    public override void Describe()
    {
        base.Describe();
        Console.WriteLine($" | Направление: {SubGenre}, Суть: {MainMechanic}");
    }
}
