using UnityEngine;
using VRKL.MBU;

/// <summary>
/// View für eine eine analoge Uhr
/// mit Stunden-, Minuten- 
/// und Sekundenzeiger
/// </summary>
public class Analog : Observer
{
    /// <summary>
    /// Das beobachtete Objekt
    /// </summary>
    protected Clock Model;

    /// <summary>
    /// Die Rotationsmatrix für den Stundenzeiger
    /// </summary>
    public Transform hourHand;
    /// <summary>
    /// Die Rotationsmatrix für den Minutenzeiger
    /// </summary>
    public Transform minuteHand;
    /// <summary>
    /// Die Rotationsmatrix für den Minutenzeiger
    /// </summary>
    public Transform secondHand;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    private void Awake()
    {
        Model = new Clock();
        Model.Attach(this);
    }

    /// <summary>
    /// Wir lassen die Uhr ticken
    /// </summary>
    private void FixedUpdate()
    {
        Model.Tick();
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
    public override void Refresh()
    {
        // Winkel für den Minutenzeiger
        float angleMinutes = - 360.0f * ((float)Model.Minute/60.0f);
        // Winkel für den Stundenzeiger
        float angleHours = - 360.0f * ((float)(Model.Hour % 12)/12.0f);
        // Winkel für den Sekundenzeiger
        float angleSeconds = -360.0F * ((float)Model.Second / 60.0F);
        // Jetzt führen wir die Rotation durch
        secondHand.localRotation = Quaternion.Euler(0.0f, 0.0f, angleSeconds);
        minuteHand.localRotation = Quaternion.Euler(0.0f, 0.0f, angleMinutes);
        hourHand.localRotation = Quaternion.Euler(0.0f, 0.0f, angleHours);
    }
}
