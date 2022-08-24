using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Blake Sound")]
    public List<AudioClip> blakeRanSoundsOriginal;
    private List<AudioClip> blakeRanSounds;

    private float blakeRanSoundTime;
    private float blakeRanSoundTimer;

    public int blakeRanSoundTimerMax;
    public int blakeRanSoundTimerMin;

    public bool blakeRanSoundEnabled;

    private bool blakeAudioIsPLaying;

    private AudioClip blakeSound;

    private AudioSource blakeSoundSource;

    public GameObject blakePlayer;

    [Header("NotMonster Sound")]

    public List<AudioClip> notmonsterRanSoundsOriginal;
    private List<AudioClip> notmonsterRanSounds;

    private float notmonsterRanSoundTime;
    private float notmonsterRanSoundTimer;

    public int notmonsterRanSoundTimerMax;
    public int notmonsterRanSoundTimerMin;

    public bool notmonsterRanSoundEnabled;

    private bool notmonsterAudioIsPLaying;

    private AudioClip notmonsterSound;

    private AudioSource notmonsterSoundSource;

    public GameObject notmonster;
    // Start is called before the first frame update
    void Start()
    {
        blakeSoundSource = blakePlayer.GetComponent<AudioSource>();

        blakeRanSoundEnabled = true;

        blakeRanSounds = blakeRanSoundsOriginal;

        blakeRanSoundTimer = blakeRanSoundTime;

        blakeAudioIsPLaying = false;

        BlakeRanSoundReset();

        notmonsterSoundSource = notmonster.GetComponent<AudioSource>();

        notmonsterRanSoundEnabled = true;

        notmonsterRanSounds = notmonsterRanSoundsOriginal;

        notmonsterRanSoundTimer = notmonsterRanSoundTime;

        notmonsterAudioIsPLaying = false;

        NotmonsterRanSoundReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (blakeRanSoundEnabled)
        {
            blakeRanSoundTimer -= Time.deltaTime;
            if(blakeRanSoundTimer <= 0)
            {
                blakeRanSoundEnabled = false;
                BlakeRanSound();
            }
        }

        if (notmonsterRanSoundEnabled)
        {
            notmonsterRanSoundTimer -= Time.deltaTime;
            if (notmonsterRanSoundTimer <= 0)
            {
                notmonsterRanSoundEnabled = false;
                NotmonsterRanSound();
            }
        }
    }

    public void LateUpdate()
    {
        if (blakeAudioIsPLaying)
        {
            if (!blakeRanSoundEnabled)
            {
                if (!blakeSoundSource.isPlaying)
                {
                    blakeRanSoundEnabled = true;
                    BlakeRanSoundReset();
                }
            }
        }

        if (notmonsterAudioIsPLaying)
        {
            if (!notmonsterRanSoundEnabled)
            {
                if (!notmonsterSoundSource.isPlaying)
                {
                    notmonsterRanSoundEnabled = true;
                    NotmonsterRanSoundReset();
                }
            }
        }
    }

    public void BlakeRanSound()
    {
        if(blakeRanSounds.Count > 0)
        {
            int rand = Random.Range(0, blakeRanSounds.Count);

            blakeSound = blakeRanSounds[rand];
            PlayBlakeAudio(blakeSound);
            blakeRanSounds.Remove(blakeRanSounds[rand]);
        }
        else
        {
            BlakeRanSoundListReset();
        }
    }

    public void BlakeRanSoundListReset()
    {
        blakeRanSounds = blakeRanSoundsOriginal;
        BlakeRanSound();
    }

    public void BlakeRanSoundReset()
    {
        int rand = Random.Range(blakeRanSoundTimerMin, blakeRanSoundTimerMax);
        
        blakeRanSoundTime = rand;

        blakeRanSoundTimer = blakeRanSoundTime;
    }

    public void PlayBlakeAudio(AudioClip sound)
    {
        blakeSoundSource.clip = sound;
        blakeSoundSource.Play();
        blakeAudioIsPLaying = true;
    }

    public void DisableBlakeRanSound()
    {
        blakeRanSoundEnabled = false;
    }

    public void EnableBlakeRanSound()
    {
        blakeRanSoundEnabled = true;
        BlakeRanSoundReset();
    }








    public void NotmonsterRanSound()
    {
        if (notmonsterRanSounds.Count > 0)
        {
            int rand = Random.Range(0, notmonsterRanSounds.Count);

            notmonsterSound = notmonsterRanSounds[rand];
            PlayNotmonsterAudio(notmonsterSound);
            notmonsterRanSounds.Remove(notmonsterRanSounds[rand]);
        }
        else
        {
            NotmonsterRanSoundListReset();
        }
    }

    public void NotmonsterRanSoundListReset()
    {
        notmonsterRanSounds = notmonsterRanSoundsOriginal;
        NotmonsterRanSound();
    }

    public void NotmonsterRanSoundReset()
    {
        int rand = Random.Range(notmonsterRanSoundTimerMin, notmonsterRanSoundTimerMax);

        notmonsterRanSoundTime = rand;

        notmonsterRanSoundTimer = notmonsterRanSoundTime;
    }

    public void PlayNotmonsterAudio(AudioClip sound)
    {
        notmonsterSoundSource.clip = sound;
        notmonsterSoundSource.Play();
        notmonsterAudioIsPLaying = true;
    }

    public void DisableNotmonsterRanSound()
    {
        notmonsterRanSoundEnabled = false;
    }

    public void EnableNotmonsterRanSound()
    {
        notmonsterRanSoundEnabled = true;
        NotmonsterRanSoundReset();
    }
}
