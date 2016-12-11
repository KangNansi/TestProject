using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMap : MonoBehaviour {

	struct MapKey{
		public readonly int x;
		public readonly int y;
		public MapKey(int nx, int ny){
			x=nx;
			y=ny;
		}
	}

	public TileMap originMap;
	int mapSizeX=30;
	int mapSizeY=30;

	Dictionary<MapKey, TileMap> worldMap;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTile(int x, int y, int tx, int ty){
		MapKey mapKey= new MapKey(x/mapSizeX,y/mapSizeY);
		if (worldMap == null)
			worldMap = new Dictionary<MapKey, TileMap> ();
		if(!worldMap.ContainsKey(mapKey)){
			GameObject newmap = new GameObject(); 
			newmap.transform.SetParent(transform,false);
			newmap.transform.position = new Vector3(mapKey.x, mapKey.y, 0);
			newmap.hideFlags = HideFlags.HideInHierarchy;
			TileMap tmap = newmap.AddComponent<TileMap> ();
			tmap = Instantiate(originMap);
			tmap.CreateMap ();
			worldMap.Add(mapKey,tmap);
		}
		worldMap [mapKey].setTile (x%mapSizeX, y%mapSizeY, tx, ty);
	}

}
