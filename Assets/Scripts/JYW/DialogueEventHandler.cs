using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    [Header("References")]
    public RabbitController rabbitController; // 토끼 컨트롤러 참조

    private void Start()
    {
        // RabbitController 자동 찾기 (설정되지 않은 경우)
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

    // Dialogue Editor의 대화가 끝났을 때 호출되는 함수
    // 이 함수를 Dialogue Editor의 Node End Event에 연결하세요
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

    // NPCController에서 직접 호출할 수도 있는 함수
    public void NotifyDialogueEnd()
    {
        OnDialogueComplete();
    }
}