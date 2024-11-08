using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Toast : MonoBehaviour
{
    static Toast _Instance;
    public CanvasGroup Canvas;
    public GameObject Render;
    public TextMeshProUGUI TxtContent;
    public GameObject Bg;
    bool IsStop = false;

    bool isClosing = true;

    static void CheckInstance(Action completed)//
    {
        if (_Instance == null)
        {
            var loadAsset = Resources.LoadAsync<Toast>("Prefabs/Popups/Toast");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as Toast;
                if (asset != null)
                {
                    _Instance = Instantiate(asset,
                        Popups.CanvasPopup4.transform,
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
    public static void Show(string content, bool isStop = false)
    {
        CheckInstance(() =>
        {
            _Instance.AppearBot(content, true);
        });
    }

    public static void ShowBot(string content)
    {
        CheckInstance(() =>
        {
            _Instance.AppearBot(content, true);
        });
    }

    void Appear(string content, bool isStop)
    {
        Bg.SetActive(true);
        if (IsStop)
        {
            return;
        }
        IsStop = isStop;


        Canvas.alpha = 1;
        TxtContent.text = content;
        TxtContent.transform.localScale = Vector3.zero;

        Render.transform.localPosition = Vector3.zero;
        LeanTween.cancel(gameObject);
        LeanTween.cancel(Render);
        LeanTween.cancel(TxtContent.gameObject);
        LeanTween.scale(TxtContent.gameObject, Vector3.one, .3f).setEaseOutBack().setOnComplete(() =>
        {
            isClosing = false;

            StartCoroutine(MonoHelper.DoSomeThing(1f, () =>
             {
                 isClosing = true;
                 LeanTween.moveLocalY(Render, 300, 1).setEaseInQuint();
                 LeanTween.alphaCanvas(Canvas, 0, 1.1f).setEaseInQuint().setOnComplete(() =>
                 {
                     IsStop = false;
                     Bg.SetActive(false);
                 });
             }));
        });

    }

    void AppearBot(string content, bool isStop)
    {
        transform.SetAsLastSibling();
        
        Bg.SetActive(false);
        if (IsStop)
        {
            return;
        }
        IsStop = isStop;

        Canvas.alpha = 1;
        TxtContent.text = content;

        TxtContent.transform.localScale = Vector3.zero;

        Render.GetComponent<RectTransform>().anchoredPosition = Vector3.up * -125;

        LeanTween.cancel(Render);
        LeanTween.cancel(TxtContent.gameObject);
        LeanTween.cancel(gameObject);
        LeanTween.scale(TxtContent.gameObject, Vector3.one, .3f).setEaseOutBack().setOnComplete(() =>
        { 
            isClosing = false;

            StartCoroutine(MonoHelper.DoSomeThing(1f, () =>
            {
                isClosing = true;
                //LeanTween.moveLocalY(Render, 1100, 1).setEaseInQuint();
                // BuniTween.MoveAnchorY(Render.GetComponent<RectTransform>(), 125, 1, null);
                LeanTween.alphaCanvas(Canvas, 0, 1.1f).setEaseInQuint().setOnComplete(() =>
                {
                    IsStop = false;
                    Bg.SetActive(false);
                });
            }));
        });

    }

    public void OnCloseToast()
    {
        if (GameStatics.IsAnimating || isClosing) return;
        isClosing = true;
        LeanTween.cancel(gameObject);
        LeanTween.cancel(Render);
        LeanTween.cancel(Canvas.gameObject);

        LeanTween.moveLocalY(Render, 300, .5f).setEaseInQuint();
        LeanTween.alphaCanvas(Canvas, 0, .55f).setEaseInQuint().setOnComplete(() =>
        {
            IsStop = false;
            Bg.SetActive(false);
        });
    }
}
