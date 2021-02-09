using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Eine Anzeige einer Digitaluhr mit Stunden und Minuten
/// </summary>
public class DigitalSimple : ClockView
{
    /// <summary>
    /// Text-Variable für die Ausgabe der digitalen Zeit.
    /// </summary>
    private Text m_txt;

    /// <summary>
    /// Wir verbinden die Variable txt mit einer Text-Component des GameObjects.
    /// </summary>
    private void Start()
    {
        m_txt = gameObject.GetComponent<Text>();
    }

    /// <summary>
    /// wir bauen den Text den wir ausgeben mit Hilfe der Funktion toString().
    /// </summary>
	protected override void Draw()
    {
        string timeOutput;

        timeOutput = Model.Hour + " : " + Model.Minute;
        m_txt.text = timeOutput;
    }
}


