using System;
using VRKL.MBU;

/// <summary>
/// Subject-Klasse für eine Uhr
/// </summary>
public class Clock : Subject
{

    public Clock() : base()
    {
        DateTime time = DateTime.Now;
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
    }

    /// <summary>
    /// Wie in Gamma lassen wir die Uhr ticken
    /// und benachrichtigen alle Observer.
    /// </summary>
    public void Tick()
    {
        DateTime time = DateTime.Now;
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;

        Notify();
    }

    /// <summary>
    /// Variable für die Stunden
    /// </summary>
    public int Hour
    {
        get { return _hour; }
        set { _hour = value; }
    }

    private int _hour { get; set; }
    /// <summary>
    /// Variable für die Minuten
    /// </summary>
    public int Minute
    {
        get { return _minute; }
        set { _minute = value; }
    }
    private int _minute { get; set; }

    /// <summary>
    /// Variable für die Sekunden
    /// </summary>
    public int Second
    {
        get { return _second; }
        set { _second = value; }
    }
    private int _second { get; set; }
}
