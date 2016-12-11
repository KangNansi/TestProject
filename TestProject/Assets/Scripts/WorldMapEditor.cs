using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldMap))]
[CanEditMultipleObjects]
public class WorldMapEditor : Editor {


	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		if (GUILayout.Button ("test"))
			(target as WorldMap).SetTile (50, 50, 0, 0);
	}
}
