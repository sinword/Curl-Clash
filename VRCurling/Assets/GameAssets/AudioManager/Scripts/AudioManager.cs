

using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class AudioManager: SingletonWithoutDestroy<AudioManager>{

    public float GlobalVolume = 1.0f;
    public float MusicVolume = 1.0f;
    public float SoundVolume = 1.0f;
    private HashSet<GameObject> soundObjects = new HashSet<GameObject>();
    private HashSet<GameObject> musicObjects = new HashSet<GameObject>();
    public void PlaySound(AudioClip clip, float volume = 1.0f, Transform parent = null){
        if(clip == null) throw new System.Exception("Audio clip is null");
        if(volume < 0.0f || volume > 1.0f) throw new System.Exception("Volume is out of range");
        GameObject soundObject = new GameObject("Sound");
        if(parent != null) soundObject.transform.parent = parent;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume * SoundVolume * GlobalVolume;
        audioSource.Play();
        soundObjects.Add(soundObject);
        DestroyAudioObjectsIn(soundObject, (int)(clip.length * 1000));
    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f, bool loop = true, Transform parent = null){
        if(clip == null) throw new System.Exception("Audio clip is null");
        if(volume < 0.0f || volume > 1.0f) throw new System.Exception("Volume is out of range");
        GameObject musicObject = new GameObject("Music");
        if(parent != null) musicObject.transform.parent = parent;
        AudioSource audioSource = musicObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume * MusicVolume * GlobalVolume;
        audioSource.loop = loop;
        audioSource.Play();
        musicObjects.Add(musicObject);
        if(!loop) DestroyAudioObjectsIn(musicObject, (int)(clip.length * 1000));
    }

    public void StopAllSound(){
        foreach(GameObject sound in soundObjects){
            if(sound != null) Destroy(sound);
        }
        soundObjects.Clear();
    }

    public void StopAllMusic(){
        foreach(GameObject music in musicObjects){
            if(music != null) Destroy(music);
        }
        musicObjects.Clear();
    }

    private async void DestroyAudioObjectsIn(GameObject audio, int milliseconds){
        await UniTask.Delay(milliseconds);
        if (soundObjects.Contains(audio)) soundObjects.Remove(audio);
        if (musicObjects.Contains(audio)) musicObjects.Remove(audio);
        Destroy(audio);
    }

}