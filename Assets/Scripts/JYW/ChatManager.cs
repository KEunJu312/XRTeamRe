using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject chatLogPanel;
    public Text chatLogText;

    void Start()
    {
        chatLogPanel.SetActive(false);
    }

    public void showNPCMassage(string npcMessage)
    {
        if (!chatLogPanel.activeSelf)
        {
            chatLogPanel.SetActive(true);
        }
        chatLogText.text += "\nRabbit: " + npcMessage;
    }
}
