using UnityEngine;

public class GiftSuccenssTrigger : MonoBehaviour
{
    public GameObject keyPrefab;          // 떨어뜨릴 키 프리팹
    public Transform dropPosition;        // 키 시작 위치 (없으면 현재 위치 위)
    public AudioClip dropSound;           // 효과음
    public float keyDropHeight = 5.0f;    // 현재 위치 기준 생성 높이

    private AudioSource audioSource;

    void Start()
    {
        // 효과음 재생용 오디오 소스 확보
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("열쇠 상자입니다.");
            DropKey();
            PlaySound();
            Destroy(gameObject); // 선물 제거
        }
    }

    void DropKey()
    {
        if (keyPrefab == null)
        {
            Debug.LogWarning("Key 프리팹이 할당되지 않았습니다.");
            return;
        }

        // 위치 계산
        Vector3 spawnPos = dropPosition != null
            ? dropPosition.position
            : transform.position + Vector3.up * keyDropHeight;

        // 키 생성
        GameObject key = Instantiate(keyPrefab, spawnPos, Quaternion.identity);

        // 중력용 Rigidbody 추가
        if (!key.TryGetComponent(out Rigidbody rb))
        {
            rb = key.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.mass = 0.5f;

        // 회전 스크립트 추가 (또는 프리팹에 미리 넣어도 됨)
        if (!key.TryGetComponent(out KeySpinning _))
        {
            key.AddComponent<KeySpinning>();
        }

        // 파티클 자동 재생 (프리팹 내부의 ParticleSystem이 자동 시작 상태여야 함)
    }

    void PlaySound()
    {
        if (dropSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }
}
