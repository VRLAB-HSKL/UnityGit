using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.IO;

/// <summary>
/// Beispiel für das Einlesen von Text und Bitmaps
/// während der Laufzeit einer Unity-Anwendung.
/// </summary>
public class ReadManualResources : MonoBehaviour
{
    /// <summary>
    /// Dateiname für die variable Bitmap
    /// </summary>
	public string fileName;
    /// <summary>
    /// Die URL für die Bitmap, die wir
    /// im Inspektor definineren können.
    /// <remark>
    /// Wie alle anderen URLs auch beginnt diese
    /// mit "file:".
    /// </remark>
    /// </summary>
    private string m_url;
    /// <summary>
    /// Eine Instanz einer Bitmap für die Szene
    /// </summary>
	private RawImage m_image;

    /// <summary>
    /// URL für die fest definierte Bitmap
    /// </summary>
    private string m_urlFixed;
    /// <summary>
    /// Der Name der Download-Bitmap
    /// </summary>
	private string m_fixedName = "download.png";
    /// <summary>
    /// Die Instanz für die Download-Bitmap
    /// </summary>
	private RawImage m_fixedImage;

    /// <summary>
    /// URL für 
    /// </summary>
    private string m_urlText;
    /// <summary>
    /// Der Name der ASCII-Datei mit Text
    /// </summary>
	private string m_textName = "titel.txt";
    /// <summary>
    /// Instanz von Text für die Ausgabe des Texts
    /// </summary>
	private Text m_txt;

	/// <summary>
    /// Verbindungen herstellen
    /// </summary> 
	/// <remarks>
	/// Wir verwenden GameObject.Find, damit können wir
	/// die GameObjects mit ihren Namen ansprechen.
	/// </remarks>
	private void Awake()
    {
		m_image = GameObject.Find ("extern").GetComponent<RawImage> ();
		m_fixedImage = GameObject.Find ("fixed").GetComponent<RawImage> ();
		m_txt = GameObject.Find ("titel").GetComponent<Text> ();
	}

    /// <summary>
    /// Wir bauen die Pfade zusammen und lesen anschließend
    /// jedes Asset in einer Coroutine ein.
    /// 
    /// Wir gehen dabei davon aus, dass die Assets, die
    /// wir verwenden im Verzeichnis "Resources" zusammengefasst sind.
    /// </summary>
	private void Start ()
    {
        m_urlFixed = "file:" + Application.dataPath;
        m_urlFixed = Path.Combine (m_urlFixed, "Resources");
        m_urlFixed = Path.Combine (m_urlFixed, m_fixedName);

        m_url = "file:" + Application.dataPath;
        m_url = Path.Combine (m_url, "Resources");
        m_url = Path.Combine (m_url, fileName);

        m_urlText = "file:" + Application.dataPath;
        m_urlText = Path.Combine (m_urlText, "Resources");
        m_urlText = Path.Combine (m_urlText, m_textName);

        // Text laden
		StartCoroutine (LoadText());
        // Das fest vorgebene png-File "download" laden
        StartCoroutine(LoadTexture(m_urlFixed, m_fixedImage));
        // Das im Inspektor definierbare png-File  laden
        StartCoroutine(LoadTexture(m_url, m_image));
    }

    /// <summary>
    /// Coroutine, die eine Bitmap mit Hilfe einer URL einliest.
    /// <remark>
    /// Wir verwenden eine Instanz von DownloadHandlerTexture für
    /// den Download.
    /// </remark>
    /// </summary>
    private IEnumerator LoadTexture(string uri, RawImage whichImage)
    {
        UnityWebRequest www = new UnityWebRequest(uri);
        DownloadHandlerTexture texHandler = new DownloadHandlerTexture(true);
        www.downloadHandler = texHandler;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.LogError(www.error);
        else
            whichImage.texture = texHandler.texture;
    }

    /// <summary>
    /// Coroutine, die den Text aus dem Netz lädt
    /// </summary>
	private IEnumerator LoadText()
    {
        UnityWebRequest www = new UnityWebRequest(m_urlText);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.LogError(www.error);
        else
            m_txt.text = www.downloadHandler.text;
    }
}
