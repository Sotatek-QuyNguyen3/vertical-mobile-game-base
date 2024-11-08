using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatics
{
    public static bool IsAnimating = false;
    public static bool IsPause = false;

    public static int IdTileSelect = -1;
    public static bool DarkTheme = false;
    public static bool IsUseBoom = false;
    public static int LevelJump = 0;
    public static bool isOpenMusic = false;
    public static bool isAds = false;
    public static int TimeRevive = 0;
    public static int TimeShowInput = 0;
    public static bool IsOpenMilestone = false;
    public static Sprite SprBanner = null;
    public static int TimeShowRating = 0;

    public static int TimeOfflineEarning = 20;

    public static bool show1 = false;
    public static bool show2 = false;
    public static bool show3 = false;

    public static bool isHasRank = true;

    public static bool isAlreadyAskUpdateNewVersion = false;

    public static bool IsCombat = false;
    public static bool IsCanContactHero = true;
    public static bool IsInitFirebase = false;

    public static bool isIntroduceSkill = false;

    public static int IsDownloadDone = 0;

    public static JSONObject BATTLE_DATA;
    /* PlayerPrefs Keys */
    public static string PREFS_DEV_IN_BATTLE = "DEV_IN_BATTLE";
    public static string PREFS_FIRST_TIME_INGAME = "FirstTimeInGame";
    public static string PREFS_CURRENT_LEVEL_KEY = "CurrentLevel";
    public static string PREFS_CURRENT_ROOM_KEY = "CurrentRoom";
    public static string PREFS_STATE_LEVEL_KEY = "StateCurrentLevel";
    public static string PREFS_STATE_SURVIVOR_KEY = "StateSurvivorMode";
    public static string PREFS_CURRENT_HERO_CODE_KEY = "HeroCode";
    public static string PREFS_USER_DATA = "UserData";
    public static string PREFS_USER_STATS_DATA = "UserStatsData";
    public static string PREFS_GAME_DATA = "GameData";
    public static string PREFS_SHOW_INVENTORY = "SHOW_INVENTORY";
    public static string PREFS_TIME_PLAYED = "TIMES_PLAYED";
    public static string PREF_deviceId = "DeviceId";

    public static string PREFS_TEST_DATA = "TestData";

    /* Smash */
    public static string PERFECT_HIT = "PerfectHit";
    public static string GOOD_HIT = "GoodHit";
    public static string NORMAL_HIT = "NormalHit";
}
