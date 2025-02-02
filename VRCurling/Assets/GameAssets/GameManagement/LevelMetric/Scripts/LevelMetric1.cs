using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

public class LevelMetric1 : BaseLevelMetric
{
    [SerializeField] private CurlingRink curlingRink;
    [SerializeField] private CurlingRunner curlingRunner;
    private void Awake()
    {
        Assert.IsNotNull(curlingRunner);
        Assert.IsNotNull(curlingRink);
    }
    public override float ComputeMetric()
    {
        return 100;
    }

    public override async void StartPracticeMonitor()
    {
        await UniTask.WaitUntil(() => curlingRunner.CurrentState == CurlingRunner.State.Thrown);
        var stoneStopEvent = curlingRunner.WaitUntilCurrentStoneStopped();
        var outEvent = Utils.WaitEvent(curlingRink.OnStoneOut);
        int index = await UniTask.WhenAny(stoneStopEvent, outEvent);
        await UniTask.WaitForSeconds(0.5f);
        switch (index)
        {
            case 0:
                OnPracticeEnd.Invoke(PracticeState.Success);
                break;
            case 1:
                OnPracticeEnd.Invoke(PracticeState.Fail);
                break;
        }
    }
}