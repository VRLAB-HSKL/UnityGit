using UnityEngine;
using UnityEditor;

/// <summary>
/// Klasse für die Erzeugung eines Tetraeders während der Laufzeit.
/// 
/// Die vier Eckpunkte des Tetraeders werden wie in Blinn
/// und Brill/Bender in den Hexaeder platziert.
/// </summary>

public class Tetraeder : PolyMesh
{
    /// <summary>
    /// Die Beschreibung setzen, damit wir das Netz mit dieser
    /// Beschreibung abspeichern können.
    /// 
    /// Wir verwenden den Text "tetraeder".
    /// </summary>
    protected override void Awake()
    {
        Description = "tetraeder";
    }
    /// <summary>
    /// Wir erzeugen für jedes Face des Tetraeders ein SubMesh.
    /// Auch das Material muss anschließend für jedes SubMesh erzeugt
    /// und zugewiesen werden. Das könnte verwendet werden um
    /// für jedes Face ein eigenes Material zu verwenden.
    /// </summary>
    protected override void Create()
    {
        const int numberOfVertices = 4;
        const int numberOfSubMeshes = 4;
        Vector3[] vertices = new Vector3[numberOfVertices];
        int[][] topology = new int[numberOfSubMeshes][];
        Material[] materials = new Material[numberOfSubMeshes];

        vertices[0] = new Vector3(1.0f,  1.0f,  1.0f);
        vertices[1] = new Vector3(1.0f, -1.0f, -1.0f);
        vertices[2] = new Vector3(-1.0f,-1.0f,  1.0f);
        vertices[3] = new Vector3(-1.0f, 1.0f, -1.0f);

        // Die Einträge in der Topologie beziehen sich auf 
        // die Indizes der Eckpunkte.
        topology[0] = new int[3] { 0, 3, 2 };
        topology[1] = new int[3] { 0, 2, 1 };
        topology[2] = new int[3] { 1, 2, 3 };
        topology[3] = new int[3] { 0, 1, 3 };

        // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
        // Es wäre möglich weniger als vier SubMeshes zu erzeugen,
        // solange wir keine Dreiecke in einem Submesh haben, die eine
        // gemeinsame Kante aufweisen!
        Mesh simpleMesh = new Mesh()
        {
            vertices = vertices,
            subMeshCount = numberOfSubMeshes
        };
        // Wir nutzen nicht aus, dass wir pro Submesh ein eigenes
        // Material verwenden.
        for (int i = 0; i < numberOfSubMeshes; i++)
        {
            simpleMesh.SetTriangles(topology[i], i);
            materials[i] = meshMaterial;
        }

        // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
        simpleMesh.RecalculateNormals();
        simpleMesh.RecalculateBounds();

        // Zuweisungen für die erzeugten Komponenten
        this.objectFilter.mesh = simpleMesh;
        this.objectRenderer.materials = materials;
    }
}
