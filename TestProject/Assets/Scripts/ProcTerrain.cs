using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ProcTerrain : MonoBehaviour {
    public int size_x = 30;
    public int size_y = 30;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void modifyHeight(Vector2 center, float range, float value)
    {
        Vector3[] vertices = GetComponent<MeshFilter>().sharedMesh.vertices;
        for (int i = 0; i < (size_x + 1) * (size_y + 1); i++)
        {
            float distance = Vector2.Distance(vertices[i], center);
            if (distance < range)
            {
                vertices[i].z += value * ((range - distance) / range);
            }
        }
        GetComponent<MeshFilter>().sharedMesh.vertices = vertices;
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        UpdateMesh();
    }

    public Vector3 getNormal()
    {
        return transform.rotation * Vector3.forward;
    }

    public void CreateMesh()
    {
        if (size_x < 1) size_x = 1;
        if (size_y < 1) size_y = 1;
        int nb_vertices = (size_x + 1) * (size_y + 1);
        Vector3[] vertices = new Vector3[nb_vertices];
        int[] triangles = new int[size_x*size_y*2*3];
        Vector2[] uv = new Vector2[nb_vertices];

        for (int i = 0; i < size_x + 1; i++)
        {
            for (int j = 0; j < size_y + 1; j++)
            {
                vertices[i + (j * (size_x + 1))] = new Vector3(i, j, 0);
                uv[i + (j * (size_x + 1))] = new Vector2(i / (float)(size_x + 1), j / (float)(size_y + 1));
            }
        }

        for(int i = 0; i < (size_x); i++)
        {
            for(int j = 0; j < (size_y); j++)
            {
                triangles[(i + (j * size_x)) * 6] = i + (j * (size_x + 1));
                triangles[(i + (j * size_x)) * 6+1] = (i+1) + (j * (size_x + 1));
                triangles[(i + (j * size_x)) * 6+2] = (i+1) + ((j+1) * (size_x + 1));
                triangles[(i + (j * size_x)) * 6+3] = i + (j * (size_x + 1));
                triangles[(i + (j * size_x)) * 6+4] = (i+1) + ((j+1) * (size_x + 1));
                triangles[(i + (j * size_x)) * 6+5] = i + ((j+1) * (size_x + 1));
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        GetComponent<MeshCollider>().sharedMesh = null;

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
        GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
    }

}

