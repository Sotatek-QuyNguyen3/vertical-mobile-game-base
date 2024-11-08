using Hellmade.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public static class PlayerPrefsUtil
{
    public static void LoadUserData()
    {
        if (PlayerPrefs.HasKey(GameStatics.PREF_deviceId))
        {
            string userDataStr = PlayerPrefs.GetString(GameStatics.PREF_deviceId);
            if (String.IsNullOrEmpty(userDataStr))
            {
                SetUserDataFirstime();
            }
            else
            {
                Debug.Log(PlayerPrefs.GetString(GameStatics.PREFS_USER_DATA));
                if (!String.IsNullOrEmpty(PlayerPrefs.GetString(GameStatics.PREFS_USER_DATA)))
                {
                    Debug.Log(JsonUtility.FromJson<UserDataOOP>(PlayerPrefs.GetString(GameStatics.PREFS_USER_DATA)));
                    UserDataManager.Instance.UserData =
                        JsonUtility.FromJson<UserDataOOP>(PlayerPrefs.GetString(GameStatics.PREFS_USER_DATA));
                }
                    
                //EazySoundManager.GlobalSoundsVolume = UserDataManager.Instance.UserData.settings.sound_vfx_volume;
                //EazySoundManager.GlobalUISoundsVolume = UserDataManager.Instance.UserData.settings.sound_vfx_volume;
                //EazySoundManager.GlobalMusicVolume = UserDataManager.Instance.UserData.settings.music_volume;
            }
        }
        else
        {
            /* Nếu trong PlayerPrefs chưa có */
            SetUserDataFirstime();
        }
    }
    public static void LoadUserStatsData()
    {
        string userStatsData = PlayerPrefs.GetString(GameStatics.PREFS_USER_STATS_DATA);

        if (String.IsNullOrEmpty(userStatsData))
        {
            SetUserStatsFirstime();
        }
        else
        {
            if (!String.IsNullOrEmpty(PlayerPrefs.GetString(GameStatics.PREFS_USER_STATS_DATA)))
            {
                Debug.Log(JsonUtility.FromJson<UserStatsOOP>(PlayerPrefs.GetString(GameStatics.PREFS_USER_STATS_DATA)));
                UserStatsManager.Instance.UserStats =
                    JsonUtility.FromJson<UserStatsOOP>(PlayerPrefs.GetString(GameStatics.PREFS_USER_STATS_DATA));
            }
        }
    }
    public static void SetUserDataFirstime()
    {
        string deviceId = "";
#if UNITY_ANDROID
        Debug.Log("UNITY ANDROID");
        deviceId = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_IOS
        Debug.Log("UNITY IOS");
        deviceId = Device.vendorIdentifier;
#else
        Debug.Log("UNITY ESLE");
        deviceId = SystemInfo.deviceUniqueIdentifier;
#endif
        if (string.IsNullOrEmpty(deviceId) || deviceId.Length < 1)
        {
            deviceId = "" + SystemInfo.deviceUniqueIdentifier;
        }

        Debug.Log("==== First Time : DeviceID : " + deviceId);
        Debug.Log("==== First Time : DeviceID System: " + SystemInfo.deviceUniqueIdentifier);
        UserDataManager.Instance.UserData.device_id = deviceId;
        UserDataManager.Instance.UserData.user_name = SystemInfo.deviceName;
        PlayerPrefs.SetString(GameStatics.PREF_deviceId, deviceId);
        Debug.Log($"set pref device: {deviceId}");
        SaveDataToPrefs();
    }
    public static void SetUserStatsFirstime()
    {
        UserStatsManager.Instance.UserStats.power = GameDataManager.Instance.GameData.basePower + UserStatsManager.Instance.UserStats.powerUpgradePoint;
        UserStatsManager.Instance.UserStats.stamina = GameDataManager.Instance.GameData.baseStamina + UserStatsManager.Instance.UserStats.staminaUpgradePoint;
        UserStatsManager.Instance.UserStats.energy = GameDataManager.Instance.GameData.baseEnergy + UserStatsManager.Instance.UserStats.energyUpgradePoint;
        UserStatsManager.Instance.UserStats.perfectHitZone = GameDataManager.Instance.GameData.basePerfectHitZone + UserStatsManager.Instance.UserStats.perfectHitZoneUpgradePoint;

        SaveDataToPrefs();
    }
    public static void SaveDataToPrefs()
    {
        string userDataStr = JsonUtility.ToJson(UserDataManager.Instance.UserData);
        PlayerPrefs.SetString(GameStatics.PREFS_USER_DATA, userDataStr);

        string gameDataStr = JsonUtility.ToJson(GameDataManager.Instance.GameData);
        PlayerPrefs.SetString(GameStatics.PREFS_GAME_DATA, gameDataStr);

        string userStatsStr = JsonUtility.ToJson(UserStatsManager.Instance.UserStats);
        PlayerPrefs.SetString(GameStatics.PREFS_USER_STATS_DATA, userStatsStr);
    }
    public static void SaveDataToFile()
    {
        string gameDataFile = GetSavedPath() + "/gamedata.json";
        string userDataFile = GetSavedPath() + "/userdata.json";
        string userStatsDataFile = GetSavedPath() + "/userstatsdata.json";

        string gameDataStr = JsonUtility.ToJson(GameDataManager.Instance.GameData);
        string userDataStr = JsonUtility.ToJson(UserDataManager.Instance.UserData);
        string userStatsDataStr = JsonUtility.ToJson(UserStatsManager.Instance.UserStats);

        File.WriteAllText(gameDataFile, gameDataStr);
        File.WriteAllText(userDataFile, userDataStr);
        File.WriteAllText(userStatsDataFile, userStatsDataStr);
    }
    public static string GetSavedPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources";
#else
        return Application.persistentDataPath ; // + Path.DirectorySeparatorChar;
#endif
    }
}
