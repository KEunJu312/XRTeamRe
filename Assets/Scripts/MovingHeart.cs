using UnityEngine;

public class MovingHeart : MonoBehaviour
{
    public bool isLeft = true; // ÁÂ/¿ì ±¸ºÐ
    public float moveSpeed = 300f;

    public RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void MoveTowardsCenter()
    {
        Vector3 targetPos = new Vector3(0f, rectTransform.anchoredPosition.y, 0f);
        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
    }

    public bool IsInPerfectRange(Vector3 centerPos, float range)
    {
        return Mathf.Abs(rectTransform.anchoredPosition.x - centerPos.x) <= range;
    }
}
