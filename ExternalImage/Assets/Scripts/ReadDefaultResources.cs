using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Beispiel für das Lesen von externen Resources während der Laufzeit. 
/// </summary>
public class ReadDefaultResources : MonoBehaviour
{
	/// <summary>
	/// Variable für die Bitmap, deren Name wir
	/// im Inspektor angeben können.
	/// </summary>
	public string m_fileName;
	/// <summary>
	/// Variable für die Bitmap, die wir während der Laufzeit einlesen.
	/// </summary>
	private RawImage m_image;
	/// <summary>
	/// Variable für den Dateinamen der Bitmap, die wir
	/// einlesen möchten.
	/// </summary>
	/// <remarks>
	/// Wir geben für den Dateinamen keinen Suffix an!
	/// </remarks>
	private const string m_fixedName = "download";

	/// <summary>
	/// Variable für die Bitmap mit Namen fixedName.
	/// </summary>
	private RawImage m_fixedImage;
	/// <summary>
	/// Variable für das Text-Element in der Szene
	/// </summary>
	private const string m_textName = "titel";
	/// <summary>
	/// Variable, das den Inhalt der externen Datei aufnimmt.
	/// </summary>
	/// <remarks>
	/// Der Inhalt als String steht auf der Komponente txt.text.
	/// </remarks>
	private Text m_txt;

    /// <summary>
    /// Wir stellen die Verbindung zwischen unseren Variablen 
    /// und Komponenten eines GameObjects her.
    /// 
    /// Es werden keine Fehler abgefangen!
    /// </summary>
	private void Awake() {
		m_image = GameObject.Find ("extern").GetComponent<RawImage> ();
        m_fixedImage = GameObject.Find ("fixed").GetComponent<RawImage> ();
        m_txt = GameObject.Find ("titel").GetComponent<Text> ();
	}

    /// <summary>
    /// Text und Bitmap laden und darstellen.
    /// </summary>
	private void Start () {
		m_image.texture = Resources.Load(m_fileName) as Texture;
		m_fixedImage.texture = Resources.Load (m_fixedName) as Texture;
		TextAsset textAsset = Resources.Load (m_textName) as TextAsset;
        m_txt.text = textAsset.text as string;
	}
}
