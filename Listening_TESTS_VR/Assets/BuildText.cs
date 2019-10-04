using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
public class BuildText : MonoBehaviour
{
    Mesh mesh;
    BoxCollider box;
    Vector3[] vertices;
    int[] triangles;

    // Grid Settings 
    public float cellSize;
    public Vector3 gridOffset;





    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        box = GetComponent<BoxCollider>();

    }


    private void Start()
    {



    }

    private void Update()
    {
        UpdateMesh();
        MakeDiscreteProcedualGrid();
        UpdateCollider();

    }

    private void UpdateCollider()
    {
        box.size = new Vector3(cellSize, cellSize, cellSize);
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void MakeDiscreteProcedualGrid()
    {
        // set array size 
        vertices = new Vector3[4];
        triangles = new int[6];

        // set vertex offset
        float vertexOffset = cellSize * 0.5f;

        // populate the vertices and triangles arrays 
        vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset);
        vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset);
        vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset);
        vertices[3] = new Vector3(vertexOffset, 0, vertexOffset);

        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;




    }
}
