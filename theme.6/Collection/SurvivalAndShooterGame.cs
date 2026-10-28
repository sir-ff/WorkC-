using System;

    public class SurvivalAndShooterGame : CollectionItem
{
    public string Mode { get; set; }
    public int HoursPlayed { get; set; }

    public SurvivalAndShooterGame(string title, int year, int score, string mode, int hoursPlayed)
        : base(title, year, score)
    {
        Mode = mode;
        HoursPlayed = hoursPlayed;
    }

    public override void Describe()
    {
        base.Describe();
        Console.WriteLine($" | Режим: {Mode}, Наиграно: {HoursPlayed} ч.");
    }
}
