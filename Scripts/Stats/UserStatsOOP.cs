using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserStatsOOP
{
    [Header("Power")]
    public int power;
    public int powerUpgradePoint = 0;

    [Header("Stamina")]
    public int stamina;
    public int staminaUpgradePoint = 0;

    [Header("Energy")]
    public int energy;
    public int energyUpgradePoint = 0;

    [Header("Technique")]
    public float perfectHitZone;
    public float goodHitZone; // (perfect hit * 2)
    public int perfectHitZoneUpgradePoint = 0;

    //public int attackSpeedBase = 2; // 2s
    //[Header("Attack Speed")]
    //public float attackSpeedUpgradePoint;
    //private float attackSpeed;

    public int pointsUsed;
}
