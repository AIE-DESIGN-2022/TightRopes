using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> blakeRanSoundsOriginal;
    private List<AudioClip> blakeRanSounds;

    private float blakeRanSoundTime;
    private float blakeRanSoundTimer;

    public int blakeRanSoundTimerMax;
    public int blakeRanSoundTimerMin;

    public bool blakeRanSoundEnabled;

    private bool audioIsPLaying;

    private AudioClip blakeSound;

    private AudioSource blakeSoundSource;

    public GameObject blakePlayer;
    // Start is called before the first frame update
    void Start()
    {
        blakeSoundSource = blakePlayer.GetComponent<AudioSource>();

        blakeRanSoundEnabled = true;

        blakeRanSounds = blakeRanSoundsOriginal;

        blakeRanSoundTimer = blakeRanSoundTime;

        audioIsPLaying = false;

        BlakeRanSoundReset();
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
        //Debug.Log(blakeRanSoundTimer);
    }

    public void LateUpdate()
    {
        if (audioIsPLaying)
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
        audioIsPLaying = true;
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
}
