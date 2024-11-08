using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;
using Newtonsoft.Json.Linq;


public partial class NetworkManager : Singleton<NetworkManager>
{

    private string _accessToken = "";
    private long _expireTimeAccessToken = 0;
    private string _refreshToken = "";
    private string _userId;
    private string testWallet = "0xC8b9298612Dc79d06D7609e685d84845F7681F61";
    private int _signStatus;
    public string walletAccount;

    float _countDown = 2;
    bool isLostInternet = false;
    bool isCheck = false;

    public string AccessToken
    {
        get { return _accessToken; }
        set { _accessToken = value; }
    }
    public long ExpireTimeAccessToken
    {
        get { return _expireTimeAccessToken; }
        set { _expireTimeAccessToken = value; }
    }

    public string RefreshToken
    {
        get { return _refreshToken; }
        set { _refreshToken = value; }
    }

    public int SigninStatus
    {
        get { return _signStatus; }
        set { _signStatus = value; }
    }

    private void Start()
    {
        Application.runInBackground = true;
    }



    #region function request




    #endregion

}



