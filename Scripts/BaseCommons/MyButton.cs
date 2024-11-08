using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class MyButton : MonoBehaviour
{
    [SerializeField] public bool isPlaySound = true;
    [SerializeField] private AudioClip _onclickSound;
    public Animator Controller;
    public Button.ButtonClickedEvent OnClick = new Button.ButtonClickedEvent();

    public Button.ButtonClickedEvent OnClickDisable = new Button.ButtonClickedEvent();
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        if (_button)
        {
            _button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (_onclickSound != null)
        {
            if (isPlaySound)
                EazySoundManager.PlaySound(_onclickSound);
        }
        else
        {
            if (isPlaySound)
                EazySoundManager.PlaySound(Sounds.Instance.Sfx_Btn_Click);
        }

        if (Controller != default(Animator))
            Controller.Play("OnClick", 0, 0);
    }

    private void OnMouseUp()
    {
        if (!_button.interactable && OnClickDisable != null)
        {
            if (Controller != default(Animator))
                Controller.Play("OnClick", 0, 0);
        }
    }

    public void OnTrigger()
    {
        if (_button.interactable && OnClick != null)
            OnClick?.Invoke();

        if (!_button.interactable && OnClickDisable != null)
        {
            OnClickDisable?.Invoke();
        }
    }
}