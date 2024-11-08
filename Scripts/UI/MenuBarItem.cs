using UnityEngine;
using UnityEngine.UI;
public class MenuBarItem : MonoBehaviour
{
    public MenuBar menuBarController;
    public MenuTabEnum menuTabEnum;
    public Button button;
    public Image icon;
    public float iconSelectedScaleAddIn = 0.3f;
    public float iconHighlightDistance = 20f;
    public float animationSpeed = 0.2f;

    private RectTransform rectTransform;
    private bool isInAnimation = false;
    public Vector2 defaultRect { get; private set; }
    public Vector2 currentRect { get; private set; }

    public void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        if (menuBarController == null)
        menuBarController = GetComponentInParent<MenuBar>();
    }

    private void Start() {
        // button.onClick.AddListener(() => {
        //     menuBarController.SelectButton(this);
        // });
    }

    public void SetDefaultDefaultWidth(){
        defaultRect = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
        // Debug.Log("Default Rect: " + defaultRect);
    }

    public void StretchOut(float width,bool isScroll = true){
        if (isScroll)
            LeanTween.value(0, 1, animationSpeed)
            .setOnStart(() => {
                isInAnimation = true;
            })
            .setOnUpdate((float value) => {
                rectTransform.sizeDelta = new Vector2(defaultRect.x + width * value, defaultRect.y);
                icon.transform.localScale = Vector2.one + new Vector2(iconSelectedScaleAddIn, iconSelectedScaleAddIn) * value;
                icon.transform.localPosition = new Vector2(icon.transform.localPosition.x, iconHighlightDistance * value);
            })
            .setOnComplete(() => {
                rectTransform.sizeDelta = new Vector2(defaultRect.x + width, defaultRect.y);
                currentRect = rectTransform.sizeDelta;
                icon.transform.localScale = Vector2.one + new Vector2(iconSelectedScaleAddIn, iconSelectedScaleAddIn);
                icon.transform.localPosition = new Vector2(icon.transform.localPosition.x, iconHighlightDistance);
                isInAnimation = false;
            });
        else
            rectTransform.sizeDelta = new Vector2(defaultRect.x + width, defaultRect.y);
            icon.transform.localScale = Vector2.one + new Vector2(iconSelectedScaleAddIn, iconSelectedScaleAddIn);
            icon.transform.localPosition = new Vector2(icon.transform.localPosition.x, iconHighlightDistance);
    }

    public void StretchIn(bool isScroll = true){
        if (isScroll){

            float widthDiff = (float)(currentRect.x - defaultRect.x);
            LeanTween.value(0, 1, animationSpeed)
            .setOnStart(() => {
                isInAnimation = true;
            })
            .setOnUpdate((float value) => {
                rectTransform.sizeDelta = new Vector2(currentRect.x - widthDiff * value, defaultRect.y);
                icon.transform.localScale = Vector2.one + new Vector2(iconSelectedScaleAddIn, iconSelectedScaleAddIn) * (1 - value);
                icon.transform.localPosition = new Vector2(icon.transform.localPosition.x,iconHighlightDistance * (1 - value));
            })
            .setOnComplete(() => {
                rectTransform.sizeDelta = defaultRect;
                currentRect = rectTransform.sizeDelta;
                icon.transform.localScale = Vector2.one;
                icon.transform.localPosition = Vector2.zero;
                isInAnimation = false;
            });
        }
        else
            rectTransform.sizeDelta = defaultRect;
            icon.transform.localScale = Vector2.one;
            icon.transform.localPosition = Vector2.zero;
    }

    public Vector2 GetPosition(){
        return rectTransform.anchoredPosition;
    }
}

