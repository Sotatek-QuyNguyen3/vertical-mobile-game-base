using System;
using UnityEngine;

[Serializable]
public class GameDataOOP
{
    // base stats
    [Header("Base Stats")]
    public int basePower = 10;
    public int baseStamina = 1000;
    public int baseStaminaRecovery = 10;
    public int baseEnergy = 100;
    public float basePerfectHitZone = 0.1f;
}
