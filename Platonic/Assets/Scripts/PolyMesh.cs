using UnityEngine;
using UnityEditor;

/// <summary>
/// Abstrakte Klasse für die Erzeugung eines platonischen Körpers während der Laufzeit.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class PolyMesh : MonoBehaviour
{
    /// <summary>
    /// Wir erzeugen eine Material-Komponente,
    /// so dass wir im Editor dem Netz ein Material zuweisen können.
    /// </summary>
    [Tooltip("Material für die grafische Ausgabe des polygonalen Netzes")]
    public Material meshMaterial;

    /// <summary>
    /// Wir benötigen eine Instanz der Klasse MeshFilter.
    /// </summary>
    protected MeshFilter objectFilter;
    /// <summary>
    /// Mit Hilfe einer Instanz von MeshRenderer stellen wir den platonischen Körper dar.
    /// </summary>
    protected MeshRenderer objectRenderer;

    /// <summary>
    /// String, den wir beim Speichern des Objekts mit der Funktion Save
    /// verwenden können. 
    /// 
    /// Als Default wird der String "mesh" verwendet.
    /// </summary>
    private string description = "mesh";
    protected string Description
    {
        get { return this.description; }
        set { this.description = value;  }
    }

    /// <summary>
    /// Wir merken uns, ob wir bereits abgespeichert haben.
    /// </summary>
    private bool Saved { get; set; }

    /// <summary>
    /// Das polygonale Netz wird in der abgeleiteten Klasse
    /// mit Hilfe der Funktion Create erzeugt!
    /// </summary>
    protected abstract void Create();

    protected virtual void Awake() { }

    /// <summary>
    /// MeshRenderer und MeshFilter zuordnen und Netz erzeugen.
    /// </summary>
    protected virtual void Start()
    {
        this.objectFilter = gameObject.GetComponent<MeshFilter>();
        this.objectRenderer = gameObject.GetComponent<MeshRenderer>();

        // Polygonales Netz erzeugen
        Create();

        Saved = false;
    }

    /// <summary>
    /// Mit "M" wird das Dreieck in einer Datei in Assets/Editor gespeichert.
    /// </summary>
    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
            Save();
    }

    /// <summary>
    /// Mit Hilfe der Klasse AssetDatabase speichern wir das erstellte Dreieck ab.
    /// Die Dateien werden im Verzeichnis Assets/Meshes abgelegt. Als Dateiname
    /// wird der Text in der Property Description verwendet plus eine zufällige Zahl.
    /// Mit der Zufallszahl wird verhindert, dass wir eine Datei überschreiben.
    /// 
    /// Pro Ausführung der Anwendung erhalten wir so eine neue Datei. Während der 
    /// Anwendung kann das Asset nur einmal abgespeichert werden.
    /// </summary>
    protected virtual void Save()
    {
        string folder = "Assets/Meshes";

        if (!Saved)
        {
            if (AssetDatabase.IsValidFolder(folder))
            {
                string name = folder + "/" + Description + Random.Range(1, int.MaxValue).ToString() + ".asset";
                AssetDatabase.CreateAsset(this.objectFilter.sharedMesh, name);
                Debug.Log("Netz in der Datei " + name + " abgespeichert!");
                Saved = true;
            }
            else
                Debug.LogError("Das verwendete Verzeichnis für das Abspeichern von Assets ist ungültig!");
        }
        else
        {
            Debug.Log("Das Netz wurde bereits abgespeichert!");
        }
    }
}
