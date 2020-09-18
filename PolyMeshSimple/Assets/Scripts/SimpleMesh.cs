using UnityEngine;
using System.Collections;

/// <summary>
/// Ein einfaches polygonales Netz erzeugen
/// 
/// Verwendung: ein leeres GameObject im Editor erzeugen
/// und dieses Skript diesem GameObject hinzufügen.
/// Bei der Ausführung der Anwendung wird das polygonale Netz
/// erstellt und dargestellt.
/// 
/// Anschließend können wir dem GameObject ein Material zuweisen.
/// 
/// Ein polygonales Netz in Unity wird als Instanz der Klasse Mesh
/// erzeugt. Dort wird die Geometrie in Form eines Arrays von Eckpunkten
/// und die Topologie als Array mit Indexeinträgen in Form einer 
/// Eckenliste definiert.
/// Normalenvektoren werden Eckpunkten zugewiesen oder können
/// mit Mesh::RecalculateNormals() aus den Eckpunkten und der Topologie
/// erzeugt.
/// 
/// Wir stellen mit RequireComponent sicher, dass wir ein MeshFilter
/// und MeshRenderer haben.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class SimpleMesh : MonoBehaviour {

    /// <summary>
    /// Wir erzeugen eine Material-Komponente,
    /// so dass wir im Editor dem Netz ein Material zuweisen können.
    /// </summary>
    public Material meshMaterial;

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
    private MeshFilter objMeshFilter;
    /// <summary>
    /// Mit Hilfe einer Instanz von MehRenderer stellen wir das Dreieck dar.
    /// </summary>
    private MeshRenderer objRenderer;

    /// <summary>
    /// Das Dreieck erzeugen
    /// </summary>
    private void Start () 
	{
        // Dem leeren GameObject, dem wir dieses Skript zuweisen fügen
        // wir eine MeshFilter-Komponente und auch einen MeshRenderer hinzu
        this.objMeshFilter = gameObject.GetComponent<MeshFilter>();
        this.objRenderer = gameObject.GetComponent<MeshRenderer>();
        CreateTriangle();
	}

    /// <summary>
    /// Wir speichern die Geometrie und die Topologie des Dreiecks ab
    /// und legen die Daten in eine Instanz der Klaasse Mesh.
    /// </summary>
    private void CreateTriangle()
    {
        // Wir stellen ein Dreieck dar.
        vertices = new Vector3[3];
        vertices[0] = new Vector3(-1.0f, 0.0f, 3.0f);
        vertices[1] = new Vector3(-1.0f, 0.5f, 3.0f);
        vertices[2] = new Vector3(0.0f, 0.0f, 2.0f);

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
}
