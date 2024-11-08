using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class RewardDataOOP
{
    public string id = ""; /* id of reward */
    public RewardType type = RewardType.Furniture; /* Reward thuộc kiểu gì */
    public RewardSource source = RewardSource.WinMatch3; /* Reward có được từ đâu */
    public int quantity = 1; /* Số lượng */
}

public enum RewardSource {
    Buy, Fundamental, Mission,WinMatch3
}