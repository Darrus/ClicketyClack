/** 
*  @file    MeshBuilder2.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief   Contain class MeshBuilder2
*
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
*  @brief A class To Create mesh using Data pass in
*/
public class MeshBuilder2
{
    public List<Vector3> Vertices { get { return vertices; } } ///< List of Vector3 Vertices points of the mesh
    public List<Vector2> UVs { get { return uv; } }  ///< List of Vector2 UVs points 


    List<Vector3> vertices = new List<Vector3>(); ///< List of Vector3 Vertices points of the mesh
    List<Vector2> uv = new List<Vector2>(); ///< List of Vector2 UVs points 
    List<int> indices = new List<int>(); ///< List of indices order


    /**
    *  @brief Setting the indices to Add Triangle
    *  
    *  @param int index0, index of the element of the list
    *  
    *  @param int index1, index of the element of the list
    *  
    *  @param int index2, index of the element of the list
    *  
    *  @return null
    */
    public void AddTriangle(int index0, int index1, int index2)
    {
        indices.Add(index0);
        indices.Add(index1);
        indices.Add(index2);
    }

    /**
   *  @brief Create a Mesh using Vertices, uv, indices data
   *  
   *  @return Mesh mesh
   */
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
