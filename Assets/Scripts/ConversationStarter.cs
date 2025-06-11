using UnityEngine;
using DialogueEditor;
using UnityEngine.XR;
using System.Collections.Generic;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] public NPCConversation myConversation;
    [SerializeField] private Transform rightControllerTransform; // Camera Offset > RightController ����

    private InputDevice rightController;

    void Start()
    {
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        if (rightHandDevices.Count > 0)
        {
            rightController = rightHandDevices[0];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!rightController.isValid) return;

        // 1. Ʈ���� ��ư �Է� Ȯ��
        bool triggerPressed = false;
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            // 2. ��Ʈ�ѷ��� �䳢(NPC)�� �ٶ󺸰� �ִ��� Raycast üũ
            Ray ray = new Ray(rightControllerTransform.position, rightControllerTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f)) // 10m �̳� �Ÿ� üũ
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    // 3. ��ȭ ����
                    ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }
    }
}
