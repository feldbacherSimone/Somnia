using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public static class SoundManager 
{
    public enum Sound
    {
       PuzzleInteract, 
       PuzzleSolve, 
       PlayerWalk, 
       PlayerLand,
       PlayerJump,

       MenuButton, 
       PlayerWalkGrass,

       TitleFade,
       DisableBarrier, 
    }

    public enum Mixer //there's only two channels at this point but if i have to debug for 30 minutes just because I made a typo again one more time im gonna dlelete this project 
    {
        SFX, 
        Music, 
    }


    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static GameObject stepSoundObject;
    private static AudioSource stepSource;

    private static GameObject DelaySource; 

    private static AudioMixer audioMixer; 

    public static void LoadMixer()
    {
        audioMixer = Resources.Load<AudioMixer>("Mixer");
        Debug.Log("loadedMixer");
    }

    public static void PlaySound(Sound sound, Mixer mixer)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            AssignMixer(mixer, oneShotAudioSource);
            oneShotAudioSource.volume = GetVolume(sound);
           
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static void PlaySound(Sound sound, Vector3 pos, Mixer mixer)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = pos; 
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        AssignMixer(mixer, audioSource);
        audioSource.clip = GetAudioClip(sound);
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 10;
        audioSource.dopplerLevel = 0;
        audioSource.volume = GetVolume(sound); 
        audioSource.Play();


        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static void PlaySound(Sound sound, Vector3 pos, float volume, Mixer mixer)
    {
        if (stepSoundObject == null)
        {
            stepSoundObject = new GameObject("Sound");
            stepSoundObject.transform.position = pos;
            stepSource = stepSoundObject.AddComponent<AudioSource>();

            AssignMixer(mixer, stepSource);

            stepSource.clip = GetAudioClip(sound);
            stepSource.spatialBlend = 1;
            stepSource.maxDistance = 10;
            stepSource.dopplerLevel = 0;
            stepSource.volume = volume;
            stepSource.Play();
            Object.Destroy(stepSoundObject, stepSource.clip.length);
        }
 
     
    }

    public static float PlayAndWait(Sound sound, Vector3 pos, Mixer mixer)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = pos;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        AssignMixer(mixer, audioSource);

        audioSource.clip = GetAudioClip(sound);
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 10;
        audioSource.dopplerLevel = 0;

        audioSource.Play();

        Object.Destroy(soundGameObject, audioSource.clip.length);
        return audioSource.clip.length;
    }


     



    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach( GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip[Random.Range(0, soundAudioClip.audioClip.Length - 1)]; 
            }


        }
        Debug.LogError("you fucked up bro, there's no" + sound + "audioClip");
        return null; 
    }
    private static float GetVolume(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.volume;
            }


        }
        Debug.LogError("you fucked up bro, there's no" + sound + "Volume");
        return 1;
    }

    private static void AssignMixer(Mixer mixer, AudioSource source)
    {
        if (audioMixer != null          )
            source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixer.ToString())[0]; 
        else
            Debug.LogError("You Dumb Fuck, there is no Mixer to Accesess");
    }
}
