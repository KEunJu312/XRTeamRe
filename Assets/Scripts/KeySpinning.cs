using UnityEngine;

public class KeySpinning : MonoBehaviour
{
    public float rotationSpeed = 90f; // 초당 회전 각도

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
