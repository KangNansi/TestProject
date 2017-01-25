using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProcTerrain))]
[CanEditMultipleObjects]
public class ProcTerrainEditor : Editor
{
    public Vector2 scrollPosition;
    int tx = 0;
    int ty = 0;

    public int brush_size;
    public float brush_opacity;


    int getInvertedY(int ty)
    {
        var t = (target as TileMap);
        return (t.texture.height / t.tile_height - 1) - ty;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var t = (target as ProcTerrain);
        if (GUILayout.Button("Generate"))
        {
            (target as ProcTerrain).CreateMesh();
        }


        EditorGUILayout.LabelField("Brush Size:");
        brush_size = EditorGUILayout.IntSlider(brush_size, 1, 150);
        EditorGUILayout.LabelField("Opacity:");
        brush_opacity = EditorGUILayout.Slider(brush_opacity, -1, 1);


        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI()
    {
        int ControlID = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(ControlID);
        switch (Event.current.type)
        {
            case EventType.MouseDrag:
            case EventType.MouseDown:
                if (Event.current.button == 0)
                {
                    GUIUtility.hotControl = ControlID;
                    draw();
                }
                break;
            case EventType.MouseUp:
                if (Event.current.button == 0)
                    GUIUtility.hotControl = 0;
                break;
            default:
                break;
        }



    }

    Vector3 getMousePoint()
    {
        ProcTerrain map = target as ProcTerrain;
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Plane plane = new Plane(map.getNormal(), map.transform.position);
        float distance;
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(ray, out hit))
            point = hit.point - map.transform.position;
        else
        {
            plane.Raycast(ray, out distance);
            point = ray.GetPoint(distance) - map.transform.position;
        }
        return Quaternion.Inverse(map.transform.rotation) * point;
    }

    void draw()
    {
        ProcTerrain map = target as ProcTerrain;

        Vector3 point = getMousePoint();
        map.modifyHeight(point, brush_size, brush_opacity);
    }
}

