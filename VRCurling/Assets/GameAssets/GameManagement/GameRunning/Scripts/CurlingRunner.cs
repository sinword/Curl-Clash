using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using Wave.Essence.Tracker.Model.Demo;
using System.Threading;
using UnityEngine.Assertions;

public class CurlingRunner: MonoBehaviour{
    public CurlingStone CurrentStone { get; private set;} = null;
    private CurlingBroom CurlingBroom = null;
    [SerializeField] private SweepingManager sweepingManager;

    public bool PlayerIsAtStartPoint => Vector2.Distance(Player.Instance.Get2DPosition(), playerStartPoint) < 0.2f;
    private Vector2 playerStartPoint;
    public enum State{
        Idle,
        Sliding,
        Thrown,
        Sweeping
    };
    public State CurrentState {get; private set;}  = State.Idle;
    [ContextMenu("Start Curling")]
    public async void StartCurling(){
        playerStartPoint = Player.Instance.Get2DPosition();
        CurrentStone = EquipmentManager.Instance.SpawnCurlingStone(Vector3.zero, Quaternion.identity).GetComponent<CurlingStone>();
        CurrentState = State.Idle;
        Debug.Log("Enter Idle State");
        await WaitUntilPlayerSlide();
        CurrentState = State.Sliding;
        Debug.Log("Enter Sliding State");
        PlayerSliding();
        await UniTask.WaitUntil(() => !CurrentStone.IsHeld);
        CurrentStone.SetToVelocityCalculator();
        CurrentStone.SetUnbindTracker();
        CurrentState = State.Thrown;
        Debug.Log("Enter Thrown State");
    }
    private async void PlayerSliding(){
        var velocity = Player.Instance.GetPlayerCurrentVelocity() * 0.3f;
        TrackerPose stoneTracker = CurrentStone.GetComponent<TrackerPose>();
        TrackerPose broomTracker = CurlingBroom.GetComponent<TrackerPose>();
        Debug.Log("Player Sliding Start");
        var offset = Vector3.zero;
        while (CurrentState == State.Sliding){
            offset += velocity;
            stoneTracker.positionOffset = offset;
            broomTracker.positionOffset = offset;
            Player.Instance.transform.position = offset;
            velocity *= 0.995f;
            await UniTask.NextFrame();
        }
        Debug.Log("Player Sliding End");
    }
    [ContextMenu("Spawn Stone Going To Circle")]
    public async void SpawnThrowingStone(){
        playerStartPoint = Player.Instance.Get2DPosition();
        Vector3 stonePosition = new Vector3(0, 0.2f, 0);
        Quaternion stoneRotation = Quaternion.Euler(0, 0.15f, 0);
        float stoneVelocity = 3.5f;
        
        CurrentStone = EquipmentManager.Instance.SpawnCurlingStone(stonePosition, stoneRotation).GetComponent<CurlingStone>();
        CurrentStone.SetUnbindTracker();
        await UniTask.DelayFrame(1);
        CurrentStone.SetVelocity(CurrentStone.transform.forward * stoneVelocity);
        CurrentState = State.Thrown;
    }
    public async void SwitchSweeping(){
        CurrentState = State.Sweeping;
        Assert.IsNotNull(CurlingBroom);
        Assert.IsNotNull(CurrentStone);
        sweepingManager.StartSweeping(CurrentStone, CurlingBroom);

        int stone_slow_count = 0;
        while(CurrentState == State.Sweeping){
            CurlingBroom.SetOffset(CurrentStone.Get2DPosition());
            Player.Instance.SetOffset(CurrentStone.Get2DPosition());
            if(CurrentStone.stoneVelocityCalculator.GetVelocity().magnitude < 0.5f){
                stone_slow_count++;
                if(stone_slow_count > 10){
                    break;
                }
            }
            else{
                stone_slow_count = 0;
            }
            await UniTask.WaitForFixedUpdate();
        }

        sweepingManager.StopSweeping();
    }
    [ContextMenu("Stop Sweeping")]
    public void ResetPlayerBroomPosition(){
        CurlingBroom.ResetOffset();
        Player.Instance.ResetOffset();
        CurrentState = State.Idle;
        Debug.Log("Reset Player Broom Position");
    }
    [ContextMenu("Remove Stone")]
    public void RemoveCurlingStone(){
        EquipmentManager.Instance.RemoveCurlingStone(CurrentStone.gameObject);
    }

    public async UniTask WaitUntilPlayerSlide(){
        await UniTask.WaitUntil(() => Vector2.Distance(Player.Instance.Get2DPosition(), playerStartPoint) > 0.6);
    }
    [ContextMenu("Spawn Broom")]
    public void SpawnBroom(){
        if(CurlingBroom != null){
            Debug.LogWarning("Broom already exists");
        }
        CurlingBroom = EquipmentManager.Instance.SpawnCurlingBroom(Vector3.zero, Quaternion.identity).GetComponent<CurlingBroom>();
    }
    public async UniTask WaitUntilCurrentStoneStopped(CancellationToken cancellationToken = default){
        Assert.IsNotNull(CurrentStone);
        await UniTask.WaitUntil(() => CurrentStone.IsStopped, cancellationToken: cancellationToken);
        Debug.Log("Stone Stopped");
    }
}