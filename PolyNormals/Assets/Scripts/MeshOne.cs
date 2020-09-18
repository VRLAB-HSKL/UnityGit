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
/// Normalenvektoren werden Eckpunkten zugewiesen!
/// 
/// Wir stellen mit RequireComponent sicher, dass wir
/// einen MeshFilter und einen MeshRenderer haben!
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class MeshOne : MonoBehaviour
{
    /// <summary>
    /// Wir erzeugen eine Material-Komponente,
    /// so dass wir im Editor dem Netz ein Material zuweisen können.
    /// </summary>
    public Material meshMaterial;
    /// <summary>
    /// Wir benötigen eine Instanz der Klasse MeshFilter
    /// </summary>
    private MeshFilter objMeshFilter;
    /// <summary>
    /// Mit Hilfe einer Instanz von MeshRenderer stellen wir das Dreieck dar.
    /// </summary>
    private MeshRenderer objRenderer;

    /// <summary>
    /// Verbindung zu MeshFilter und MeshRenderer herstellen und dann
    /// das Netz mit Create erzeugen.
    /// </summary>
    private void Start () 
	{
        // Dem leeren GameObject, dem wir dieses Skript zuweisen fügen
        // wir eine MeshFilter-Komponente und auch einen MeshRenderer hinzu
        this.objMeshFilter = gameObject.GetComponent<MeshFilter>();
        this.objRenderer = gameObject.GetComponent<MeshRenderer>();

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

        float yMin = 0.0f;
        float yMax = 0.5f;

        // Eckpunkte
        vertices[0] = new Vector3(-1.0f, yMin, -1.0f);
        vertices[1] = new Vector3(-1.0f, yMax, -1.0f);
        vertices[2] = new Vector3(0.0f, yMin, 0.0f);
        vertices[3] = new Vector3(0.0f, yMax, 0.0f);

        // Normalenvektoren (Normalen hängen an Eckpunkten!)
        // Alle vier Eckpunkte erhalten die gleiche Normale, da
        // wir ein planares Rechteck modellieren!
        Vector3 norm = new Vector3(1.0f, 0.0f, -1.0f);
        norm.Normalize();

        for (int i = 0; i < numberOfVertices; i++)
            normals[i] = norm;

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
