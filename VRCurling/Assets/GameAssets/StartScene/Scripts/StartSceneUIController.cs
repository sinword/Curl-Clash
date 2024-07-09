using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUIController : MonoBehaviour
{   
    private Button[] levelButtons;
    void Start()
    {
        AudioClip UiAudio = Resources.Load<AudioClip>("Audio/UI");
        UiAudio.PlayMusic(volume: 0.8f, parent: transform);
        levelButtons = GetComponentsInChildren<Button>();
    }
    public void EnterLevelScene(int levelIndex)
    {
        GameManager.Instance.EnterLevelScene(levelIndex);
        foreach (var button in levelButtons)
        {
            button.interactable = false;
        }
    }
}
