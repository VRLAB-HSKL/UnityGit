using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Beispiel für das Einlesen von Text und Bitmaps
/// während der Laufzeit einer Unity-Anwendung.
/// Die Resourcen stehen dabei im WWW und
/// werden mit Hilfe ihrer URL angesprochen.
/// </summary>
public class ReadWebResources : MonoBehaviour
{
    /// <summary>
    /// URL für die Bitmap, die wir
    /// von webhome lesen.
    /// 
    /// Default-Wert ist das pdf-Logo.
    /// </summary>
    public string url = "http://webhome.hs-kl.de/~brill/Assets/images/pdf.jpg";
    /// <summary>
    /// Variable für das Darstellen der eingelesenen Bitmap aus dem Web.
    /// </summary>
    private RawImage m_image;

    /// <summary>
    /// URL für die fest vereinbarte Bitmap.
    /// 
    /// Wir verwenden das download-Logo.
    /// </summary>
	private  string m_urlFixed = "http://webhome.hs-kl.de/~brill/Assets/images/download.png";
    /// <summary>
    /// Instanz für das Darstellen des Logos.
    /// </summary>
    private RawImage m_fixedImage;

    /// <summary>
    /// URL für den Text, den wir aus dem Web einlesen.
    /// 
    /// Wir verwenden dafür eine Datei auf webhome.
    /// </summary>
    private  string m_urlText = "http://webhome.hs-kl.de/~brill/Assets/titel.txt";
    /// <summary>
    /// Variable für den aus der Datei eingelesenen Text.
    /// </summary>
    private Text m_txt;


    /// <summary>
    /// Verbindungen herstellen
    /// </summary> 
    /// <remarks>
    /// Wir verwenden GameObject.Find, damit können wir
    /// die GameObjects mit ihren Namen ansprechen.
    /// </remarks>
    private void Awake() {
		m_image = GameObject.Find ("extern").GetComponent<RawImage> ();
        m_fixedImage = GameObject.Find ("fixed").GetComponent<RawImage> ();
        m_txt = GameObject.Find ("titel").GetComponent<Text> ();
	}

    /// <summary>
    /// Wir verwenden für die Assets Coroutines,
    /// um sie einzulesen.
    /// 
    /// Es gibt eine Coroutine für ASCII-Text und
    /// eine für die Bitmaps. 
    /// </summary>
	private void Start () {
        // Text laden
        StartCoroutine(LoadText());
        // Das fest vorgebene png-File "download" laden
        StartCoroutine(LoadTexture(m_urlFixed, m_fixedImage));
        // Das im Inspektor definierbare png-File  laden
        StartCoroutine(LoadTexture(url, m_image));
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
    /// Coroutine, die einen Text aus dem Netz lädt
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
