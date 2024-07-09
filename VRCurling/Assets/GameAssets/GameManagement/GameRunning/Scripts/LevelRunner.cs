using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine.Events;
using System.Threading;

public class LevelRunner : Singleton<LevelRunner>
{
    public UnityEvent OnRequestExit;
    [SerializeField] public BaseLevelMetric metric;
    [SerializeField] public Transform PlayerRig;
    [SerializeField] private LevelUIManager levelUIManager;
    [SerializeField] public CurlingRunner curlingRunner;
    [SerializeField] public CurlingRink curlingRink;
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private AudioClip BackgroundMusic;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start(){
        BackgroundMusic.PlayMusic(volume:0.2f);
        levelUIManager.HideAllUI();
        curlingRunner.SpawnBroom();
        StartLevel();
    }
    [ContextMenu("Start Level")]
    public async void StartLevel(){
        if(curlingRunner.CurrentStone != null){
            curlingRunner.RemoveCurlingStone();
        }
        await UniTask.WaitForSeconds(0.2f);
        await levelUIManager.ShowStartUI();
        DoArrowGeneration();
        levelUIManager.HideAllUI();
        metric.StartPracticeMonitor();
        var cts = new CancellationTokenSource();
        StartPractice(cts.Token);
        var practiceFinalState = await Utils.WaitEvent(metric.OnPracticeEnd);
        cts.Cancel();

        curlingRunner.ResetPlayerBroomPosition();

        await UniTask.DelayFrame(3);

        if(practiceFinalState == BaseLevelMetric.PracticeState.Success){
            await levelUIManager.ShowSuccessUI();
            if(levelUIManager.UIData.NeedScore){
                var state = await levelUIManager.ShowScoreUI((int)metric.ComputeMetric());
                if(state == LevelUIManager.UIState.Cancel){
                    await PlayerGoBack();
                    StartLevel();
                    return;
                }
            }
            if(levelUIManager.UIData.NeedCongratulation){
                await levelUIManager.ShowCongratulationUI();
            }
            await PlayerGoBack();
            RequestExit();
        }else{
            await levelUIManager.ShowFailUI();
            await PlayerGoBack();
            StartLevel();
        }
    }

    private async void DoArrowGeneration(){
        await UniTask.WaitUntil(() => curlingRunner.CurrentStone != null);
        arrowManager.SpawnArrow(curlingRunner.CurrentStone.transform.position);
        ArrowFollowStone();
        await UniTask.WaitForSeconds(2);
        var heldTask = UniTask.WaitUntil(() => curlingRunner.CurrentStone.IsHeld);
        await UniTask.WhenAny(heldTask, UniTask.Delay(TimeSpan.FromSeconds(2)));
        arrowManager.DestroyCurrentArrow();
    }

    private async void ArrowFollowStone(){
        while(curlingRunner.CurrentStone != null && arrowManager.currentArrow != null){
            arrowManager.MoveArrow(curlingRunner.CurrentStone.transform.position);
            await UniTask.Yield();
        }
    }

    public async void StartPractice(CancellationToken cancellationToken = default){
        if(levelUIManager.UIData.NeedCurling){
            curlingRunner.StartCurling();
        }else{
            curlingRunner.SpawnThrowingStone();
        }
        await UniTask.WaitUntil(() => curlingRunner.CurrentState == CurlingRunner.State.Thrown, cancellationToken: cancellationToken);
        if(levelUIManager.UIData.NeedSweeping){
            levelUIManager.ShowStandToSweepHintUI();
        }
        await UniTask.WaitUntil(() => Player.Instance.state == Player.State.Standing, cancellationToken: cancellationToken);
        if(levelUIManager.UIData.NeedSweeping){
            levelUIManager.HideAllUI();
            curlingRunner.SwitchSweeping();
        }

    }
    public async UniTask PlayerGoBack(CancellationToken cancellationToken = default){
        curlingRunner.ResetPlayerBroomPosition();
        levelUIManager.ShowGoBackHintUI();
        await UniTask.WaitUntil(() => curlingRunner.PlayerIsAtStartPoint, cancellationToken: cancellationToken);
        levelUIManager.HideAllUI();
    }
    [ContextMenu("RequestExit")]
    public void RequestExit()
    {
        OnRequestExit?.Invoke();
    }
}
