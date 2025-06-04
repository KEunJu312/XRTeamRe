using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlashbackTrigger : MonoBehaviour
{
    public Volume postProcessingVolume;  // Post Processing Volume (Global or Local)
    public float flashbackDuration = 3f;  // �÷��ù� ���� �ð�

    private bool isFlashingBack = false;

    private void OnTriggerEnter(Collider other)
    {
        // XR Origin (�÷��̾�) �� �浹 ��
        if (other.CompareTag("Player") && !isFlashingBack)
        {
            StartCoroutine(FlashbackSequence());
        }
    }

    private IEnumerator FlashbackSequence()
    {
        isFlashingBack = true;
        Debug.Log("Flashback ����");

        // Post Processing ȿ�� Ȱ��ȭ (��: Chromatic Aberration, Vignette ��)
        postProcessingVolume.weight = 1f;

        // ���� ȿ���� ī�޶� ��鸲 �� �߰� ����

        yield return new WaitForSeconds(flashbackDuration);

        // ȿ�� ����
        postProcessingVolume.weight = 0f;
        Debug.Log("Flashback ����");

        isFlashingBack = false;
    }
}
