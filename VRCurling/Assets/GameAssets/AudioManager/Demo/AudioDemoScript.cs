using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDemoScript : MonoBehaviour
{
    public AudioClip soundClip;
    public AudioClip musicClip;

    // show gui
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Play Sound"))
        {
            soundClip.PlaySound(0.8f);
            // AudioManager.Instance.PlaySound(soundClip, 0.8f); This is the same as the above line
        }

        if (GUI.Button(new Rect(10, 70, 100, 50), "Play Music"))
        {
            musicClip.PlayMusic(0.8f, true);
            // AudioManager.Instance.PlayMusic(musicClip, 0.8f, true); This is the same as the above line
        }

        if (GUI.Button(new Rect(10, 130, 100, 50), "Stop All Sound"))
        {
            AudioManager.Instance.StopAllSound();
        }

        if (GUI.Button(new Rect(10, 190, 100, 50), "Stop All Music"))
        {
            AudioManager.Instance.StopAllMusic();
        }


        AudioManager.Instance.GlobalVolume = GUI.HorizontalSlider(new Rect(10, 250, 100, 50), AudioManager.Instance.GlobalVolume, 0.0f, 1.0f);
        GUI.Label(new Rect(120, 250, 100, 50), "Global Volume");

        AudioManager.Instance.SoundVolume = GUI.HorizontalSlider(new Rect(10, 370, 100, 50), AudioManager.Instance.SoundVolume, 0.0f, 1.0f);
        GUI.Label(new Rect(120, 370, 100, 50), "Sound Volume");

        AudioManager.Instance.MusicVolume = GUI.HorizontalSlider(new Rect(10, 310, 100, 50), AudioManager.Instance.MusicVolume, 0.0f, 1.0f);
        GUI.Label(new Rect(120, 310, 100, 50), "Music Volume");

        
    }
}
