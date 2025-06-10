using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;
        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;
        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        [Header("Video Settings")]
        public VideoPlayer videoPlayer;
        public RawImage videoScreen; // Canvas의 RawImage

        void Start()
        {
            asource = GetComponent<AudioSource>();
            // 초기 상태에서 videoScreen.enabled = false; 제거
        }

        void Update()
        {
            if (open)
            {
                var target = Quaternion.Euler(0, DoorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
            }
            else
            {
                var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
            }
        }

        public void OpenDoor()
        {
            open = !open;
            asource.clip = open ? openDoor : closeDoor;
            asource.Play();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(PlayVideoAndOpenDoor());
            }
        }

        IEnumerator PlayVideoAndOpenDoor()
        {
            // 1. 영상 재생
            if (videoPlayer != null && videoScreen != null)
            {
                videoScreen.enabled = true; // 영상 화면 활성화
                videoPlayer.Play();

                // 영상 길이만큼 대기
                yield return new WaitForSeconds((float)videoPlayer.length);

                // 2. 영상 끝난 후 문 열기
                OpenDoor();
                // videoScreen.enabled = false; 제거
            }
        }
    }
}
