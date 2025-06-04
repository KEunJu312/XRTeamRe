using UnityEngine;
using DialogueEditor;
using UnityEngine.XR;
using System.Collections.Generic;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] public NPCConversation myConversation;
    [SerializeField] private Transform rightControllerTransform; // Camera Offset > RightController 연결

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

        // 1. 트리거 버튼 입력 확인
        bool triggerPressed = false;
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            // 2. 컨트롤러가 토끼(NPC)를 바라보고 있는지 Raycast 체크
            Ray ray = new Ray(rightControllerTransform.position, rightControllerTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f)) // 10m 이내 거리 체크
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    // 3. 대화 시작
                    ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }
    }
}
