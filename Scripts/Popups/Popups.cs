using Hellmade.Sound;
using System;
using System.Collections.Generic;
using UnityEngine;
//using EazyTools.SoundManager;

public abstract class Popups : MonoBehaviour
{
    public CanvasGroup Root;
    public GameObject Background;
    public CanvasGroup Panel;
    [SerializeField]private bool _isAnimateAppear;
    #region Canvas Get
    static GameObject _canvasPage;
    public static GameObject CanvasPage
    {
        get
        {
            if (_canvasPage == null)
                _canvasPage = GameObject.FindWithTag("Canvas Page");

            return _canvasPage;
        }
    }

    static GameObject _canvasPopup1;
    public static GameObject CanvasPopup
    {
        get
        {
            if (_canvasPopup1 == null)
                _canvasPopup1 = GameObject.FindWithTag("Canvas Popup 1");

            return _canvasPopup1;
        }
    }


    static GameObject _canvasPopup2;
    public static GameObject CanvasPopup2
    {
        get
        {
            if (_canvasPopup2 == null)
                _canvasPopup2 = GameObject.FindWithTag("Canvas Popup 2");

            return _canvasPopup2;
        }
    }

    static GameObject _canvasPopup3;
    public static GameObject CanvasPopup3
    {
        get
        {
            if (_canvasPopup3 == null)
                _canvasPopup3 = GameObject.FindWithTag("Canvas Popup 3");

            return _canvasPopup3;
        }
    }

    static GameObject _canvasPopup4;
    public static GameObject CanvasPopup4
    {
        get
        {
            if (_canvasPopup4 == null)
                _canvasPopup4 = GameObject.FindWithTag("Canvas Popup 4");

            return _canvasPopup4;
        }
    }

    static GameObject _canvasPopup5;
    public static GameObject CanvasPopup5
    {
        get
        {
            if (_canvasPopup5 == null)
                _canvasPopup5 = GameObject.FindWithTag("Canvas Popup 5");

            return _canvasPopup5;
        }
    }

    static GameObject _canvasToast;
    public static GameObject CanvasToast
    {
        get
        {
            if (_canvasToast == null)
                _canvasToast = GameObject.FindWithTag("Canvas Toast");

            return _canvasToast;
        }
    }

    static GameObject _canvasFX;
    public static GameObject CanvasFX
    {
        get
        {
            if (_canvasFX == null)
                _canvasFX = GameObject.FindWithTag("Canvas FX");

            return _canvasFX;
        }
    }
    #endregion

    /// <summary>
    /// Check if another popup is showed, ignore the appearance
    /// </summary>
    public static bool IsShowed = false;
    public static bool IgnoreSoundOneTime = false;

    public static List<Popups> Stacks = new List<Popups>();
    public static bool DestroyIgnore = false;
    public static bool StackIgnore = true;
    public static bool DisappearIgnore = false;
    public static bool DisappearIgnoreNextAppear = false;
    protected bool IsLoadBoxCollider = true;
    protected GameObject _collider;
    static bool _isSetStacks = false;

    private void OnLevelWasLoaded(int level)
    {
        if (_isSetStacks == false)
        {
            Stacks.Clear();

            _isSetStacks = true;

            DisappearIgnore = false;
            DisappearIgnoreNextAppear = false;
        }
    }

    private void OnDestroy()
    {
        if (_isSetStacks && !DestroyIgnore)
        {
            //
            //GameStatics.IsAnimating = false;

            //
            Stacks.Clear();

            _isSetStacks = false;
;
            IsShowed = false;
            DisappearIgnore = false;
            DisappearIgnoreNextAppear = false;
        }
        else if (DestroyIgnore)
        {
            DestroyIgnore = false;
        }
        _collider = null;
    }

    /// <summary>
    /// Abstract functions
    /// 
    public abstract void NextStep(object value = null);


    /// <summary>
    /// Virtual functions
    /// </summary>
    /// 
    public virtual void Appear()
    {
        gameObject.SetActive(true);
        Root.alpha = 1;
        Root.interactable = true;
        Root.blocksRaycasts = true;

        EazySoundManager.PlaySound(Sounds.Instance.Sfx_Show_Popup);
        if(Panel!=null && _isAnimateAppear)
		{

            LeanTween.value(gameObject,0,1,0.4f).setOnUpdate((float value)=> {
                Panel.transform.localScale = Vector3.one * value;
            }).setEaseOutBounce().setOnComplete(() => {


            });

           
		}
        else
		{
            Root.alpha = 1;
        }
        IsShowed = true;

        if (IsLoadBoxCollider)
        {
            if (_collider == null)
            {
                _collider = Instantiate(Resources.Load<GameObject>("Prefabs/Collider/Collider"), Panel.transform, false);
            }
        }
    }

    public virtual void Disappear()
    {
        //Root.alpha = 0;
        Root.interactable = false;
        Root.blocksRaycasts = false;
        EazySoundManager.PlaySound(Sounds.Instance.Sfx_Hide_Popup);


        if (Panel != null && _isAnimateAppear)
        {
            LeanTween.value(gameObject, 1, 0,0.2f).setOnUpdate((float value) => {
                Panel.transform.localScale = Vector3.one * value;
            }).setEaseInSine().setOnComplete(() => {

                Root.alpha = 0;
            });
        }
        else
		{
            Root.alpha = 0;
        }
        if (!DisappearIgnore)
        {
            if (!StackIgnore)
            {
                Stacks.Remove(this);

                if (DisappearIgnoreNextAppear)
                {
                    DisappearIgnoreNextAppear = false;
                }
                else
                {
                    if (Stacks.Count > 0)
                    {
                        StackIgnore = true;
                        Stacks[Stacks.Count - 1].Appear();
                    }
                }
            }
            else
            {

                StackIgnore = false;
            }
        }
        else
        {
            DisappearIgnore = false;
            Stacks.Clear();
        }
        gameObject.SetActive(false); 
    }

    public virtual void Disable()
    {
        Root.alpha = 0;
        Root.interactable = false;
        Root.blocksRaycasts = false;

        IsShowed = false;

        if (!DisappearIgnore)
        {
            if (!StackIgnore)
            {
                Stacks.Remove(this);

                if (Stacks.Count > 0)
                {
                    StackIgnore = true;
                    Stacks[Stacks.Count - 1].Appear();
                }
            }
            else
            {
                StackIgnore = false;
            }
        }
        else
        {
            DisappearIgnore = false;
            Stacks.Clear();
        }
    }
}
