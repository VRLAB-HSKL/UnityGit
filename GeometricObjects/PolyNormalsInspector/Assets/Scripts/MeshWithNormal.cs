using UnityEngine;
using System.Collections;

/// <summary>
/// Ein einfaches polygonales Netz erzeugen und
/// Normalenvektoren zuweisen.
/// 
/// Verwendung: ein leeres GameObject im Editor erzeugen
/// und dieses Skript diesem GameObject hinzufügen.
/// Bei der Ausführung der Anwendung wird das polygonale Netz
/// erstellt und dargestellt.
/// Normalenvektoren werden Eckpunkten zugewiesen
/// und im Inspector angegeben.
/// 
/// Wir stellen mit RequireComponent sicher, dass wir
/// einen MeshFilter und einen MeshRenderer haben!
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class MeshWithNormal : MonoBehaviour
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
    /// GameObject, das den Eckpunkt Nr. 3 repräsentiert
    /// </summary>
    [Tooltip("Vierter Eckpunkt")]
    public GameObject three;

    /// <summary>
    /// Normalenvektor
    /// 
    /// Als Default verwenden wir den ersten kanonischen Einheitsvektor.
    /// </summary>
    [Tooltip("Richtung des Normalenvektors eingeben (Normierung nicht notwendig!")]
    public Vector3 normalVector = new Vector3(1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Wir erzeugen eine Material-Komponente,
    /// so dass wir im Editor dem Netz ein Material zuweisen können.
    /// </summary>
    [Tooltip("Material für die grafische Ausgabe des polygonalen Netzes")]/// 
    public Material meshMaterial;
    /// <summary>
    /// Wir benötigen eine Instanz der Klasse MeshFilter
    /// </summary>
    MeshFilter objMeshFilter;
    /// <summary>
    /// Mit Hilfe einer Instanz von MeshRenderer stellen wir das Dreieck dar.
    /// </summary>
    MeshRenderer objRenderer;

    /// <summary>
    /// Verbindung zu MeshFilter und MeshRenderer herstellen und dann
    /// das Netz mit Create erzeugen.
    /// </summary>
    void Start () 
	{
        // Dem leeren GameObject, dem wir dieses Skript zuweisen fügen
        // wir eine MeshFilter-Komponente und auch einen MeshRenderer hinzu
        this.objMeshFilter = gameObject.GetComponent<MeshFilter>();
        this.objRenderer = gameObject.GetComponent<MeshRenderer>();

        normalVector.Normalize();
        Create();
	}

    /// <summary>
    /// Geometrie, Topologie und Normalenvektoren besetzen und an einen Instanz
    /// der Klasse Mesh übergeben.
    /// </summary>
    private void Create()
    {
        const int numberOfVertices = 4;
        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector3[] normals = new Vector3[numberOfVertices];
        int[] topology = new int[6];
        // Instanz der Klasse Mesh
        Mesh simpleMesh;

        // Eckpunkte
        vertices[0] = zero.transform.position;
        vertices[1] = one.transform.position;
        vertices[2] = two.transform.position;
        vertices[3] = three.transform.position;

        // Normalenvektoren (Normalen hängen an Eckpunkten!)
        // Alle vier Eckpunkte erhalten die gleiche Normale, da
        // wir ein planares Rechteck modellieren!
        for (int i = 0; i < numberOfVertices; i++)
            normals[i] = normalVector;

        // Topologie
        topology[0] = 0;
        topology[1] = 1;
        topology[2] = 2;

        topology[3] = 3;
        topology[4] = 2;
        topology[5] = 1;

        // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
        simpleMesh = new Mesh
        {
            vertices = vertices,
            normals = normals,
            triangles = topology
        };

        // Zuweisungen für die erzeugten Komponenten
        this.objMeshFilter.mesh = simpleMesh;
        this.objRenderer.material = meshMaterial;
    }
}
