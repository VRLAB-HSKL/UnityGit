using UnityEngine;
using System.Collections;
using VRKL.MBU;

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

public class SimpleMesh : PolyMesh 
{
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
    /// Wir speichern die Geometrie und die Topologie des Dreiecks ab
    /// und legen die Daten in eine Instanz der Klaasse Mesh.
    /// </summary>
    protected override void Create()
    {
        // Wir stellen ein Dreieck dar.
        vertices = new Vector3[3];
        vertices[0] = new Vector3(-1.0f, 0.0f, 3.0f);
        vertices[1] = new Vector3(-1.0f, 0.5f, 3.0f);
        vertices[2] = new Vector3(0.0f, 0.0f, 2.0f);

        // Die Einträge in der Topologie beziehen sich auf 
        // die Indizes der Eckpunkte.
        // Die Durchlaufrichtung der Indices ist wichtig, da sonst
        // bei Backface Culling die Dreiecke nicht dargestellt werden.
        // Unity definiert ein Frontface als ein Polygon, das 
        // im Uhrzeigersinn durchlaufen wird!
        topology = new int[3];

        topology[0] = 0;
        topology[1] = 1;
        topology[2] = 2;

        Material[] materials = new Material[1];

        // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
        Mesh simpleMesh = new Mesh()
        {
            vertices = vertices,
            subMeshCount = 1,
            triangles = topology
        };

        // Wir nutzen nicht aus, dass wir pro Submesh ein eigenes
        // Material verwenden.
        materials[0] = meshMaterial;

        // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
        simpleMesh.RecalculateNormals();
        simpleMesh.RecalculateBounds();
        simpleMesh.OptimizeIndexBuffers();

        // Zuweisungen für die erzeugten Komponenten
        this.objMeshFilter.mesh = simpleMesh;
        this.objectRenderer.materials = materials;
    }
}
