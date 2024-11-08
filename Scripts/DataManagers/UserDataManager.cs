
using UnityEngine;
using System.Collections.Generic;

public class UserDataManager : PersistentSingleton<UserDataManager>
{
    [SerializeField] private UserDataOOP _userData = new UserDataOOP();

    public UserDataOOP UserData
    {
        get => _userData;
        set => _userData = value;
    }

    public int UserXp => _userData.balance_xp;
    public int UserGem => _userData.balance_gem;
    public int UserGold => _userData.balance_gold;

    private void Start()
    {
        if (_userData == null)
        {
            _userData = new UserDataOOP();
        }

        PlayerPrefsUtil.LoadUserData();
    }

    public void GainGold(int amount)
    {
        _userData.balance_gold += amount;
        Debug.Log("gain gold: " + amount);
        EventManager.Instance.TriggerEvent(EventName.UserChangeGold, new Dictionary<string, object> { { "amount", amount } });
    }

    public void GainGem(int amount)
    {
        _userData.balance_gem += amount;
        Debug.Log("gain gem: " + amount);
        EventManager.Instance.TriggerEvent(EventName.UserChangeGem, new Dictionary<string, object> { { "amount", amount } });
    }

    public void GainXp(int amount)
    {
        _userData.balance_xp += amount;
        Debug.Log("gain xp: " + amount);
        EventManager.Instance.TriggerEvent(EventName.UserChangeXp, new Dictionary<string, object> { { "amount", amount } });
    }

}