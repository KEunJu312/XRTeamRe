using UnityEngine;

public class TeleportBoxTrigger : MonoBehaviour
{
    public GameObject xrOrigin;               // XR Origin 참조
    public Vector3 teleportLocation;          // 이 박스를 밟으면 이동할 위치

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 XR Origin인 경우 → 이동
        if (other.gameObject == xrOrigin)
        {
            xrOrigin.transform.position = teleportLocation;
            Debug.Log($"Teleported XR Origin to {teleportLocation} by {gameObject.name}");

            // 박스 제거 (재사용 방지)
            Destroy(gameObject);
        }
    }
}
