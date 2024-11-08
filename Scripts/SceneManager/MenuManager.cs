using UnityEngine;
using System.Collections.Generic;
using TS.PageSlider;
using System.Collections;
using System;
public class MenuManager : Singleton<MenuManager>
{
    public MenuTabEnum currentTab;
    public List<MenuPanel> menuPanels;
    public PageSlider menuSlider;
    public MenuBar menuBar;
    public RectTransform scrollContent;
    // public Action<int,int> onPageChangeEnded;

    protected override void Awake() {
        base.Awake();
    }
    public IEnumerator Start()
    {
        SetCurrentTab(MenuTabEnum.Battle);
        menuSlider._scroller.OnPageChangeEnded.AddListener(OnPageChangeEnded);
        yield return null;
        // onPageChangeEnded = OnPageChangeEnded;

    }
    public void SetCurrentTab(MenuTabEnum tab){
        currentTab = tab;
        menuSlider.ScrollToPage((int)tab);
    }
    public MenuTabEnum GetCurrentTab(){
        return currentTab;
    }
    public void OnPageChangeEnded(int from, int to){
        SetCurrentTab((MenuTabEnum)to);
        menuBar.SelectButtonByScroll(to);
    }
}

