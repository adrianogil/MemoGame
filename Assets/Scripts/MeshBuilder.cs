using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Facing
{
    Front,
    Back,
    Both   
}

public class MeshBuilder {

    private List<Vector3> m_Vertices = new List<Vector3>();
    public List<Vector3> Vertices { get { return m_Vertices; }}

    private List<Vector3> m_Normals = new List<Vector3>();
    public List<Vector3> Normals { get { return m_Normals; }}

    private List<Vector2> m_UVs = new List<Vector2>();
    public List<Vector2> UVs { get { return m_UVs; }}

    private List<Vector4> m_Tangents = new List<Vector4>();
    public List<Vector4> Tangents { get { return m_Tangents; }}

    private List<int> m_Indices = new List<int>();

    public void AddTriangle(int index0, int index1, int index2)
    {
        m_Indices.Add (index0);
        m_Indices.Add (index1);
        m_Indices.Add (index2);
    }

    public void AddQuad(Vector3 pos, Vector3 v1, Vector3 v2, Facing facing = Facing.Both)
    {
        int t = m_Vertices.Count;

        m_Vertices.Add(pos);
        m_Vertices.Add(pos+v1);
        m_Vertices.Add(pos+v2);
        m_Vertices.Add(pos+v1+v2);

        if (facing == Facing.Back || facing == Facing.Both)
        {
            AddTriangle(t+0, t+1, t+2);
            AddTriangle(t+2, t+1, t+3);    
        }
        
        if (facing == Facing.Front || facing == Facing.Both)
        {
            AddTriangle(t+0, t+2, t+1);
            AddTriangle(t+2, t+3, t+1);
        }
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh ();

        mesh.vertices = m_Vertices.ToArray ();
        mesh.triangles = m_Indices.ToArray ();

        // Normals sao opcionais
        if (m_Normals.Count == m_Vertices.Count) {
            mesh.normals = m_Normals.ToArray();
        }

        // UVs sao opcionais
        if (m_UVs.Count == m_Vertices.Count) {
            mesh.uv = m_UVs.ToArray();
        }

        // Tangents sao opcionais
        if (m_Tangents.Count == m_Vertices.Count) {
            mesh.tangents = m_Tangents.ToArray();
        }

        mesh.RecalculateBounds ();

        return mesh;
    }

}
