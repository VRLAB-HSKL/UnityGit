using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Darstellung eines Countdowns
/// mit Hilfe von Bitmaps.
/// </summary>
public class Countdown : MonoBehaviour
{ 	
    /// <summary>
    /// Sprite für die Zahl 1
    /// </summary>
	public Sprite imageDigit1;
    /// <summary>
    /// Sprite für die Zahl 3
    /// </summary>
	public Sprite imageDigit2;
    /// <summary>
    /// Sprite für die Zahl 3
    /// </summary>
	public Sprite imageDigit3;
    /// <summary>
    /// Sprite für die Zahl 4
    /// </summary>
	public Sprite imageDigit4;
    /// <summary>
    /// Sprite für die Zahl 15
    /// </summary>
	public Sprite imageDigit5;
    /// <summary>
    /// Sprite für die Zahl 6
    /// </summary>
	public Sprite imageDigit6;
    /// <summary>
    /// Sprite, das am Ende des Countdowns dargestellt werden soll.
    /// </summary>
	public Sprite imageStart;

    private Image m_image;

    /// <summary>
    /// Bei welcher Zahl beginnen wir?
    /// </summary>
    private int m_startingNumber = 6;
    /// <summary>
    /// Wie viele Sekunden warten wir für den Timer?
    /// </summary>
    private int m_countdownTimerDelay;
    /// <summary>
    /// Start-Zeit für den Counter
    /// </summary>
	private float m_countdownTimerStartTime;
	
	void Awake()
    {
		CountdownTimerReset(m_startingNumber);
	}

    /// <summary>
    /// Verbindung zur Image-Kompomente herstellen.
    /// 
    /// Wir überprüfen nicht, ob es diese Komponente gibt!
    /// </summary>
	void Start ()
    {
		m_image = gameObject.GetComponent<Image> ();
	}

	void Update ()
    {
		m_image.sprite = CountdownTimerSprite ();
	}

    /// <summary>
    /// Den Counter zurücksetzen
    /// </summary>
    /// <param name="delayInSeconds"></param>
	void CountdownTimerReset(int delayInSeconds)
    {
        m_countdownTimerDelay = delayInSeconds;
        m_countdownTimerStartTime = Time.time;
	}
	
    /// <summary>
    /// Berechnen, wie viele Sekunden der Timer noch zählen muss.
    /// </summary>
    /// <returns>
    /// Sekunden, die noch zu zählen sind.
    /// </returns>
	int CountdownTimerSecondsRemaining() {
		int elapsedSeconds = (int)(Time.time - m_countdownTimerStartTime);
		int secondsLeft = (m_countdownTimerDelay - elapsedSeconds);
		return secondsLeft;
	}
	
    /// <summary>
    /// Setzen des korrekten Sprites, abhängig vom Ergebnis von CountdownTimerSecondsRemaining().
    /// </summary>
    /// <returns></returns>
	Sprite CountdownTimerSprite(){
		switch( CountdownTimerSecondsRemaining() )
        {
		case 6:
			return imageDigit6;
		case 5:
			return imageDigit5;
		case 4:
			return imageDigit4;
		case 3:
			return imageDigit3;
		case 2:
			return imageDigit2;
		case 1:
			return imageDigit1;
		default:
			return imageStart;
		}
	}
}

