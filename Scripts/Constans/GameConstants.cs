using UnityEngine;

public static class GameConstants
{
    public static string PREFS_USER_ID = "USER_ID";
    public static string IS_SOUND_ON = "IS_SOUND_ON";
    public static string IS_MUSIC_ON = "IS_MUSIC_ON";
    public static string OPEN_LEVEL = "OpenLevel";
    
    
    public const int centerPositionX = 1;
    public const int centerPositionY = 1;
    
    public static Quaternion flippedRotation = Quaternion.Euler(0, 180f, 0); /* Quay nhân vật sang trái */
    public static Vector3 flippedScale = new Vector3(-1f, 1f, 1f);
    public static Vector3 normalScale = new Vector3(1f,1f,1f);
    
    public static int TYPE_ENDPOINT_DEV = 0;
    public static int TYPE_ENDPOINT_PRE_PROD = 1;
    public static int TYPE_ENDPOINT_MAINET = 2;

    public static int typeEndpoint = TYPE_ENDPOINT_DEV;

    public static string HOST
    {
        get { return CalculatorHost(); }
    }

    private static string CalculatorHost()
    {
        if (typeEndpoint == TYPE_ENDPOINT_DEV)
        {
            return HOST_DEV;
        }
        else if (typeEndpoint == TYPE_ENDPOINT_PRE_PROD)
        {
            return HOST_PREPROD;
        }
        else if (typeEndpoint == TYPE_ENDPOINT_MAINET)
        {
            return HOST_MAIN;
        }
        else
        {
            return HOST_DEV;
        }
    }
    
    public const string HOST_DEV = "https://heroes-of-lighthalzen.bunicorn.game/api/"; // API Official dev new 27/6/23

    public const string HOST_PREPROD = "https://heroes-of-lighthalzen.bunicorn.game/api/staging-"; // API Testnet release
    
    public const string HOST_MAIN =
        "https://api-prod-heroes-of-lighthalzen.bunicorn.game/api/"; // API Official Mainnet for External Test use Dev domain

    public const string HOST_API = "https://heroes-of-lighthalzen.bunicorn.game/dev/api/thalia-home/";

}