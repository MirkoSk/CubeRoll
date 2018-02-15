using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Allows for Nested Prefabs. The script stores a reference to a prefab and displays it's Meshes in EditMode.
/// During PlayMode, the linked prefab gets instantiated into the scene.
/// 
/// Author: Original script by Nicholas Francis (http://framebunker.com/blog/poor-mans-nested-prefabs/), modified for CubeRoll by Mirko Skroch
/// </summary>
[ExecuteInEditMode]
public class PrefabInstance : MonoBehaviour
{
    #region Structs
    // Struct of all components. Used for edit-time visualization and gizmo drawing
    public struct Thingy
    {
        public Mesh mesh;
        public Matrix4x4 matrix;
        public List<Material> materials;
    }
    #endregion



    #region Variable Declarations
    [SerializeField] protected GameObject prefab;

    protected GameObject prefabInstance;
    List<Thingy> things = new List<Thingy>();
    #endregion



    #region Unity Event Functions (Play Mode)
    private void OnEnable()
    {
        if (Application.isPlaying) SpawnPrefab();
    }

    private void OnDisable()
    {
        if (Application.isPlaying) DespawnPrefab();
    }
    #endregion



    #region Unity Event Functions (Edit Mode)
#if UNITY_EDITOR
    //Editor-time-only update: Draw the meshes so we can see the objects in the scene view
    private void Update()
    {
        if (EditorApplication.isPlaying) return;

        Matrix4x4 mat = transform.localToWorldMatrix;
        foreach (Thingy t in things)
            for (int i = 0; i < t.materials.Count; i++)
                Graphics.DrawMesh(t.mesh, mat * t.matrix, t.materials[i], gameObject.layer, null, i);
    }

    void OnValidate()
    {
        things.Clear();
        if (enabled)
            Rebuild(prefab, Matrix4x4.identity);
    }

    // Picking logic: Since we don't have gizmos.drawmesh, draw a bounding cube around each thingy
    protected void OnDrawGizmos() { DrawGizmos(new Color(0, 0, 0, 0)); }
    protected void OnDrawGizmosSelected() { DrawGizmos(new Color(0, 0, 1, .2f)); }
    protected void DrawGizmos(Color col)
    {
        if (EditorApplication.isPlaying)
            return;
        Gizmos.color = col;
        Matrix4x4 mat = transform.localToWorldMatrix;
        foreach (Thingy t in things)
        {
            Gizmos.matrix = mat * t.matrix;
            Gizmos.DrawCube(t.mesh.bounds.center, t.mesh.bounds.size);
        }
    }
#endif
    #endregion



    #region Protected Functions
    virtual protected void SpawnPrefab()
    {
        if (Application.isPlaying)
        {
            prefabInstance = Instantiate(prefab, transform.position, transform.rotation, transform);
        }
    }

    virtual protected void DespawnPrefab()
    {
        if (Application.isPlaying)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    protected void Rebuild(GameObject source, Matrix4x4 inMatrix)
    {
        if (!source)
            return;

        Matrix4x4 baseMat = inMatrix * Matrix4x4.TRS(-source.transform.position, Quaternion.identity, Vector3.one);

        foreach (MeshRenderer mr in source.GetComponentsInChildren(typeof(MeshRenderer), true))
        {
            if (mr.enabled)
            {
                things.Add(new Thingy()
                {
                    mesh = mr.GetComponent<MeshFilter>().sharedMesh,
                    matrix = baseMat * mr.transform.localToWorldMatrix,
                    materials = new List<Material>(mr.sharedMaterials)
                });
            }
        }

        foreach (PrefabInstance pi in source.GetComponentsInChildren(typeof(PrefabInstance), true))
        {
            if (pi.enabled && pi.gameObject.activeSelf)
                Rebuild(pi.prefab, baseMat * pi.transform.localToWorldMatrix);
        }
    }
    #endregion
}