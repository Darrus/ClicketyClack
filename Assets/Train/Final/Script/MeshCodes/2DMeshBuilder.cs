using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshBuilder2
{
    public List<Vector3> Vertices { get { return vertices; } }
    public List<Vector2> UVs { get { return uv; } }


    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uv = new List<Vector2>();
    List<int> indices = new List<int>();

    public void AddTriangle(int index0, int index1, int index2)
    {
        indices.Add(index0);
        indices.Add(index1);
        indices.Add(index2);
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();

        if(indices.Count > 0)
            mesh.triangles = indices.ToArray();

        if (uv.Count == vertices.Count)
        {
            mesh.uv = uv.ToArray();
        }
        mesh.RecalculateBounds();

        return mesh;
    }
}
