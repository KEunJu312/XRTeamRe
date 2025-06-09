using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    [Header("References")]
    public RabbitController rabbitController; // �䳢 ��Ʈ�ѷ� ����

    private void Start()
    {
        // RabbitController �ڵ� ã�� (�������� ���� ���)
        if (rabbitController == null)
        {

            GameObject rabbit = GameObject.Find("Rabbit");
            if (rabbit != null)
            {
                rabbitController = rabbit.GetComponent<RabbitController>();
                rabbitController.SetFollowPlayer(true);
            }
        }
    }

    // Dialogue Editor�� ��ȭ�� ������ �� ȣ��Ǵ� �Լ�
    // �� �Լ��� Dialogue Editor�� Node End Event�� �����ϼ���
    public void OnDialogueComplete()
    {
        Debug.Log("Dialogue completed. Notifying rabbit controller.");

        if (rabbitController != null)
        {
            rabbitController.OnDialogueEnd();
        }
        else
        {
            Debug.LogError("RabbitController not found!");
        }
    }

    // NPCController���� ���� ȣ���� ���� �ִ� �Լ�
    public void NotifyDialogueEnd()
    {
        OnDialogueComplete();
    }
}