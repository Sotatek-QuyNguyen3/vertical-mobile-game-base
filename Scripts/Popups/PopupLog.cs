using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupLog : Popups
{
    public static PopupLog _Instance;
    [SerializeField] private TextMeshProUGUI _LogTxtObject;
    public static String _LogText = "Start Logging. \n";
    public static void Show()//
    {
        CheckInstance(() =>
        {
            _Instance.Appear();
            _Instance.SetData();
        });
    }

    static void CheckInstance(Action completed)//
    {
        if (_Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupLog>("Prefabs/Popups/PopupLog" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupLog;
                if (asset != null)
                {
                    _Instance = Instantiate(asset,
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

    public void SetData()
    {
        _LogTxtObject.text = _LogText;
    }

    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public override void Disappear()
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
}
