using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    public TextMeshProUGUI deviceIDText;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI userLevelText;
 

    private string device_id;
    private string user_name;
    private int user_level;
    private int upgradePoint;
  
    private UserStatsManager userStatsManager = new UserStatsManager();
    
    void Start()
    {
       this.Setup();
    }
        void Awake()
    {
        // Đảm bảo rằng chỉ có một instance duy nhất
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ instance giữa các scene (tùy chọn)
        }
        else
        {
            Destroy(gameObject); // Xóa nếu đã có một instance khác
        }
    }

    void Setup(){
        device_id = UserDataManager.Instance.UserData.device_id;
        deviceIDText.text = "Device ID " + device_id;
        user_name = UserDataManager.Instance.UserData.user_name;
        userNameText.text = "User name "+ user_name;
        user_level = UserDataManager.Instance.UserData.account_level;
        userLevelText.text = "Lvl "+ user_level.ToString();
      
    }
  
    void Update()
    {  
       
        
    }
     public void OnButtonLevelupClicked()
    {
        this.setLevel();
        userLevelText.text = "Lvl: "+ user_level.ToString();
    }
   
    public int getLevel(){
        return this.user_level;
    }
    public void setLevel(){
        this.user_level++;
    }
  
   
   

}
