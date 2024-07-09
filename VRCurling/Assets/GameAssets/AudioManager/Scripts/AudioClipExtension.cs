using UnityEngine;


public static class AudioClipExtension
{
    public static void PlaySound(this AudioClip clip, float volume = 1.0f, Transform parent = null)
    {
        AudioManager.Instance.PlaySound(clip, volume, parent);
    }

    public static void PlayMusic(this AudioClip clip, float volume = 1.0f, bool loop = true, Transform parent = null)
    {
        AudioManager.Instance.PlayMusic(clip, volume, loop, parent);
    }
}