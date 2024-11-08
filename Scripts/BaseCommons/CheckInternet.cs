using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternet : Singleton<CheckInternet>
{
    float TimeDelay = 1;
    float TimeCount;
    bool IsNotInternet = false;
    // Update is called once per frame
    void Update()
    {
        if(TimeCount>0)
        {
            TimeCount -= Time.deltaTime;
        }
        else
        {
            TimeCount += TimeDelay;
            if(Application.internetReachability == NetworkReachability.NotReachable)
            {
                IsNotInternet = true;
                // Indicator.Show();
                //Show Indicator
            }
            else
            {
                if(IsNotInternet == true)
                {
                    IsNotInternet = false;
                    //Hide Indicator
                    // Indicator.Hide();
                }
            }
        }
    }
}
