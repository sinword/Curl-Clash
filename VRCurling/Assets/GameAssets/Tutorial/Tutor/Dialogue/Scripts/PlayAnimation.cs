//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayAnimation : DAS_Action
{   
    public DAS_Dialogue dialogue;
    public DAS_DialogueSystem dialogueSystem;
    public TutorController tutorController;
    public GameObject effect;
    public Animator animator;
    int slideHash = Animator.StringToHash("Base Layer.slide");
    int cheerHash = Animator.StringToHash("Base Layer.Cheer");
       public override void execute()
    {
        // in this example, there is still dialogues after the animation
        // dialoguesystem.active = true will keep the dialogue active,
        // so the user dont open the dialogue again while animation is playing
       

        dialogueSystem.active = true;
        dialogueSystem.dialogueController.dialogueInCourse = true;
        effect.SetActive(true);
        // GetComponent<AudioSource>().Play();
        Debug.Log(DAS_DialogueSystem.currentDialogueIndex);
     


        if (DAS_DialogueSystem.currentDialogueIndex == 0) // say hello diag1
            {
                animator.SetTrigger("happy");
            }

        if (DAS_DialogueSystem.currentDialogueIndex == 1) // say hello diag1
            {
                animator.SetTrigger("talk");
            }
        if (DAS_DialogueSystem.currentDialogueIndex == 3) // say hello diag1
            {
                animator.SetTrigger("talk");
            }
         if (DAS_DialogueSystem.currentDialogueIndex == 6) // fighting   diag9
            {
                animator.SetTrigger("ready");
            }
        
        if (DAS_DialogueSystem.currentDialogueIndex == 7) // slide and throw the curling stone.  diag7
            {

                animator.SetTrigger("slide");
                
                StartCoroutine(CheckAnimationState("slide"));
                

                
                // AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 代表第一層
                // Debug.Log(stateInfo);
                // if (stateInfo.IsName("slide") && stateInfo.normalizedTime >= 1.0f) {
                //     // normalizedTime >= 1.0f 表示動畫已經播放完畢
                   
                // }


                // // 終點位置和旋轉設定
                // Vector3 EndPos = new Vector3(-16f, 4.7f, 10f); // 終點位置
                // Vector3 EndRotation = new Vector3(0f, 180f, 0f); // 終點旋轉
                // float movetime = 10f; // 移動所需時間

                // transform.DOComplete(); // 結束所有正在進行的 DOTween 動畫
                // transform.DOMove(EndPos, movetime); // 從起點移動到終點
                // transform.DORotate(EndRotation, movetime); // 從起始旋轉到終點旋轉
          
           }

        if (DAS_DialogueSystem.currentDialogueIndex == 8) // fighting   diag9
            {
                animator.SetTrigger("cheer");
            }

        if (DAS_DialogueSystem.currentDialogueIndex == 9) // clapping   diag10
            {
                animator.SetTrigger("clapp");
            }
        
        if (DAS_DialogueSystem.currentDialogueIndex == 12) // sweep diag13
            {
                animator.SetTrigger("sweep");
            }
           

        if (DAS_DialogueSystem.currentDialogueIndex == 13) // clapping diag14
            {
                animator.SetTrigger("clapp");
            }
        
        if (DAS_DialogueSystem.currentDialogueIndex == 14) // bye diag15
            {
                animator.SetTrigger("bye");
                

            }
            
     
      
        Invoke("callNextDialogue", 1f);
    }

    private void callNextDialogue()
    {
        effect.SetActive(false);
        dialogueSystem.callSpecificDialogue(dialogue);
        // dialogueSystem.nextDialogue();  
        
    }
    private IEnumerator CheckAnimationState(string animationName)
    {
        // Get the current state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        // Wait until the specified animation is playing
        while (!stateInfo.IsName(animationName))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Waiting for animation to start: " + stateInfo.shortNameHash);
        }

        // Wait until the animation finishes playing
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Animation is playing. Normalized time: " + stateInfo.normalizedTime);
        }

        // Animation has finished playing
        Debug.Log(animationName + " animation finished.");

        // 終點位置和旋轉設定
        Vector3 EndPos = new Vector3(-16f, 4.7f, 10f); // 終點位置
        Vector3 EndRotation = new Vector3(0f, 180f, 0f); // 終點旋轉
        float movetime = 10f; // 移動所需時間

        transform.DOComplete(); // 結束所有正在進行的 DOTween 動畫
        transform.DOMove(EndPos, movetime); // 從起點移動到終點
        transform.DORotate(EndRotation, movetime); // 從起始旋轉到終點旋轉
    }


}
