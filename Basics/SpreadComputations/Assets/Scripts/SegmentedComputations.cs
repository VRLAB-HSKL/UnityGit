using UnityEngine;
using System.Collections;

/// <summary>
/// Von der Framerate unabhängige Berechnungen, mit Hilfe
/// von Coroutinen und yield
/// </summary>
public class SegmentedComputations : MonoBehaviour
{
    /// <summary>
    /// Größe des Arrays
    /// </summary>
    private const int arraySize = 50;
    /// <summary>
    /// Paketgröße, die bei einem Aufruf bearbeitet werden soll
    /// </summary>
    private int segmentSize = 10;
    /// <summary>
    /// Kleinste mögliche Zahl
    /// </summary>
    [Range(0, 1000)]
    [Tooltip("Geben Sie die kleinste Zahl an, die wir erzeugen")]
    public int minNumber = 0;
    /// <summary>
    /// Größte mögliche Zahl
    /// </summary>
    [Range(0, 1000)]
    [Tooltip("Geben Sie die größte Zahl an, die wir erzeugen!")]
    public int maxNumber = 1000;
    /// <summary>
    /// Zufallszahlen
    /// </summary>
    private int[] randomNumbers;
	
	private void Awake()
    {
        randomNumbers = new int[arraySize];
		for (var i=0; i< arraySize; i++) 
			randomNumbers [i] = Random.Range (minNumber, maxNumber);

        Debug.Log(">>> Awake: Wir verarbeiten " + arraySize + " zufällige Zahlen zwischen " + minNumber + " und " + maxNumber);
		StartCoroutine (FindMinMax ());
    }
    /// <summary>
    /// Funktion, die die Zufallszahlen nach Minimum und Maximum durchsucht.
    /// Es kommt nicht auf das Verfahren, sondern nur um die Anwendung
    /// an.
    /// </summary>
	private IEnumerator FindMinMax()
    {
		int i;
		int min = maxNumber - 1;
		int max = minNumber - 1;
        Debug.Log(">>> FindMinMax");
        Debug.Log("** Die Berechnung wird gestartet - pro Frame werden " + segmentSize + " Zahlen bearbeitet!");
		for (i = 0; i< arraySize; i++) {
			if (i % segmentSize == 0) {
                Debug.Log("Frame: " + Time.frameCount + ", i: " + i + ", min:"
					+ min + ", max: " + max);
				yield return null;
			}
            Debug.Log("+++ i = " + i);
			if (randomNumbers [i] > max)
				max = randomNumbers [i];
			else if (randomNumbers [i] < min)
				min = randomNumbers [i];
            Debug.Log("+++ Das aktuelle Minimum ist " + min);
            Debug.Log("+++ Das aktuelle Maximum ist " + max);
		}

		// Berechnung ist fertig, wir de-aktivieren
		// die Komponente mit enable = false;
		Debug.Log("** Die Berechnung ist beendet - Komponente wird jetzt de-aktiviert!");
        Debug.Log("** Das berechnete Minimum :" + min);
        Debug.Log("** Das berechnete Maximum :" + max);
        Debug.Log("<<< FindMinMax");
        enabled = false;
	}
}
