using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class APIGet
{
    public static string APIGetAllTrainers()
    {
        return string.Format("{0}islands/v2/my/trainers/in-game?limit=100", GameConstants.HOST);
    }

    public static string APIGetBunicorns()
    {
        string url = string.Format("{0}islands/v2/my/bunicorns/in-game?limit=100", GameConstants.HOST);
        return url;
    }



    public static string APIGetListBunicornDetails(List<string> ids)
    {
        string url = string.Format("{0}islands/my/bunicorns/list-details?bunicornIds=", GameConstants.HOST);
        string idsUrl = "";
        while (ids.Count > 0)
        {
            idsUrl += ids + ",";
        }
        url += UnityWebRequest.EscapeURL(idsUrl);
        return url;

    }



}
