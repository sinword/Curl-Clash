using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

public class LevelMetric3 : BaseLevelMetric
{
    [SerializeField] private Transform Circle;
    [SerializeField] private CurlingRink curlingRink;
    [SerializeField] private CurlingRunner curlingRunner;
    [SerializeField] private float radius;
    private void Awake()
    {
        Assert.IsNotNull(Circle);
        Assert.IsNotNull(curlingRunner);
        Assert.IsNotNull(curlingRink);
    }
    public override float ComputeMetric()
    {
        var stone = curlingRunner.CurrentStone;
        var distance = Vector3.Distance(stone.transform.position, Circle.position);
        if (distance < radius)
        {
            return 100 - distance / radius * 100;
        }
        else
        {
            return 0;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Circle.position, radius);
    }
}