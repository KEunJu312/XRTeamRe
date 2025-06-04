using UnityEngine;
using DialogueEditor;
using UnityEngine.XR;
using NUnit.Framework;
using System.Collections.Generic;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] public NPCConversation myConversation;

    private InputDevice rightController;

    void Start()
    {
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        if (rightHandDevices.Count > 0) { rightController = rightHandDevices[0]; }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (!rightController.isValid) return;
            
            bool aButtonPressed = false;

            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton,out aButtonPressed)&&aButtonPressed) 
            {
                ConversationManager.Instance.StartConversation(myConversation);
            }
        }
    }
}
