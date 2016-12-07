using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
[CanEditMultipleObjects]
public class TileMapEditor : Editor {


    void OnEnable()
    {

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSceneGUI()
    {
        var t = (target as TileMap);
        t.UpdateMesh();
    }
}
