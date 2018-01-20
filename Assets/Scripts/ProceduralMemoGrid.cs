using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProceduralMemoGrid : MonoBehaviour {

    public Material itemMaterial;
    public float cameraDistance = 5f;
    public float paddingXPercent = 0.1f;
    public float paddingYPercent = 0.1f;
    public int gridSizeX;
    public int gridSizeY;

	// Use this for initialization
	void Start () {
		Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate()
    {
        Vector3 gridPoint = Camera.main.transform.position + 
            cameraDistance * Camera.main.transform.forward;

        float scaledSize = 0.7f;

        float height = scaledSize * 2f * cameraDistance * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad);
        float width = height * (Screen.width * 1f/ Screen.height);

        Vector3 left = (-1f)*Camera.main.transform.right;
        Vector3 up = Camera.main.transform.up;

        Vector3 upperLeftPoint = gridPoint + 
            0.5f * width * left + 
            0.5f * height * up;

        Vector2 gridItemSize = new Vector2(width * (1f/gridSizeX),height * (1f/gridSizeY));

        upperLeftPoint.x += 0.5f * gridItemSize.x;
        upperLeftPoint.y -= 0.5f * gridItemSize.y;

        Vector2 gridItemInternalSize = Vector2.Scale(new Vector2(1f - paddingXPercent, 1f - paddingYPercent),
            gridItemSize);

        GameObject go;

        ProceduralMemoItem.itemActivate = new MemoItemLogic();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 gridItemPos = upperLeftPoint - x * gridItemSize.x * left - y * gridItemSize.y * up;
                go = ProceduralMemoItem.Generate(gridItemInternalSize,left,(-1f)*up, itemMaterial);
                
                go.transform.position = gridItemPos;
                go.name = go.name + "_" + x + "_" + y;
            }
        }

        Debug.Log("GilLog - ProceduralMemoGrid::Generate - height " + height + "  - width " + width + " ");        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ProceduralMemoGrid))]
public class ProceduralMemoGridEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    
        ProceduralMemoGrid editorObj = target as ProceduralMemoGrid;
    
        if (editorObj == null) return;

        if (GUILayout.Button("Generate"))
        {
            editorObj.Generate();
        }
    }
}
#endif


