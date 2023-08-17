using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public AudioMixerGroup musicMixer, sfxMixer;

    [Header("Sounds")]
    public int swordWoosh;
    public int checkPoint;
    public int hurtEnemy;
    public int getHit;
    public int getHitShield;
    public int enemySword;
    public int footstepsGrass;
    public int heart;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayMusic(0);
    }


    void Update()
    {

    }

    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("music", UIManager.instance.musicVolSlider.value);
        if(UIManager.instance.musicVolSlider.value == UIManager.instance.musicVolSlider.minValue)
        {
            musicMixer.audioMixer.SetFloat("music", -80);
        }
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("sfxvol", UIManager.instance.sfxVolSlider.value);
        if (UIManager.instance.sfxVolSlider.value == UIManager.instance.sfxVolSlider.minValue)
        {
            musicMixer.audioMixer.SetFloat("music", -80);
        }
    }
}
