using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProceduralMemoItem : MonoBehaviour {

	public Material material;
    public Vector3 downDirection;
    public Vector3 leftDirection;
    public Vector2 gridItemSize;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        
        meshBuilder.AddQuad(Vector3.zero, 
            gridItemSize.x * leftDirection, 
            gridItemSize.y * downDirection, 
            Facing.Front);
        meshBuilder.UVs.Add(new Vector2(0f,0f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0f));
        meshBuilder.UVs.Add(new Vector2(0f,0.5f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0.5f));

        meshBuilder.AddQuad(Vector3.zero, 
            gridItemSize.x * leftDirection, 
            gridItemSize.y * downDirection,
            Facing.Back);
        meshBuilder.UVs.Add(new Vector2(0.5f,0.5f));
        meshBuilder.UVs.Add(new Vector2(1f,0.5f));
        meshBuilder.UVs.Add(new Vector2(0.5f,1f));
        meshBuilder.UVs.Add(new Vector2(1f,1f));

		Mesh mesh = meshBuilder.CreateMesh ();

		MeshFilter filter = gameObject.GetComponent<MeshFilter> ();
		if (filter == null)
			filter = gameObject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;

		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer> ();
		if (renderer == null)
			renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;
	}

    public static GameObject Generate(Vector2 gridItemSize, Vector3 left, Vector3 down, Material material)
    {
        GameObject go = new GameObject("MemoItem");
        ProceduralMemoItem memoItem = go.AddComponent<ProceduralMemoItem>();

        memoItem.material = material;
        memoItem.gridItemSize = gridItemSize;
        memoItem.downDirection = down;
        memoItem.leftDirection = left;

        memoItem.Generate();

        return go;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ProceduralMemoItem))]
public class ProceduralMemoItemEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    
        ProceduralMemoItem editorObj = target as ProceduralMemoItem;
    
        if (editorObj == null) return;

        if (GUILayout.Button("Generate")) {
            editorObj.Generate();
        }
    }

}
#endif