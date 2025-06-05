using DialogueEditor;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public NPCConversation conversationData;

    public void StartDialogue()
    {
        if (conversationData != null)
        {
            ConversationManager.Instance.StartConversation(conversationData);
        }
    }
}


