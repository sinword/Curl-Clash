using UnityEngine;

[CreateAssetMenu]
public class LevelUIData: ScriptableObject{
    public GameObject StartUI;
    public GameObject SuccessUI;
    public GameObject FailUI;
    public GameObject ScoreUI;
    public GameObject GoBackUI;
    public GameObject StandToSweepHintUI;
    public GameObject CongratulationUI;
    public bool NeedCurling;
    public bool NeedSweeping;
    public bool NeedScore;
    public bool NeedCongratulation;
}