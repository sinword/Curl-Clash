using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Message
{
    public string title;
    public VideoClip videoClip;
    [TextArea(3, 10)]
    public string message;
}
