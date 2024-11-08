
using UnityEngine;

public class UserStatsManager : Singleton<UserStatsManager>
{
    [SerializeField] private UserStatsOOP userStats = new UserStatsOOP();
    public UserStatsOOP UserStats
    {
        get => userStats;
        set => userStats = value;
    }
    private void Start()
    {
        if (userStats == null)
        {
            userStats = new UserStatsOOP();
        }

        PlayerPrefsUtil.LoadUserStatsData();
    }
}
