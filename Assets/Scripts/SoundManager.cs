using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioSource audioSourceSE;

    [SerializeField] AudioClip[] audioClipBGM;

    [SerializeField] AudioClip[] audioClipSE;

    
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayBGM()
    {
        audioSourceBGM.clip = audioClipBGM[0];
        audioSourceBGM.Play();
    }
    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }
    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(audioClipSE[(int)se]);
    }
}


public enum SE
{
    Button,
    Damage,
    GameClear,
    GameOver,
    LvUp,
    Xp,
    DonWeapon,
    GunWeapon,
    WaterWeapon,
    WindWeapon,
}