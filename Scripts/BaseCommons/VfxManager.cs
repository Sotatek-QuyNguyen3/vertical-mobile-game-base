using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class VfxManager : Singleton<VfxManager>
{
    /* Lưu trữ những effect đã sử dụng trong game */
    public Dictionary<VfxName, GameObject> effectList = new();

    public string EFFECT_PREFAB = "/";

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public ParticleSystem EnableEffect(VfxName vfxName, Vector3 position, float duration = 0)
    {
        if (!effectList.ContainsKey(vfxName))
        {
            AddEffect(vfxName);
        }

        var effect = LeanPool.Spawn(effectList[vfxName]).GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.gameObject.SetActive(true);

        LeanPool.Despawn(effect, duration == 0 ? effect.main.duration : duration);
        return effect;
    }

    public ParticleSystem EnableEffect(VfxName vfxName, Transform parent, Vector3 localPosition, Vector3 offset,
        float duration = 0)
    {
        if (!effectList.ContainsKey(vfxName))
        {
            AddEffect(vfxName);
        }

        var effect = LeanPool.Spawn(effectList[vfxName], parent).GetComponent<ParticleSystem>();
        Quaternion newRotation = Quaternion.Euler(180f, parent.rotation.y, 180f + effect.transform.rotation.z);
        // effect.transform.rotation = newRotation;
        effect.transform.localPosition = localPosition + offset;
        effect.gameObject.SetActive(true);

        LeanPool.Despawn(effect, duration == 0 ? effect.main.duration : duration);
        return effect;
    }
    

    private void AddEffect(VfxName vfxName)
    {
        GameObject newEffect =
            AssetManager.Instance.LoadResourcesByPath(string.Format(EFFECT_PREFAB, vfxName));
        effectList.Add(vfxName, newEffect);
    }

    private void OnChangeRoom(Dictionary<string, object> msg)
    {
        LeanPool.DespawnAll();
    }
}

public enum VfxSkillName
{
    Slash_Effect_InAir_01,
}

public enum VfxName
{
    Heal_Effect,
}