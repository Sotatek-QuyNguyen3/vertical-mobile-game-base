using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class MenuBar : MonoBehaviour
{
    [SerializeField] private List<MenuBarItem> menuBarItems;
    [SerializeField] private MenuBarItem selectingButton;
    [SerializeField] private Image selectingButtonMarker;
    [SerializeField] private float animationSpeed = 0.25f;
    [SerializeField] private AnimationCurve animationCurve;
    

    private float rectWidth;
    private float spaceToStretch;
    private RectTransform rectTransform;
    private Canvas parentCanvas;
    private int currentPageIndex;
    private bool isInAnimation = false;

    public void Awake(){
        selectingButton = menuBarItems[3];
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public IEnumerator Start(){
        CalculateRectWidth();
        yield return null;
        for (int i = 0; i < menuBarItems.Count; i++){
            int index = i;
            menuBarItems[i].button.onClick.AddListener(() => {
                SelectButton(index);
            });
            menuBarItems[i].animationSpeed = animationSpeed / 3;
        }
        SelectButtonByScroll((int)MenuManager.Instance.GetCurrentTab(),false);
        InitSelectingButtonMarker();
        selectingButtonMarker.gameObject.SetActive(true);
    }

    public void CalculateRectWidth(){
        rectWidth = rectTransform.rect.width;
        spaceToStretch = rectWidth;
        foreach (MenuBarItem menuBarItem in menuBarItems){
            menuBarItem.SetDefaultDefaultWidth();
            spaceToStretch -= menuBarItem.defaultRect.x;
        }
    }

    public void SelectButtonByScroll(int index,bool isScroll = true){
        if (currentPageIndex == index) return;
        currentPageIndex = index;
        menuBarItems[index].StretchOut(spaceToStretch,isScroll);
        selectingButton.StretchIn(isScroll);
        selectingButton = menuBarItems[index];
        SetSelectingButtonMarkerPosition();
    }

    public void SelectButton(int index,bool isScroll = true){
        if (currentPageIndex == index) return;
        if (isInAnimation) return;
        currentPageIndex = index;
        menuBarItems[index].StretchOut(spaceToStretch,isScroll);
        selectingButton.StretchIn(isScroll);
        selectingButton = menuBarItems[index];
        MenuManager.Instance.menuSlider.ScrollToPage(index);
        SetSelectingButtonMarkerPosition();
        LeanTween.delayedCall(animationSpeed, () => {
            isInAnimation = false;
        });
    }

    public void InitSelectingButtonMarker(){
        Vector2 newSize = menuBarItems[0].defaultRect + new Vector2(spaceToStretch, rectTransform.rect.height);
        selectingButtonMarker.rectTransform.sizeDelta = newSize;
    }

    // Đăt anchor của selectingButtonMarker là middle left nếu muốn hàm này chạy đc
    public void SetSelectingButtonMarkerPosition(){
        Vector2 targetPosition = Vector2.zero;
        LeanTween.value(0,1,animationSpeed)
        .setOnStart(() => {
            targetPosition = new Vector2 (selectingButton.defaultRect.x / 2 + selectingButton.defaultRect.x * currentPageIndex + spaceToStretch / 2, rectTransform.anchoredPosition.y - rectTransform.rect.height / 2);
        })
        .setOnUpdate((float value) => {
            selectingButtonMarker.rectTransform.anchoredPosition = Vector2.Lerp(selectingButtonMarker.rectTransform.anchoredPosition,targetPosition,value);
        }).setEase(animationCurve)
        .setOnComplete(() => {
            // selectingButtonMarker.rectTransform.anchoredPosition = targetPosition;
        });
    }
}

