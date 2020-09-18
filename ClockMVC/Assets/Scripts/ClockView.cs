using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für die Views einer Uhr.
/// 
/// Die von dieser Klasse abgeleiteten Klassen
/// implementieren die Funktion <code>DrawTime</code>.
/// </summary>
public abstract class ClockView : MonoBehaviour
{
    protected Clock Model;
    protected ClockController Cont;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    protected virtual void Awake()
    {
        Model = new Clock();
        Cont= new ClockController(Model);
    }

    /// <summary>
    /// Wir fragen mit DateTime.Now
    /// die aktuelle Uhrzeit ab,
    /// setzen diese Zeit und besetzen die 
    /// Variablen für die Ausgabe der Uhrzeiger.
    /// </summary>
    protected void Update()
    {
        Cont.Refresh();
        Draw();
    }

    /// <summary>
    /// Die Winkel für die Zeiger berechnen, anwenden
    /// und damit die grafische Ausgabe durchführen.
    /// 
    /// Der Winkel für den Stundenzeiger erwartet die Stunden
    /// im 12-Stundenformat und berechnet sich als -360*(minuten/12).
    /// 
    /// Der Winkel für den Minutenzeiger berechnet sich als
    /// -360*(minuten/60).
    /// </summary>
    protected abstract void Draw();
}
