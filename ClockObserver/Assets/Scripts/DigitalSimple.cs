using UnityEngine.UI;
using VRKL.MBU;

/// <summary>
/// Eine Anzeige einer Digitaluhr mit Stunden- und Minutenangaben.
/// </summary>
public class DigitalSimple : Observer
{
    /// <summary>
    /// Das beobachtete Objekt
    /// </summary>
    protected Clock Model;

    /// <summary>
    /// Text-Feld für die Ausgabe der Digitaluhr
    /// </summary>
    private Text m_txt;

    /// <summary>
    /// <summary>
    /// In Awake stellen die Verbindung zur Subject-Klasse her.
    /// </summary>
    /// </summary>
    private void Awake()
    {
        Model = new Clock();
        Model.Attach(this);
    }

    /// <summary>
    /// Wir lassen die Uhr ticken ...
    /// </summary>
    private void FixedUpdate()
    {
        Model.Tick();
    }

    /// <summary>
    /// Wir verbinden die Variable txt mit einer Text-Component des GameObjects.
    /// </summary>
    private void Start()
    {
        m_txt = gameObject.GetComponent<Text>();
    }

    /// <summary>
    /// wir bauen den Text den wir ausgeben zusammen.
    /// </summary>
    /// <returns></returns>
    public override void Refresh()
    {
        string timeOutput;

        timeOutput = Model.Hour + " : " + Model.Minute;
        m_txt.text = timeOutput;
    }
}


