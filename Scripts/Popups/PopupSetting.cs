
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Hellmade.Sound;

public class PopupSetting : Popups
{
    public static PopupSetting Instance;
    Action _onClose;
    #region DEFINE VARIABLES
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Slider sound;
    [SerializeField] private Slider sfx;
    #endregion

    #region FUNCTION
    private void Awake() {
        sfx.value = Sounds.Instance.sfxGameVolume;
        sfx.onValueChanged.AddListener(Sounds.Instance.ChangeSFXVolumn);

        sound.value = Sounds.Instance.musicGameVolume;
        sound.onValueChanged.AddListener(Sounds.Instance.ChangeMusicVolumn);
    }
    void InitUI()
    {
    }

    private void Update() {
        // Sounds.Instance.ChangeSFXVolumn(sfx.value);
    }
    
    #endregion

    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupSetting>("Prefabs/Popups/PopupSetting" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupSetting;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup4.transform,
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
            if (!Instance.gameObject.activeSelf) return;
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
