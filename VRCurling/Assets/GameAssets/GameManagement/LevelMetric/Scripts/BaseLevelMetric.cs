using UnityEngine;
using UnityEngine.Events;

public abstract class BaseLevelMetric: MonoBehaviour{
    public UnityEvent<PracticeState> OnPracticeEnd;
    public abstract float ComputeMetric();
    public abstract void StartPracticeMonitor();
    public enum PracticeState{
        Success,
        Fail,
    }
}