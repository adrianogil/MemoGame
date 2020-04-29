using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum MemoFace
{
    Front,
    Back
}

public interface IMemoItemActivate
{
    bool IsGameEnded();

    void Activate(ProceduralMemoItem memoItem, MemoFace facing);
}

[System.Serializable]
public class MemoItemData
{
    public Material material;
    public Vector3 downDirection;
    public Vector3 leftDirection;
    public Vector2 gridItemSize;
    public string itemName;
    public int itemX;
    public int itemY;
    public int itemNumber;
}

public class ProceduralMemoItem : MonoBehaviour {

	public Material material;
    public Vector3 downDirection;
    public Vector3 leftDirection;
    public Vector2 gridItemSize;

    public MemoItemData itemData;

    public static IMemoItemActivate itemActivate = null;

    public MemoFace facing = MemoFace.Back;

    private MeshCollider meshCollider;

	// Use this for initialization
	void Start () {

	}

    public void SwapFacingDelayed(float time)
    {
        Invoke("SwapFacing", time);
    }

    public void SwapFacing()
    {
        transform.DOPause();
        if (facing == MemoFace.Back)
            transform.DORotate (Vector3.up*180f, 1f);
        else
            transform.DORotate (Vector3.zero, 1f);
        facing = facing == MemoFace.Back? MemoFace.Front : MemoFace.Back;
    }

    public bool Matches(ProceduralMemoItem item)
    {
        return itemData.itemNumber == item.itemData.itemNumber;
    }

	// Update is called once per frame
	void Update () {
	   	 if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (meshCollider.Raycast(ray, out hit, 100.0F) && facing == MemoFace.Back)
            {
                Debug.Log("GilLog - ProceduralMemoItem::Update - MeshCollider - " + gameObject.name);

				SwapFacing();

                if (itemActivate != null)
                {
                    itemActivate.Activate(this, facing);
                }
            }

        }
	}

    public void Generate()
    {
        if (itemData == null)
        {
            Debug.LogError("GilLog - ProceduralMemoItem::Generate - Null Data");
            return;
        }

        MeshBuilder meshBuilder = new MeshBuilder();

        Vector3 center = new Vector3(
                (0.5f) * itemData.gridItemSize.x,
                (0.5f) * itemData.gridItemSize.y,
                0f
            );

        meshBuilder.AddQuad(center,
            itemData.gridItemSize.x * itemData.leftDirection,
            itemData.gridItemSize.y * itemData.downDirection,
            Facing.Front);
        meshBuilder.UVs.Add(new Vector2(0f,0f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0f));
        meshBuilder.UVs.Add(new Vector2(0f,0.5f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0.5f));

        meshBuilder.AddQuad(center,
            itemData.gridItemSize.x * itemData.leftDirection,
            itemData.gridItemSize.y * itemData.downDirection,
            Facing.Back);
        meshBuilder.UVs.Add(new Vector2(0.5f,1f));
        meshBuilder.UVs.Add(new Vector2(1f,1f));
        meshBuilder.UVs.Add(new Vector2(0.5f,0.5f));
        meshBuilder.UVs.Add(new Vector2(1f,0.5f));

		Mesh mesh = meshBuilder.CreateMesh ();

		MeshFilter filter = gameObject.GetComponent<MeshFilter> ();
		if (filter == null)
			filter = gameObject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;

		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer> ();
		if (renderer == null)
			renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = itemData.material;

        SetupImage(renderer.material);

        meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
	}

    private void SetupImage(Material material)
    {
        material.mainTexture = ImageManager.Instance.images[itemData.itemNumber].texture;
    }

    public static GameObject Generate(MemoItemData itemData)
    {
        GameObject go = new GameObject("MemoItem");
        ProceduralMemoItem memoItem = go.AddComponent<ProceduralMemoItem>();
        memoItem.itemData = itemData;

        memoItem.Generate();

        return go;
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