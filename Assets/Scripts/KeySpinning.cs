using UnityEngine;

public class KeySpinning : MonoBehaviour
{
    public float rotationSpeed = 90f; // �ʴ� ȸ�� ����

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
