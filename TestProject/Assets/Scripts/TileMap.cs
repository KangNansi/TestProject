﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour {
    public int size_x;
    public int size_y;
    public Texture texture;
    public int tile_width;
    public int tile_height;
	[SerializeField]
    private float tileSizeX=1.0f;
	[SerializeField]
    private float tileSizeY=1.0f;
	[System.Serializable]
    struct Tile
    {
        public int x, y;
		public Tile(int tx, int ty){
			x=tx; y=ty;
		}
    }
	[SerializeField]
	private Tile[] map;


    

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setTile(int x, int y, int tx, int ty)
    {
		if (x < 0 || y < 0 || x >= size_x || y >= size_y)
			return;
		map [x+(y*size_x)].x = tx;
		map [x+(y*size_x)].y = ty;
        Vector2[] uv = GetComponent<MeshFilter>().sharedMesh.uv;
        int quadIndex = (x + (y * size_x)) * 4;
		uv[quadIndex] = new Vector2(tx * tileSizeX+0.0001f, ty * tileSizeY+0.0001f);
		uv[quadIndex+1] = new Vector2(tx * tileSizeX+0.0001f, (ty+1) * tileSizeY-0.0001f);
		uv[quadIndex+2] = new Vector2((tx+1) * tileSizeX-0.0001f, (ty+1) * tileSizeY-0.0001f);
		uv[quadIndex+3] = new Vector2((tx+1) * tileSizeX-0.0001f, ty * tileSizeY+0.0001f);
        GetComponent<MeshFilter>().sharedMesh.uv = uv;
    }

    public void randomizeTile()
    {
        int maxTx = texture.width/tile_width;
        int maxTy = texture.height / tile_height;
        for (int i = 0; i < size_x * size_y; i++)
            setTile(i % size_x, i / size_x, Random.Range(0, maxTx), Random.Range(0, maxTy));
    }

    public void fillTile(int tx, int ty)
    {
        for (int i = 0; i < size_x * size_y; i++)
            setTile(i % size_x, i / size_x, tx, ty);
    }

    public Vector3 getNormal()
    {
        return transform.rotation*Vector3.forward;
    }

    public void CreateMesh()
    {
        if (size_x < 1) size_x = 1;
        if (size_y < 1) size_y = 1;
        int nb_tiles = size_x * size_y;
        Vector3[] vertices = new Vector3[nb_tiles*4];
        int[] triangles = new int[nb_tiles*6];
        Vector2[] uv = new Vector2[nb_tiles*4];
        if (texture != null)
        {
            tileSizeX = tile_width / (float)texture.width;
            tileSizeY = tile_height / (float)texture.height;
        }

        for(int i=0; i<nb_tiles; i++)
        {
            int quadIndex = i * 4;
            int triangleIndex = i * 6;
            //Set Tile in order : Left-Bottom, Left-Up, Right-Up, Right-Bottom
            //Setting each tile vertices
            vertices[quadIndex] = new Vector3(i%size_x, i/size_x, 0);
            vertices[quadIndex + 1] = new Vector3(i % size_x, i / size_x + 1.01f, 0);
            vertices[quadIndex + 2] = new Vector3(i % size_x + 1.01f, i / size_x + 1.01f, 0);
            vertices[quadIndex + 3] = new Vector3(i % size_x + 1.01f, i / size_x, 0);

            //Settings each tile uv coords
            uv[quadIndex] = new Vector2(0,0); 
            uv[quadIndex + 1] = new Vector2(0, tileSizeY);
            uv[quadIndex + 2] = new Vector2(tileSizeX, tileSizeY);
            uv[quadIndex + 3] = new Vector2(tileSizeX, 0);

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

	public void CreateMap(){
		map = new Tile[size_x*size_y];
		CreateMesh ();
		for (int i = 0; i < size_x; i++)
			for (int j = 0; j < size_y; j++) {
				setTile (i, j, 0, 0);
			}
	}

	void RebuildMapToInsert(int x, int y, int tx, int ty){
		Tile[] old_map = map;
		int x_offset = (x < 0) ? -x : 0;
		int y_offset = (y < 0) ? -y : 0;
		int old_size_x = size_x;
		int old_size_y = size_y;

	}
}
