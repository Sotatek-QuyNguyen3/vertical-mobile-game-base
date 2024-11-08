using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Contain Section")] [SerializeField]
    public GameObject HUDContainerGO;

    [Header("HUD")] public TextMeshProUGUI versionText;
    [SerializeField] private GameObject _xpGroup;
    [SerializeField] private TextMeshProUGUI _userXpTxt;
    [SerializeField] private GameObject _gemGroup;
    [SerializeField] private TextMeshProUGUI _userGemTxt;
    [SerializeField] private GameObject _goldGroup;
    [SerializeField] private TextMeshProUGUI _userGoldTxt;
    [SerializeField] private GameObject _profileGroup;
    [SerializeField] private TextMeshProUGUI _userStrengthTxt;
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private TextMeshProUGUI _lvTxt;
    [SerializeField] private TextMeshProUGUI _userNameTxt;






    #region UnityMethod

    private void Start()
    {
        // Set the quality level to Fastest (index 0)
        QualitySettings.SetQualityLevel(0, true);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // versionText.text = $"v{Application.version}";
        InitEventTracking();
        
        InitHUD();
        
#if UNITY_EDITOR
        
#elif UNITY_WEBGL
        Debug.Log("Login WebGL");
        WebInteract.GameRequestLogin("");
#endif
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {

    }

    public void HideProfile(){
        _profileGroup.SetActive(false);
    }

    public void ShowProfile(){
        _profileGroup.SetActive(true);
    }

    public void HideCurrency(){
        _goldGroup.SetActive(false);
        _gemGroup.SetActive(false);
    }

    public void ShowCurrency(){
        _goldGroup.SetActive(true);
        _gemGroup.SetActive(true);
    }

    private void InitEventTracking(){
        EventManager.Instance.StartListening(EventName.UserChangeGold, OnUserChangeGold);
        EventManager.Instance.StartListening(EventName.UserChangeGem, OnUserChangeGem);
        EventManager.Instance.StartListening(EventName.UserChangeXp, OnUserChangeExp);
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening(EventName.UserChangeGold, OnUserChangeGold);
            EventManager.Instance.StopListening(EventName.UserChangeGem, OnUserChangeGem);
            EventManager.Instance.StopListening(EventName.UserChangeXp, OnUserChangeExp);
        }
    }

    #endregion

    #region Show Hide

    public void Show()
    {
        InitHUD();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public Vector3 CenterPosition()
    {
        return Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
    }

    #endregion

    #region SetupUI

    private void InitHUD()
    {
        SetProfileText();
        SetGoldText();
        SetGemText();
        SetXpText();
    }

    private void SetProfileText(){
        _userNameTxt.text = UserDataManager.Instance.UserData.user_name;
        _userStrengthTxt.text = UserDataManager.Instance.UserData.userStrength.ToString();
        _lvTxt.text = UserDataManager.Instance.UserData.account_level.ToString();
        _userXpTxt.text = CurrencyHelper.ConvertToThousandString(UserDataManager.Instance.UserXp);
        // Chờ đến khi có data thì sửa
        _xpSlider.value = 1;
    }

    public void SetGoldText()
    {
        _userGoldTxt.text = CurrencyHelper.ConvertToThousandString(UserDataManager.Instance.UserGold);
    }
    public void SetGemText()
    {
        _userGemTxt.text = CurrencyHelper.ConvertToThousandString(UserDataManager.Instance.UserGem);
    }
    public void SetXpText()
    {
        _userXpTxt.text = CurrencyHelper.ConvertToThousandString(UserDataManager.Instance.UserXp);
    }

    #endregion

    #region Event Listeners

    private void UpdateUserCurrency(Dictionary<string, object> msg, Action<string> updateTextAction, Func<int> getCurrentValue)
    {
        int changeAmount = (int)msg["amount"];
        if (changeAmount != 0)
        {
            LeanTween.value(changeAmount, 0f, .5f).setOnUpdate(value =>
            {
                float newValue = getCurrentValue() - changeAmount + (changeAmount - value);
                newValue = Mathf.Max(0, newValue);
                updateTextAction(CurrencyHelper.ConvertToThousandString(Mathf.CeilToInt(newValue)));
            }).setOnComplete(() =>
            {
                updateTextAction(CurrencyHelper.ConvertToThousandString(getCurrentValue()));
            });
        }
    }

    private void OnUserChangeGold(Dictionary<string, object> msg)
    {
        UpdateUserCurrency(msg, text => _userGoldTxt.text = text, () => UserDataManager.Instance.UserGold);
    }

    private void OnUserChangeGem(Dictionary<string, object> msg)
    {
        UpdateUserCurrency(msg, text => _userGemTxt.text = text, () => UserDataManager.Instance.UserGem);
    }

    private void OnUserChangeExp(Dictionary<string, object> msg)
    {
        UpdateUserCurrency(msg, text => _userXpTxt.text = text, () => UserDataManager.Instance.UserXp);
    }

    // private void OnUserChangeGold(Dictionary<string, object> msg)
    // {
    //     float changeAmount = (float)msg["amount"];
    //     if (changeAmount != 0)
    //         LeanTween.value(changeAmount, 0f, .5f).setOnUpdate(value =>
    //         {
    //             float newValue = UserDataManager.Instance.UserGold - changeAmount + (changeAmount - value);
    //             newValue = Mathf.Max(0, newValue);
    //             _userGoldTxt.text = CurrencyHelper.ConvertToThousandString(Mathf.CeilToInt(newValue));
    //         }).setOnComplete(()=>
    //         {
    //             SetGoldText();
    //         });
    // }
    // private void OnUserChangeGem(Dictionary<string, object> msg)
    // {
    // float changeAmount = (float)msg["amount"];
    // if (changeAmount != 0)
    //     LeanTween.value(changeAmount, 0f, .5f).setOnUpdate(value =>
    //     {
    //         float newValue = UserDataManager.Instance.UserGem - changeAmount + (changeAmount - value);
    //         newValue = Mathf.Max(0, newValue);
    //         _userGemTxt.text = CurrencyHelper.ConvertToThousandString(Mathf.CeilToInt(newValue));
    //     }).setOnComplete(() =>
    //     {
    //         SetGemText();
    //     });
    // }

    // private void OnUserChangeExp(Dictionary<string, object> msg)
    // {
    //     float changeAmount = (float)msg["amount"];
    //     if (changeAmount != 0)
    //         LeanTween.value(changeAmount, 0f, .5f).setOnUpdate(value =>
    //         {
    //             float newValue = UserDataManager.Instance.UserXp - changeAmount + (changeAmount - value);
    //             newValue = Mathf.Max(0, newValue);
    //             _userXpTxt.text = CurrencyHelper.ConvertToThousandString(Mathf.CeilToInt(newValue));
    //         }).setOnComplete(() =>
    //         {
    //             SetXpText();
    //         });
    // }

    #endregion

    #region ButtonAction

    public void OnClickMenu()
    {
        
    }

    public void OnClickGoldIcon()
    {
        UserDataManager.Instance.GainGold(100);
    }

    public void OnClickGemIcon()
    {
        UserDataManager.Instance.GainGem(100);
    }

    public void OnXpClickIcon()
    {
        UserDataManager.Instance.GainXp(100);
    }

    #endregion

    #region CHEAT SECTION

    #endregion
}