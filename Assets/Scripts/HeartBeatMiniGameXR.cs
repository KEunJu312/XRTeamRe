using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HeartBeatMiniGameXR : MonoBehaviour
{
    public FlashbackTriggerWithHeartMiniGame flashbackTrigger;
    public Volume postProcessingVolume;

    public RectTransform centerHeart;
    public GameObject movingHeartPrefab;

    public float spawnInterval = 1.5f;
    public float perfectRange = 30f;
    public int successNeeded = 5;

    private int currentSuccess = 0;
    public float flashbackStrengthIncrease = 0.1f;

    private List<MovingHeart> activeHearts = new List<MovingHeart>();

    // 버튼 Down 판정용 변수
    private bool prevLeftPressed = false;
    private bool prevRightPressed = false;

    // 오디오 관련
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip failClip;

    void OnEnable()
    {
        StartCoroutine(SpawnHearts());
    }

    void OnDisable()
    {
        StopAllCoroutines();
        activeHearts.Clear();
    }

    IEnumerator SpawnHearts()
    {
        while (true)
        {
            SpawnHeart();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnHeart()
    {
        GameObject newHeart = Instantiate(movingHeartPrefab, transform);
        MovingHeart moving = newHeart.GetComponent<MovingHeart>();

        bool left = Random.value > 0.5f;
        moving.isLeft = left;

        if (left)
        {
            moving.rectTransform.anchoredPosition = new Vector2(-500f, 0f);
        }
        else
        {
            moving.rectTransform.anchoredPosition = new Vector2(500f, 0f);
        }

        activeHearts.Add(moving);
    }

    void Update()
    {
        bool leftPressed = false;
        bool rightPressed = false;

        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out leftPressed);
        rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out rightPressed);

        bool leftDown = leftPressed && !prevLeftPressed;
        bool rightDown = rightPressed && !prevRightPressed;

        prevLeftPressed = leftPressed;
        prevRightPressed = rightPressed;

        for (int i = activeHearts.Count - 1; i >= 0; i--)
        {
            MovingHeart heart = activeHearts[i];
            heart.MoveTowardsCenter();

            if (heart.IsInPerfectRange(centerHeart.anchoredPosition, perfectRange))
            {
                if (heart.isLeft && leftDown)
                {
                    OnHeartSuccess(heart);
                    Debug.Log("왼쪽 하트 누르기 성공");
                    break;
                }
                else if (!heart.isLeft && rightDown)
                {
                    OnHeartSuccess(heart);
                    Debug.Log("오른쪽 하트 누르기 성공");
                    break;
                }
                else if (leftDown || rightDown)
                {
                    OnHeartFail(heart);
                    Debug.Log("하트 누르기 실패");
                    break;
                }
            }
        }
    }

    private void OnHeartSuccess(MovingHeart heart)
    {
        Debug.Log("Heart Success!");
        currentSuccess++;
        activeHearts.Remove(heart);
        Destroy(heart.gameObject);

        // 성공 시 오디오 재생
        if (audioSource != null && successClip != null)
        {
            audioSource.PlayOneShot(successClip);
        }

        if (currentSuccess >= successNeeded)
        {
            flashbackTrigger.SetEscapeConditionMet(); // 5번 성공 시 탈출 조건 충족
        }
    }

    private void OnHeartFail(MovingHeart heart)
    {
        Debug.Log("Heart Fail! Flashback Stronger!");
        postProcessingVolume.weight = Mathf.Clamp01(postProcessingVolume.weight + flashbackStrengthIncrease);

        activeHearts.Remove(heart);
        Destroy(heart.gameObject);

        // 실패 시 오디오 재생
        if (audioSource != null && failClip != null)
        {
            audioSource.PlayOneShot(failClip);
        }
    }
}


