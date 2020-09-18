using System;

/// <summary>
/// Controller für eine Uhr
/// </summary>
public class ClockController
{
    public ClockController(Clock c)
    {
        TheClock = c;
    }

    public Clock TheClock { get; set; }

    /// <summary>
    /// Die Uhr mit Hilfe von <code>DateTime</code>
    /// neu setzen.
    /// <remarks>
    /// Wir könnten hier auch überprüfen, ob die Uhrzeit in der
    /// Genauigkeit, die wir verwenden überhaupt neu gesetzt werden
    /// muss. Darauf verzichten wir hier.</remarks>
    /// </summary>
    public void Refresh()
    {
        DateTime time = DateTime.Now;
        TheClock.Hour = time.Hour;
        TheClock.Minute = time.Minute;
        TheClock.Second = time.Second;
    }
}