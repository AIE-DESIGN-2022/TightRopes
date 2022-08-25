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

    private bool blakeRanAudioIsPLaying;

    private bool crouchWalkPlaying;
    private bool walkPlaying;
    private bool proneWalkPlaying;

    private AudioClip blakeRanSound;
    private AudioClip blakeActionSound;


    public List<AudioClip> blakeCrouchWalkingSounds;
    public List<AudioClip> blakeProneWalkingSounds;
    public List<AudioClip> blakeWalkingSounds;
    public List<AudioClip> jumpSounds;
    public List<AudioClip> vaultSounds;
    public List<AudioClip> climbSounds;


    private AudioSource blakeRanSoundSource;
    private AudioSource blakeActionSoundSource;

    public GameObject blakePlayer;

    public GameObject blake;

    private Animator blakeAnimator;

    [Header("NotMonster Sound")]

    public List<AudioClip> notmonsterRanSoundsOriginal;
    public List<AudioClip> notmonsterRanSounds;

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
        walkPlaying = false;

        proneWalkPlaying = false;

        crouchWalkPlaying = false;

        blakeAnimator = blake.GetComponent<Animator>();

        blakeActionSoundSource = blake.GetComponent<AudioSource>();

        blakeRanSoundSource = blakePlayer.GetComponent<AudioSource>();

        blakeRanSoundEnabled = true;

        blakeRanSounds = new List<AudioClip>(blakeRanSoundsOriginal);

        blakeRanSoundTimer = blakeRanSoundTime;

        blakeRanAudioIsPLaying = false;

        BlakeRanSoundReset();

        notmonsterSoundSource = notmonster.GetComponent<AudioSource>();

        notmonsterRanSoundEnabled = true;

        notmonsterRanSounds = new List<AudioClip>(notmonsterRanSoundsOriginal);

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

        if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running Jump"))
        {
            BlakeSounds("Jump");
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Vault"))
        {
            BlakeSounds("Vault");
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Climb"))
        {
            BlakeSounds("Climb");
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Standing torch idle 0 1"))
        {
            PlayBlakeActionAudio(null, null);
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Crouch Idle 0"))
        {
            PlayBlakeActionAudio(null, null);
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Prone Idle 0"))
        {
            PlayBlakeActionAudio(null, null);
            crouchWalkPlaying = false;
            proneWalkPlaying = false;
            walkPlaying = false;
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Crouch Walk 0"))
        {
            if (!crouchWalkPlaying)
            {
                crouchWalkPlaying = true;
                proneWalkPlaying = false;
                walkPlaying = false;
                BlakeSounds("Crouch Walking");
            }
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Crawl 0"))
        {
            if (!proneWalkPlaying)
            {
                crouchWalkPlaying = false;
                proneWalkPlaying = true;
                walkPlaying = false;
                BlakeSounds("Prone Walking");
            }
        }
        else if (blakeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Torch walking 0"))
        {
            if (!walkPlaying)
            {
                crouchWalkPlaying = false;
                proneWalkPlaying = false;
                walkPlaying = true;
                BlakeSounds("Walking");
            }
        }
    }

    public void LateUpdate()
    {
        if (blakeRanAudioIsPLaying)
        {
            if (!blakeRanSoundEnabled)
            {
                if (!blakeRanSoundSource.isPlaying)
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

            blakeRanSound = blakeRanSounds[rand];
            PlayBlakeRanAudio(blakeRanSound);
            blakeRanSounds.Remove(blakeRanSounds[rand]);
        }
        else
        {
            BlakeRanSoundListReset();
        }
    }

    public void BlakeRanSoundListReset()
    {
        blakeRanSounds = new List<AudioClip>(blakeRanSoundsOriginal);
        BlakeRanSound();
    }

    public void BlakeRanSoundReset()
    {
        int rand = Random.Range(blakeRanSoundTimerMin, blakeRanSoundTimerMax);
        
        blakeRanSoundTime = rand;

        blakeRanSoundTimer = blakeRanSoundTime;
    }

    public void PlayBlakeRanAudio(AudioClip sound)
    {
        blakeRanSoundSource.clip = sound;
        blakeRanSoundSource.Play();
        blakeRanAudioIsPLaying = true;
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

    public void BlakeSounds(string action)
    {
        if(action == "Crouch Walking")
        {
            int rand = Random.Range(0, blakeCrouchWalkingSounds.Count);

            blakeActionSound = blakeCrouchWalkingSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
        else if (action == "Prone Walking")
        {
            int rand = Random.Range(0, blakeProneWalkingSounds.Count);

            blakeActionSound = blakeProneWalkingSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
        else if (action == "Walking")
        {
            int rand = Random.Range(0, blakeWalkingSounds.Count);

            blakeActionSound = blakeWalkingSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
        else if (action == "Jump")
        {
            int rand = Random.Range(0, jumpSounds.Count);

            blakeActionSound = jumpSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
        else if (action == "Vault")
        {
            int rand = Random.Range(0, vaultSounds.Count);

            blakeActionSound = vaultSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
        else if (action == "Climb")
        {
            int rand = Random.Range(0, climbSounds.Count);

            blakeActionSound = climbSounds[rand];
            PlayBlakeActionAudio(blakeActionSound, action);
        }
    }

    public void PlayBlakeActionAudio(AudioClip sound, string action)
    {
        if(sound == null)
        {
            blakeActionSoundSource.clip = null;
        }
        else if(action == "Jump" | action == "Climb" | action == "Vault")
        {
            blakeActionSoundSource.clip = sound;
            blakeActionSoundSource.loop = false;
            blakeActionSoundSource.Play();
        }
        else
        {
            blakeActionSoundSource.clip = sound;
            blakeActionSoundSource.loop = true;
            blakeActionSoundSource.Play();
        }
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
        notmonsterRanSounds = new List<AudioClip>(notmonsterRanSoundsOriginal);
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
