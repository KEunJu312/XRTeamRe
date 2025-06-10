using UnityEngine;

public class GiftSuccenssTrigger : MonoBehaviour
{
    public GameObject keyPrefab;          // ����߸� Ű ������
    public Transform dropPosition;        // Ű ���� ��ġ (������ ���� ��ġ ��)
    public AudioClip dropSound;           // ȿ����
    public float keyDropHeight = 5.0f;    // ���� ��ġ ���� ���� ����

    private AudioSource audioSource;

    void Start()
    {
        // ȿ���� ����� ����� �ҽ� Ȯ��
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("���� �����Դϴ�.");
            DropKey();
            PlaySound();
            Destroy(gameObject); // ���� ����
        }
    }

    void DropKey()
    {
        if (keyPrefab == null)
        {
            Debug.LogWarning("Key �������� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ��ġ ���
        Vector3 spawnPos = dropPosition != null
            ? dropPosition.position
            : transform.position + Vector3.up * keyDropHeight;

        // Ű ����
        GameObject key = Instantiate(keyPrefab, spawnPos, Quaternion.identity);

        // �߷¿� Rigidbody �߰�
        if (!key.TryGetComponent(out Rigidbody rb))
        {
            rb = key.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.mass = 0.5f;

        // ȸ�� ��ũ��Ʈ �߰� (�Ǵ� �����տ� �̸� �־ ��)
        if (!key.TryGetComponent(out KeySpinning _))
        {
            key.AddComponent<KeySpinning>();
        }

        // ��ƼŬ �ڵ� ��� (������ ������ ParticleSystem�� �ڵ� ���� ���¿��� ��)
    }

    void PlaySound()
    {
        if (dropSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }
}
