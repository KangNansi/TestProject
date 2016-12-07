﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
[CanEditMultipleObjects]
public class TileMapEditor : Editor {
    public Vector2 scrollPosition;
    int tx=0;
    int ty=0;
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
        Rect texRect = new Rect(0, 0, t.texture.width, t.texture.height);
        if (GUILayout.Button("Generate"))
        {
            (target as TileMap).CreateMesh();
        }
        if (GUILayout.Button("Randomize"))
        {
            (target as TileMap).randomizeTile();
        }
        if (GUILayout.Button("Fill"))
        {
            (target as TileMap).fillTile(tx, (t.texture.height/t.tile_height-1)-ty);
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUIStyle.none, GUILayout.Height(200));

        //Events
        if (Event.current.type == EventType.MouseDown && texRect.Contains(Event.current.mousePosition))
        {
            tx = (int)Event.current.mousePosition.x/t.tile_width;
            ty = (int)Event.current.mousePosition.y/t.tile_height;
            this.Repaint();
        }

        GUILayout.Label(t.texture, GUIStyle.none, GUILayout.Width(t.texture.width), GUILayout.Height(t.texture.height));
        EditorGUI.DrawRect(new Rect(tx * t.tile_width, ty * t.tile_height, t.tile_width, t.tile_height)
            , new Color(1, 1, 1, 0.2f));
        
        //EditorGUI.DrawPreviewTexture(new Rect(0, 0, t.texture.width, t.texture.height), t.texture);
        EditorGUILayout.EndScrollView();
    }

    public void OnSceneGUI()
    {
        
    }
}