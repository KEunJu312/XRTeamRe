using UnityEngine;
using DialogueEditor; // DialogueEditor 네임스페이스 사용

public class DialogueStarter : MonoBehaviour
{
    public NPCConversation rabbitConversation; // Inspector에서 RabbitConversation 연결

    public void StartRabbitDialogue()
    {
        // 대화 시작
        ConversationManager.Instance.StartConversation(rabbitConversation);
    }
}



