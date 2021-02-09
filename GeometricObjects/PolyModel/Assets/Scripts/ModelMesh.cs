using UnityEngine;
using UnityEditor;

/// <summary>
/// Ein Dreick aus drei Punkten definieren.
/// 
/// Die drei Eckpunkte werden mit Hilfe von drei
/// GameObjects erzeugt, die in der Szene sichtbar
/// und veränderbar sind.
/// Wir stellen mit RequireComponent sicher, dass wir ein MeshFilter
/// und MeshRenderer haben./// 
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class ModelMesh : MonoBehaviour
{
    /// <summary>
    /// GameObject, das den Eckpunkt Nr. 0 repräsentiert
    /// </summary>
    [Tooltip("Erster Eckpunkt")]
    public GameObject zero;
    /// <summary>
    /// GameObject, das den Eckpunkt Nr. 1 repräsentiert
    /// </summary>
    [Tooltip("Zweiter Eckpunkt")]
    public GameObject one;
    /// <summary>
    /// GameObject, das den Eckpunkt Nr. 2 repräsentiert
    /// </summary>
    [Tooltip("Dritter Eckpunkt")]
    public GameObject two;
    /// <summary>
    /// Wir stellen im Editor ein, ob wir das erstellte
    /// Dreieck im Verzeichnis Assets/Editor abspeichern möchten.
    /// Dazu gibt es die Funktion saveTriangle.
    /// </summary>
    public bool saveTheTriangle = true;

    /// <summary>
    /// Feld für die Geometrie des polygonalen Netzes
    /// </summary>
    private Vector3[] vertices;
    /// <summary>
    /// Feld für die Topologie des polygonalen Netzes
    /// </summary>
    private int[] topology;
    /// <summary>
    /// Instanz der Klasse Mesh
    /// </summary>
    private Mesh simpleMesh;

    /// <summary>
    /// Wir benötigen eine Instanz der Klasse MeshFilter
    /// </summary>
    MeshFilter objMeshFilter;
    /// <summary>
    /// Mit Hilfe einer Instanz von MehRenderer stellen wir das Dreieck dar.
    /// </summary>
    MeshRenderer objRenderer;

    /// <summary>
    /// Wir erzeugen eine Material-Komponente,
    /// so dass wir im Editor dem Netz ein Material zuweisen können.
    /// </summary>
    public Material meshMaterial;

    /// <summary>
    /// Wir merken uns, ob wir bereits abgespeichert haben.
    /// </summary>
    private bool Saved { get; set; }

    /// <summary>
    /// Wir erstellen das Dreieck in der Funktion Start().
    /// </summary>
    void Start () 
	{
        // Dem leeren GameObject, dem wir dieses Skript zuweisen fügen
        // wir eine MeshFilter-Komponente und auch einen MeshRenderer hinzu
        this.objMeshFilter = gameObject.GetComponent<MeshFilter>();
        this.objRenderer = gameObject.GetComponent<MeshRenderer>();

        CreateTriangle();

        Saved = false;
        Debug.Log("Save the triangle using KeyCode S!");
    }	

    /// <summary>
    /// Mit "S" wird das Dreieck in einer Datei in Assets/Editor gespeichert.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SaveTriangle();
        }
    }

    /// <summary>
    /// Erzeugen eines Dreiecks.
    /// 
    /// Die Koordinaten der Eckpunkte werden
    /// durch drei GameObjects definiert, die
    /// im Inspektor zugewiesen werden.
    /// </summary>
    private void CreateTriangle()
    {
        vertices = new Vector3[3];
        vertices[0] = zero.transform.position;
        vertices[1] = one.transform.position;
        vertices[2] = two.transform.position;

        // Die Einträge in der Topologie beziehen sich auf 
        // die Indizes der Eckpunkte..
        // Die Durchlaufrichtung der Indices ist wichtig, da sonst
        // bei Backface Culling die Dreiecke nicht dargestellt werden.
        // Unity definiert ein Frontface als ein Polygon, das 
        // im Uhrzeigersinn durchlaufen wird!
        topology = new int[3];

        topology[0] = 0;
        topology[1] = 1;
        topology[2] = 2;

        // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
        simpleMesh = new Mesh();
        simpleMesh.vertices = vertices;
        simpleMesh.triangles = topology;

        // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
        simpleMesh.RecalculateNormals();
        simpleMesh.RecalculateBounds();

        // Zuweisungen für die erzeugten Komponenten
        this.objMeshFilter.mesh = simpleMesh;
        this.objRenderer.material = meshMaterial;
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
    protected virtual void SaveTriangle()
    {
        string folder = "Assets/Meshes";

        if (!Saved)
        {
            if (AssetDatabase.IsValidFolder(folder))
            {
                string name = folder + "/" + Random.Range(1, int.MaxValue).ToString() + ".asset";
                AssetDatabase.CreateAsset(this.objMeshFilter.sharedMesh, name);
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
