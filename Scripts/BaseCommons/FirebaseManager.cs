using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : Singleton<FirebaseManager>
{
    // private Firebase.FirebaseApp app;
    // void Start()
    // {
    //     // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    //     // {
    //     //     var dependencyStatus = task.Result;
    //     //     if (dependencyStatus == Firebase.DependencyStatus.Available)
    //     //     {
    //     //         // Create and hold a reference to your FirebaseApp,
    //     //         // where app is a Firebase.FirebaseApp property of your application class.
    //     //         app = Firebase.FirebaseApp.DefaultInstance;
    //
    //     //         // Set a flag here to indicate whether Firebase is ready to use by your app.
    //     //     }
    //     //     else
    //     //     {
    //     //         UnityEngine.Debug.LogError(System.String.Format(
    //     //           "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    //     //         // Firebase Unity SDK is not safe to use here.
    //     //     }
    //     // });
    // }
    // public static void PushEventByName(string eventName)
    // {
    //     try
    //     {
    //         Firebase.Analytics.FirebaseAnalytics.LogEvent(
    //             eventName
    //         );
    //     }
    //     catch (Exception e)
    //     {
    //     }
    // }
    // // FirebaseManager.PushEventRoom(EVENT_TRACKING.room_start.ToString(), CurrentChapter, CurrentLevel, CurrentRoomIDPrefab, CurrentRoomIndex);
    // public static void PushEventRoom(string eventName, int chapterId, int levelId, int roomId, int roomIndex)
    // {
    //     try
    //     {
    //         Firebase.Analytics.FirebaseAnalytics.LogEvent(
    //         eventName,
    //         new Firebase.Analytics.Parameter[] {
    //         new Firebase.Analytics.Parameter(
    //         "chapterId", chapterId),
    //         new Firebase.Analytics.Parameter(
    //         "levelId", levelId),
    //         new Firebase.Analytics.Parameter(
    //         "roomId", roomId),
    //         new Firebase.Analytics.Parameter(
    //         "roomIndex", roomIndex),
    //         }
    //         );
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.Log("Push Event ERROR : " + e.ToString());
    //     }
    // }
    // public static void PushEventHeroInBattle(string eventName, string heroNameCode, int chapterId, int levelId, int roomId, int roomIndex)
    // {
    //     try
    //     {
    //         Firebase.Analytics.FirebaseAnalytics.LogEvent(
    //         eventName,
    //         new Firebase.Analytics.Parameter[] {
    //         new Firebase.Analytics.Parameter(
    //         "heroNameCode", heroNameCode),
    //         new Firebase.Analytics.Parameter(
    //         "chapterId", chapterId),
    //         new Firebase.Analytics.Parameter(
    //         "levelId", levelId),
    //         new Firebase.Analytics.Parameter(
    //         "roomId", roomId),
    //         new Firebase.Analytics.Parameter(
    //         "roomIndex", roomIndex),
    //         }
    //         );
    //     }
    //     catch (Exception e)
    //     {
    //     }
    // }

}

public enum EVENT_TRACKING
{
    stamina_show,
    stamina_ruby,
    stamina_buy,
    stamina_ads_click,
    stamina_ads_complete,
    shop_item_click,
    game_start,
    game_resume,
    game_quit,
    game_pause,
    screen_view,
    scene_shop,
    hero_die,
    sbg,
    level_quit, /* Thoat giua chung level */
    level_complete,
    room_start,
    room_clear, /* Giet het quai trong room */
    room_end, /* Cham vao Portal de sang room moi */
    revive_show,
    revive_gem_to_shop,
    revive_gem,
    revive_gem_not_enough,
    revive_free,
    revive_cancel,
    revive_ads_click,
    revive_ads_complete,
    reroll_ruby,
    reroll_ad,
    item_upgrade,
    first_open,
    first_iap,
    open_camp_scene,
    open_detail_camp_scene,
    click_play_menu_scene,
    click_play_camp_scene,
    
    ads_x2_reward_click,
    ads_x2_reward_complete,
}