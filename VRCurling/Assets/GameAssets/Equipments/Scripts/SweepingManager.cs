using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class SweepingManager : MonoBehaviour
{
    private bool isSweeping = false;
    public void StartSweeping(CurlingStone stone, CurlingBroom broom)
    {
        Assert.IsNotNull(stone);
        Assert.IsNotNull(broom);
        isSweeping = true;
        SweepingProgress(stone, broom);
    }
    public void StopSweeping()
    {
        isSweeping = false;
    }

    private async void SweepingProgress(CurlingStone stone, CurlingBroom broom){
        while(isSweeping){
            if(broom.IsOnRink){
                LevelRunner.Instance.curlingRink.SetRinkClean();
                var stoneVelocity = stone.rb.velocity;
                var stoneBroomVector = (broom.transform.position - stone.transform.position).normalized;
                stoneBroomVector.y = stoneVelocity.y;
                
                stone.rb.velocity = Vector3.Lerp(stoneVelocity, stoneBroomVector * stoneVelocity.magnitude, 0.08f * Time.fixedDeltaTime);
                
            }else{
                LevelRunner.Instance.curlingRink.SetRinkDirty();
            }
            await UniTask.WaitForFixedUpdate();
        }
    }
}
