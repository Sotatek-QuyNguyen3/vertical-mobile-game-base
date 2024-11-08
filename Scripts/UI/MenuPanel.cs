using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected Canvas canvas;
    public void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start() {
        rectTransform.sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x, rectTransform.sizeDelta.y);
    }
}

