using UnityEngine;

/// <summary>
/// Erzeugen eines einfachen polygonalen Netzes.
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
/// Diese Klasse erzeugt einen Flat Shading Effekt, die Kante zwischen
/// den beiden Rechtecken ist sichtbar. Diese Kante ist doppelt vorhanden,
/// die Topologie des erzeugten Netzes ist entsprechend verändert.
/// 
/// Wir stellen mit RequireComponent sicher, dass wir ein MeshFilter
/// und MeshRenderer haben.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class FlatMesh : MonoBehaviour
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
    /// Mit Hilfe einer Instanz von MehRenderer stellen wir das Dreieck dar.
    /// </summary>
    private MeshRenderer objRenderer;

    /// <summary>
    /// Verbindung zu MeshFilter und Renderer herstellen und
    /// die Dreiecke erzeugen.
    /// </summary>
    private void Start () 
	{
        // Dem leeren GameObject, dem wir dieses Skript zuweisen fügen
        // wir eine MeshFilter-Komponente und auch einen MeshRenderer hinzu
        this.objMeshFilter = gameObject.GetComponent<MeshFilter>();
        this.objRenderer = gameObject.GetComponent<MeshRenderer>();
        CreateTriangles();
	}

    /// <summary>
    /// Wir erzeugen die Dreiecke und übergeben sie an eine Instanz von Mesh.
    /// </summary>
    private void CreateTriangles()
    {
        // Feld für die Geometrie des polygonalen Netzes
        Vector3[] vertices;
        // Feld für die Topologie des polygonalen Netzes
        int[] topology;
        // Instanz der Klasse Mesh
        Mesh simpleMesh;

        float yMin = 0.0f;
        float yMax = 0.5f;

        vertices = new Vector3[8];
        vertices[0] = new Vector3(-1.0f, yMin, 3.0f);
        vertices[1] = new Vector3(-1.0f, yMax, 3.0f);
        vertices[2] = new Vector3(0.0f, yMin, 2.0f);
        vertices[3] = new Vector3(0.0f, yMax, 2.0f);
        vertices[4] = new Vector3(0.0f, yMin, 2.0f);
        vertices[5] = new Vector3(0.0f, yMax, 2.0f);
        vertices[6] = new Vector3(1.0f, yMin, 3.0f);
        vertices[7] = new Vector3(1.0f, yMax, 3.0f);

        // Pro Dreieck verwenden wir 3 Einträge, also ist das Feld
        // topology von der Länge 12.
        topology = new int[12];

        topology[0] = 0;
        topology[1] = 1;
        topology[2] = 2;

        topology[3] = 1;
        topology[4] = 3;
        topology[5] = 2;

        topology[6] = 4;
        topology[7] = 5;
        topology[8] = 6;

        topology[9] = 5;
        topology[10] = 7;
        topology[11] = 6;

        // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
        simpleMesh = new Mesh
        {
            vertices = vertices,
            triangles = topology
        };

        // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
        simpleMesh.RecalculateNormals();
        simpleMesh.RecalculateBounds();

        // Zuweisungen für die erzeugten Komponenten
        this.objMeshFilter.mesh = simpleMesh;
        this.objRenderer.material = meshMaterial;
    }
}
