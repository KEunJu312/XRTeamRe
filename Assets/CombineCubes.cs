using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineCubes : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        // 부모 MeshFilter는 제외하고 자식만 개수 세기
        int childCount = 0;
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].gameObject != gameObject)
                childCount++;
        }

        CombineInstance[] combine = new CombineInstance[childCount];
        int combineIndex = 0;
        for (int i = 0; i < meshFilters.Length; i++)
        {
            // 부모는 제외
            if (meshFilters[i].gameObject == gameObject)
                continue;

            combine[combineIndex].mesh = meshFilters[i].sharedMesh;
            combine[combineIndex].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            combineIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.SetActive(true);
        // 충돌체 추가
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;

    }
}

