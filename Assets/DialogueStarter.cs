using UnityEngine;
using DialogueEditor; // DialogueEditor ���ӽ����̽� ���

public class DialogueStarter : MonoBehaviour
{
    public NPCConversation rabbitConversation; // Inspector���� RabbitConversation ����

    public void StartRabbitDialogue()
    {
        // ��ȭ ����
        ConversationManager.Instance.StartConversation(rabbitConversation);
    }
}



