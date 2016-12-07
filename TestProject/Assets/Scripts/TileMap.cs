using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class TileMap : MonoBehaviour {
    public int size_x;
    public int size_y;
    public class Tile
    {
        int x, y;

    }

    

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMesh();
	}

    public void UpdateMesh()
    {
        if (size_x < 1) size_x = 1;
        if (size_y < 1) size_y = 1;
        int nb_verts = (size_x + 1)*(size_y + 1);
        Vector3[] vertices = new Vector3[nb_verts];
        int[] triangles = new int[size_x*size_y*6];
        Vector2[] uv = new Vector2[nb_verts];
        Color[] colors = new Color[nb_verts];

        for(int i=0; i<nb_verts; i++)
        {
            vertices[i] = new Vector3(i%(size_x+1), i/(size_x+1), 0);
            uv[i] = new Vector2((i % (size_x + 1)) / (float)(size_x + 1), (i / (size_x + 1)) / (float)(size_y + 1));
            colors[i] = new Color(1, 1, 1);
        }

        for(int i=0;i<size_x;i++)
            for(int j=0;j<size_y;j++)
            {
                triangles[(i+j*size_x)*6] = i + (j*(size_x+1));
                triangles[(i + j * size_x) * 6 + 1] = i + ((j+1) * (size_x + 1));
                triangles[(i + j * size_x) * 6 + 2] = (i+1) + ((j+1) * (size_x + 1));

                triangles[(i + j * size_x) * 6 + 3] = i + (j * (size_x + 1));
                triangles[(i + j * size_x) * 6 + 4] = (i+1) + ((j + 1) * (size_x + 1));
                triangles[(i + j * size_x) * 6 + 5] = (i + 1) + (j * (size_x + 1));

            }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.colors = colors;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
