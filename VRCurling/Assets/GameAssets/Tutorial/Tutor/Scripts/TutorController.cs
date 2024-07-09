using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorController : MonoBehaviour

{   
    public UnityEvent OnTutorialEnd;
    public DAS_DialogueSystem dialogueSystem;
    [ContextMenu("StartTutorial")]
    public void StartTutorial()
    {
        dialogueSystem.startDialogue(); // 5/19 威綮 added this line, to start the tutorial
    }
}
