using UnityEngine;

public class TeleportBoxTrigger : MonoBehaviour
{
    public GameObject xrOrigin;               // XR Origin ����
    public Vector3 teleportLocation;          // �� �ڽ��� ������ �̵��� ��ġ

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ XR Origin�� ��� �� �̵�
        if (other.gameObject == xrOrigin)
        {
            xrOrigin.transform.position = teleportLocation;
            Debug.Log($"Teleported XR Origin to {teleportLocation} by {gameObject.name}");

            // �ڽ� ���� (���� ����)
            Destroy(gameObject);
        }
    }
}
