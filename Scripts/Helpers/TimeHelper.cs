using UnityEngine;
using System;

public static class TimeHelper
{
    public static string TimeFormat(float timeToDisplay)
    {
        float hour = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt((timeToDisplay - hour * 3600) / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        return $"{hour:00}:{minutes:00}:{seconds:00}";
    }
    
    public static long DateTimeToUnixSeconds(DateTime time)
    {
        return ((DateTimeOffset)time).ToUnixTimeSeconds();
    }
    public static long DateTimeToUnixMilliseconds(DateTime time)
    {
        return ((DateTimeOffset)time).ToUnixTimeMilliseconds();
    }

    public static int Percentage(int value, int percent)
    {
        return (int) (value * percent * 1.0 / 100);
    }
    #region To days
    public static double ConvertMillisecondsToDays(double milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds).TotalDays;
    }

    public static double ConvertSecondsToDays(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).TotalDays;
    }

    public static double ConvertMinutesToDays(double minutes)
    {
        return TimeSpan.FromMinutes(minutes).TotalDays;
    }

    public static double ConvertHoursToDays(double hours)
    {
        return TimeSpan.FromHours(hours).TotalDays;
    }
    #endregion

    #region To hours
    public static double ConvertMillisecondsToHours(double milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds).TotalHours;
    }

    public static double ConvertSecondsToHours(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).TotalHours;
    }

    public static double ConvertMinutesToHours(double minutes)
    {
        return TimeSpan.FromMinutes(minutes).TotalHours;
    }

    public static double ConvertDaysToHours(double days)
    {
        return TimeSpan.FromHours(days).TotalHours;
    }
    #endregion

    #region To minutes
    public static double ConvertMillisecondsToMinutes(double milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes;
    }

    public static double ConvertSecondsToMinutes(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).TotalMinutes;
    }

    public static double ConvertHoursToMinutes(double hours)
    {
        return TimeSpan.FromHours(hours).TotalMinutes;
    }

    public static double ConvertDaysToMinutes(double days)
    {
        return TimeSpan.FromDays(days).TotalMinutes;
    }
    #endregion

    #region To seconds
    public static double ConvertMillisecondsToSeconds(double milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds;
    }

    public static double ConvertMinutesToSeconds(double minutes)
    {
        return TimeSpan.FromMinutes(minutes).TotalSeconds;
    }

    public static double ConvertHoursToSeconds(double hours)
    {
        return TimeSpan.FromHours(hours).TotalSeconds;
    }

    public static double ConvertDaysToSeconds(double days)
    {
        return TimeSpan.FromDays(days).TotalSeconds;
    }
    #endregion

    #region To milliseconds
    public static double ConvertSecondsToMilliseconds(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
    }

    public static double ConvertMinutesToMilliseconds(double minutes)
    {
        return TimeSpan.FromMinutes(minutes).TotalMilliseconds;
    }

    public static double ConvertHoursToMilliseconds(double hours)
    {
        return TimeSpan.FromHours(hours).TotalMilliseconds;
    }

    public static double ConvertDaysToMilliseconds(double days)
    {
        return TimeSpan.FromDays(days).TotalMilliseconds;
    }
    #endregion

    #region DateTime To Second
    public static long ConvertDatetimeToSecond(DateTime dateTime)
    {
        DateTime myDate1 = new DateTime(1970, 1, 9, 0, 0, 00);
        DateTime myDate2 = dateTime;
        TimeSpan myDateResult;
        myDateResult = myDate2 - myDate1;
        long seconds = (long)myDateResult.TotalSeconds;
        return seconds;
    }

    public static void CheckTimeDemo()
    {
        DateTime myDate1 = new DateTime(1970, 1, 9, 0, 0, 00);
        DateTime myDate2 = DateTime.Now;
        TimeSpan myDateResult;
        myDateResult = myDate2 - myDate1;
        long seconds = (long)myDateResult.TotalSeconds;
        Debug.Log(seconds);
    }
    #endregion

    #region Second To datetime
    public static string ConverSecondtoDate(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        long a = second / 60;
        if (a >= 60)
        {
            answer = string.Format("{0:D1}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds
            );
        }
        else
        {
            answer = string.Format("{0:D1} : {1:D2}",
            t.Minutes,
            t.Seconds);

        }
        return answer;
    }

    public static string ConverSecondtoDateSpace(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        long a = second / 60;
        if (a >= 60)
        {
            answer = string.Format("{0:D1} : {1:D2} : {2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds
            );
        }
        else
        {
            answer = string.Format("{0:D1} : {1:D2}",
            t.Minutes,
            t.Seconds);

        }
        return answer;
    }

    public static string ConverSecondtoDate2(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        long a = second / 60;
        if (a >= 60)
        {
            answer = string.Format("{0:D1}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds
            );
        }
        else
        {
            answer = string.Format("{0:D1}:{1:D2}",
            t.Minutes,
            t.Seconds);

        }
        return answer;
    }

    public static string ConverSecondtoDate3(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        long a = second / 60;
        if (a >= 60)
        {
            answer = string.Format("{0:D1}:{1:D2}",
            t.Hours,
            t.Minutes
            );
        }
        return answer;
    }

    public static string ConverSecondtoDate4(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        long a = second / 60;
        if (a >= 1440)
        {
            answer = string.Format("{0:D1}d:{1:D2}h:{2:D2}m",
            t.Days,
            t.Hours,
            t.Minutes
            );
        }
        else if (a >= 60)
        {
            if (a % 60 > 0)
            {
                answer = string.Format("{0:D1}h:{1:D2}m",
                t.Hours,
                t.Minutes);
            }
            else
            {
                answer = string.Format("{0:D1}h",
                t.Hours);
            }
        }
        else
        {
            if (a % 60 > 0)
            {
                answer = string.Format("{0:D1}m:{1:D2}s",
                t.Minutes,
                t.Seconds);
            }
            else
            {
                answer = string.Format("{0:D1}s",
                t.Seconds);
            }
        }

        return answer;
    }

    public static string ConverSecondtoHour(long second)
    {
        string answer = "";
        TimeSpan t = TimeSpan.FromSeconds(second);
        if (second < 86400)
        {
            answer = string.Format("{0}h",
                  t.Hours
                  );
        }
        else
        {
            answer = string.Format("{0}d:{1}h",
                  t.Days,
                  t.Hours
                  );
        }

        return answer;
    }
    #endregion

    public static void Example()
    {
        // 500000 milliseconds = 0.00578703704 days
        Debug.Log(ConvertMillisecondsToDays(500000));
        // 100 hours = 6000 minutes
        Debug.Log(ConvertHoursToMinutes(100));

        // 10000 days = 240000 hours
        Debug.Log(ConvertDaysToHours(10000));

        // 500 minutes = 8.33333333 hours
        Debug.Log(ConvertMinutesToHours(500));

        // 600000 milliseconds = 600 seconds
        Debug.Log(ConvertMillisecondsToSeconds(600000));
    }
}
