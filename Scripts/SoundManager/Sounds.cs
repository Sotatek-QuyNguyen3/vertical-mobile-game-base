using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sounds : Singleton<Sounds>
{
    public static bool IgnorePopupShow = false;
    public static bool IgnorePopupClose = false;

    [Header("Music")]
    public AudioClip Music_Main;

    [Space(10)]
    [Header("Sound")]
    public AudioClip Sfx_Show_Popup;
    public AudioClip Sfx_Hide_Popup;
    public AudioClip Sfx_Btn_Click;
    public AudioClip Sfx_Btn_Click_Disabled;
    public AudioClip Sfx_Collect_Claim;
    public AudioClip Sfx_Collect_Claim_2;
    public AudioClip Sfx_Select;
    public AudioClip Sfx_Equip;
    public AudioClip Sfx_UnEquip;
    public AudioClip Sfx_Normal_Atk;
    public AudioClip Sfx_Buff_Heal;
    public AudioClip Sfx_Get_Hit;
    public AudioClip Sfx_Die;
    public AudioClip Sfx_Complete_Room;
    public AudioClip Sfx_Run;
    public AudioClip Sfx_Jump;
    public AudioClip Sfx_Landing;
    public AudioClip Sfx_Message_Toast;
    public AudioClip Sfx_Special_Skill;
    public AudioClip Sfx_Battle_BG;
    public AudioClip Sfx_Battle_Lost;
    public AudioClip Sfx_Battle_Win;

    [Space(10)]
    [Header("Volume")]
    public int sfx_music_main;
    public int sfx_music_battle;

    public float sfxGameVolume{
        get{
            return PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
        set{
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }

    public float musicGameVolume{
        get{
            return PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
        set{
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }


    /* Hellmade.Sound.EazySoundManager.PlaySound(Sounds.Instance.Sfx_Show_Popup); */
    private void Start()
    {
        // sfx_music_main = EazySoundManager.PlayMusic(Music_Main, 0.5f, true, true);
        PlayMusic(Music_Main);

        float musicVolume = PlayerPrefs.GetFloat(GameConstants.IS_MUSIC_ON, 1f);
        float soundVolume = PlayerPrefs.GetFloat(GameConstants.IS_SOUND_ON, 1f);

        EazySoundManager.GlobalMusicVolume = musicVolume;
        EazySoundManager.GlobalSoundsVolume = soundVolume;
        EazySoundManager.GlobalUISoundsVolume = soundVolume;
    }

    Coroutine _coroutineSound;


    public void StopSound()
    {
        if (_coroutineSound != null)
        {
            StopCoroutine(_coroutineSound);
            _coroutineSound = null;
        }
    }

    public void PlaySound(AudioClip clip){
        EazySoundManager.PlaySound(clip, sfxGameVolume);
    }

    public void PlayMusic(AudioClip clip){
        EazySoundManager.PlayMusic(clip, musicGameVolume, true, true);
    }

    public void ChangeSFXVolumn(float changedVolume){
        sfxGameVolume = changedVolume;
        EazySoundManager.GlobalSoundsVolume = changedVolume;
    }

    public void ChangeMusicVolumn(float changedVolume){
        musicGameVolume = changedVolume;
        EazySoundManager.GlobalMusicVolume = changedVolume;
    }
}