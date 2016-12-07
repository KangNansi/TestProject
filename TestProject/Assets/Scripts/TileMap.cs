using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour {
    public int size_x;
    public int size_y;
    public Texture texture;
    public class Tile
    {
        int x, y;

    }

    

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CreateMesh()
    {
        if (size_x < 1) size_x = 1;
        if (size_y < 1) size_y = 1;
        int nb_tiles = size_x * size_y;
        Vector3[] vertices = new Vector3[nb_tiles*4];
        int[] triangles = new int[nb_tiles*6];
        Vector2[] uv = new Vector2[nb_tiles*4];
        

        for(int i=0; i<nb_tiles; i++)
        {
            int quadIndex = i * 4;
            int triangleIndex = i * 6;
            //Set Tile in order : Left-Bottom, Left-Up, Right-Up, Right-Bottom
            //Setting each tile vertices
            vertices[quadIndex] = new Vector3(i%size_x, i/size_x, 0);
            vertices[quadIndex + 1] = new Vector3(i % size_x, i / size_x + 1, 0);
            vertices[quadIndex + 2] = new Vector3(i % size_x + 1, i / size_x + 1, 0);
            vertices[quadIndex + 3] = new Vector3(i % size_x + 1, i / size_x, 0);

            //Settings each tile uv coords
            uv[quadIndex] = new Vector2(0,0); 
            uv[quadIndex + 1] = new Vector2(0, 1);
            uv[quadIndex + 2] = new Vector2(1, 1);
            uv[quadIndex + 3] = new Vector2(1, 0);

            //Settings each tile triangles
            //First Triangle
            triangles[triangleIndex] = quadIndex;
            triangles[triangleIndex + 1] = quadIndex + 1;
            triangles[triangleIndex + 2] = quadIndex + 2;

            //Second Triangle
            triangles[triangleIndex + 3] = quadIndex;
            triangles[triangleIndex + 4] = quadIndex + 2;
            triangles[triangleIndex + 5] = quadIndex + 3;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer.sharedMaterial == null)
        {
            renderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
        }
        renderer.sharedMaterial.SetTexture("_MainTex",texture);
    }
}
