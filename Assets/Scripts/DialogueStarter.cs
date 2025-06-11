using UnityEngine;
using DialogueEditor;
public class DialogueStarter : MonoBehaviour
{
    public NPCConversation rabbitConversation;
    public void StartRabbitDialogue()
    {
        // 대화 시작
        ConversationManager.Instance.StartConversation(rabbitConversation);
    }
}



