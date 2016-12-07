using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
[CanEditMultipleObjects]
public class TileMapEditor : Editor {
    public Vector2 scrollPosition;

    void OnEnable()
    {

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (target as TileMap);
        if (GUILayout.Button("Generate"))
        {
            (target as TileMap).CreateMesh();
        }
        //GUILayout.BeginScrollView()
        scrollPosition = GUI.BeginScrollView(new Rect(0, 0, 200, 200), scrollPosition, new Rect(0, 0, t.texture.width, t.texture.height));
        
       // EditorGUILayout.RectField(new Rect(0, 0, t.texture.width, t.texture.height));
        //GUILayout.;
        GUI.DrawTexture(new Rect(0, 0, t.texture.width, t.texture.height), (target as TileMap).texture);
        //GUILayout.EndArea();
        GUI.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    public void OnSceneGUI()
    {
        
    }
}
