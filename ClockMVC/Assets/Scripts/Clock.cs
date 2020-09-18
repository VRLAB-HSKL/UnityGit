/// <summary>
/// Model-Klasse für eine Uhr
/// </summary>
public class Clock
{
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
