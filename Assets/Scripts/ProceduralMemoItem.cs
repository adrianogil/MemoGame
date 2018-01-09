using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProceduralMemoItem : MonoBehaviour {

	public Material material;
    public Vector3 downDirection;
    public Vector3 leftDirection;
    public Vector2 gridItemSize;

    private MeshCollider meshCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	   	 if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (meshCollider.Raycast(ray, out hit, 100.0F))
            {
                Debug.Log("GilLog - ProceduralMemoItem::Update - MeshCollider - " + gameObject.name);
                // Vector3 focusPoint = transform.position;
                // focusPoint.x += (-0.5f) * gridItemSize.x;
                // focusPoint.y += (-0.5f) * gridItemSize.y;
//                transform.RotateAround(focusPoint, Vector3.up, 90);
				transform.DORotate (Vector3.up*180f, 1f);
            }
            
        }
	}

    public void Generate()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        
        Vector3 center = new Vector3(
                (0.5f) * gridItemSize.x,
                (0.5f) * gridItemSize.y,
                0f
            );

        meshBuilder.AddQuad(center, 
            gridItemSize.x * leftDirection, 
            gridItemSize.y * downDirection, 
            Facing.Front);
        meshBuilder.UVs.Add(new Vector2(0f,0f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0f));
        meshBuilder.UVs.Add(new Vector2(0f,0.5f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0.5f));

        meshBuilder.AddQuad(center, 
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

        meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
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