using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public InputActionProperty triggerAction;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        // StartDialogue(); // ó���� ��Ȱ��ȭ ���·� �ΰ�, XR ��ȣ�ۿ����� �Ѵ� �� �Ϲ���
    }

    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

