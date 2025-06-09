using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject parentObject;
    public Material meshMaterial;

    void Start() // ¶Ç´Â Awake()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = parentObject.GetComponentsInChildren<MeshFilter>();
        List<CombineInstance> combine = new List<CombineInstance>();

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf == parentObject.GetComponent<MeshFilter>()) continue;
            CombineInstance ci = new CombineInstance();
            ci.mesh = mf.sharedMesh;
            ci.transform = mf.transform.localToWorldMatrix;
            combine.Add(ci);
        }

        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.position = parentObject.transform.position;
        combinedObject.transform.rotation = parentObject.transform.rotation;
        combinedObject.transform.localScale = parentObject.transform.localScale;

        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine.ToArray());
        combinedMeshFilter.sharedMesh = combinedMesh;
        combinedMeshRenderer.material = meshMaterial;

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.gameObject != combinedObject)
                mf.gameObject.SetActive(false);
        }
    }
}
