using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public InputActionProperty triggerAction;

    private int index;

    public void SetLines(string[] newLines)
    {
        lines = newLines;
    }

    internal void SetLines(object lines)
    {
        throw new NotImplementedException();
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;
        if (triggerAction.action.WasPressedThisFrame())
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
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
