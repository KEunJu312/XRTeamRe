using UnityEngine;

public class VRFootstep : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;
    public float speedThreshold = 0.1f;

    private AudioSource audioSource;
    private CharacterController characterController;
    private float stepTimer = 0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController != null && characterController.isGrounded && characterController.velocity.magnitude > speedThreshold)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length == 0) return;
        int n = Random.Range(0, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[n]);
    }
}

