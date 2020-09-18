using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Ein Countdown mit Text.
/// </summary>
public class CountdownText : MonoBehaviour
{
	private int m_countdownTimerDelay;
	private float m_countdownTimerStartTime;
	private Text m_txt;

	public int startingNumber = 10;

    /// <summary>
    /// Awake wird aufgerufen, wenn die Anwendung startet.
    /// Es wird garantiert, dass diese Funktion vor dem
    /// ersten Aufruf von Start() aufgerufen wird!
    /// 
    /// Hier initialisieren wir den Timer.
    /// </summary>
	void Awake(){
		CountdownTimerReset( startingNumber );
	}

    /// <summary>
    /// Wir verbinden die Variable txt mit einem Game-Object.
    /// </summary>
    private void Start ()
    {
        m_txt = gameObject.GetComponent<Text> ();
	}

    /// <summary>
    /// Den Text aktualisieren.
    /// </summary>
	private void Update() => m_txt.text = CountdownTimerText();

    /// <summary>
    /// Reset für den Timer. Wir fragen die Zeit
    /// mit der Funktion Time.time ab.
    /// </summary>
    /// <param name="starter">Wartezeit, bis wir wieder die Zeit stoppen</param>
	private void CountdownTimerReset(int starter)
{
		m_countdownTimerDelay = starter;
		m_countdownTimerStartTime = Time.time;
	}
	
    /// <summary>
    /// wie lange dauert es noch, bis der Countdown beendet ist.
    /// </summary>
    /// <returns></returns>
	private int CountdownTimerSecondsRemaining() {
		int elapsedSeconds = (int)(Time.time - m_countdownTimerStartTime);
		int secondsLeft = (m_countdownTimerDelay - elapsedSeconds);
		return secondsLeft;
	}

    /// <summary>
    /// wir bauen den Text den wir ausgeben mit Hilfe der Funktion toString().
    /// </summary>
    /// <returns></returns>
	private string CountdownTimerText(){
		if (CountdownTimerSecondsRemaining () > 0)
			return CountdownTimerSecondsRemaining ().ToString();
		else
			return "Start!";
	}
}


