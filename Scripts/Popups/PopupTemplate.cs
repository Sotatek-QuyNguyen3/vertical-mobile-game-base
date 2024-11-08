using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hellmade.Sound;
using UnityEngine.SceneManagement;

public class PopupTemplate : Popups
{
    public static PopupTemplate Instance;
    Action _onClose;
    #region DEFINE VARIABLES
    [SerializeField] private TextMeshProUGUI titleText;
    #endregion

    #region FUNCTION
    void InitUI()
    {
       titleText.text = "Welcome to Popup";
    }
    
    #endregion

    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupTemplate>("Prefabs/Popups/PopupTemplate" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupTemplate;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup3.transform,
                        false);

                    if (completed != null)
                    {
                        completed();
                    }
                }
            };

        }
        else
        {
            if (completed != null)
            {
                completed();
            }
        }
    }

    public static void Show()//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI();
        });

    }

    public static void Hide()
    {
        if (GameStatics.IsAnimating) return;

        Instance.Disappear();
    }
    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public void Disappear()
    {
        //Background.gameObject.SetActive(false);
        Panel.gameObject.SetActive(false);
        base.Disappear();
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void NextStep(object value = null)
    {
    }
    #endregion

}
