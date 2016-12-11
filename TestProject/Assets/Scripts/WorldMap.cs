using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMap : MonoBehaviour {

	struct MapKey{
		public int x;
		public int y;
		public MapKey(int nx, int ny){
			x=nx;
			y=ny;
		}
	}


    public Texture texture;
    public int tileWidth;
    public int tileHeight;
	int mapSizeX=30;
	int mapSizeY=30;

	Dictionary<MapKey, TileMap> worldMap;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 getNormal()
    {
        return transform.rotation * Vector3.forward;
    }

    public void SetTile(int x, int y, int tx, int ty){
		MapKey mapKey= new MapKey(Mathf.FloorToInt(x/(float)mapSizeX),Mathf.FloorToInt(y/(float)mapSizeY));
		if (worldMap == null)
			worldMap = new Dictionary<MapKey, TileMap> ();
		if(!worldMap.ContainsKey(mapKey)){
			GameObject newmap = new GameObject(); 
			newmap.transform.SetParent(transform,false);
			newmap.transform.position = transform.position + new Vector3(mapKey.x*mapSizeX, mapKey.y*mapSizeY, 0);
			//newmap.hideFlags = HideFlags.HideInHierarchy;
			TileMap tmap = newmap.AddComponent<TileMap> ();
            tmap.tile_width = tileWidth;
            tmap.tile_height = tileHeight;
            tmap.setTexture(texture);
            tmap.size_x = mapSizeX;
            tmap.size_y = mapSizeY;
			tmap.CreateMap ();
			worldMap.Add(mapKey,tmap);
		}
        
		worldMap [mapKey].setTile ((x%mapSizeX+mapSizeX)%mapSizeX, (y % mapSizeY + mapSizeY) % mapSizeY, tx, ty);
	}

}
