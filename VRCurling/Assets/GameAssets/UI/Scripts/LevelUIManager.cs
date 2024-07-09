using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] public LevelUIData UIData;
    [SerializeField] private Transform UITransform;
    [SerializeField] private float duration = 0.2f;
    public enum UIState
    {
        Confirm,
        Cancel
    }

    private HashSet<GameObject> UIs = new HashSet<GameObject>();

    private void Awake()
    {
    }

    public async UniTask ShowStartUI()
    {
        HideAllUI();
        var _object = Instantiate(UIData.StartUI);

        UIs.Add(_object);
        UIOpenAnimation(_object);
        await ShowConfirmUI(_object);
    }

    public async UniTask ShowSuccessUI()
    {
        HideAllUI();
        AudioClip PassAudio = Resources.Load<AudioClip>("Audio/Pass");
        PassAudio.PlaySound(parent: transform);
        var _object = Instantiate(UIData.SuccessUI);
        UIs.Add(_object);
        UIOpenAnimation(_object);
        await ShowConfirmUI(_object);
    }
    public async UniTask ShowFailUI()
    {
        HideAllUI();
        AudioClip FailAudio = Resources.Load<AudioClip>("Audio/Fail");
        FailAudio.PlaySound(parent: transform);
        var _object = Instantiate(UIData.FailUI);
        UIs.Add(_object);
        UIOpenAnimation(_object);
        await ShowConfirmUI(_object);
    }

    public async UniTask<UIState> ShowScoreUI(int score)
    {
        HideAllUI();
        AudioClip ResultAudio = Resources.Load<AudioClip>("Audio/Result");
        ResultAudio.PlaySound(parent: transform);
        var _object = Instantiate(UIData.ScoreUI);
        UIs.Add(_object);
        var scoreUIController = _object.GetComponent<ScoreUIController>();
        scoreUIController.SetScore(score);
        UIOpenAnimation(_object);
        return await ShowConfirmUI(_object);
    }

    public void ShowGoBackHintUI()
    {
        HideAllUI();
        var _object = Instantiate(UIData.GoBackUI);
        UIs.Add(_object);
        UIOpenAnimation(_object);
        SetUIToPlayerFront(_object);
    }

    public void ShowStandToSweepHintUI()
    {
        HideAllUI();
        var _object = Instantiate(UIData.StandToSweepHintUI);
        UIs.Add(_object);
        UIOpenAnimation(_object);
        SetUIToPlayerFront(_object);
    }
    public async UniTask ShowCongratulationUI()
    {
        HideAllUI();
        AudioClip CongratulationAudio = Resources.Load<AudioClip>("Audio/Congratulation");
        CongratulationAudio.PlaySound(parent: transform);
        var _object = Instantiate(UIData.CongratulationUI);
        UIs.Add(_object);
        UIOpenAnimation(_object);
        await ShowConfirmUI(_object);
    }

    public void HideAllUI()
    {

        foreach (var ui in UIs)
        {
            UICloseAnimation(ui);
            Destroy(ui, duration);
        }
        UIs.Clear();
    }

    private async UniTask<UIState> ShowConfirmUI(GameObject confirmUI)
    {
        SetUIToPosition(confirmUI);

        var confirmUIController = confirmUI.GetComponent<ConfirmUI>();
        bool isConfirmed = false;
        bool isCanceled = false;
        confirmUIController.OnConfirm.AddListener(() =>
        {
            isConfirmed = true;
        });
        confirmUIController.OnCancel.AddListener(() =>
        {
            isCanceled = true;
        });
        await UniTask.WaitUntil(() => isConfirmed || isCanceled);
        return isConfirmed ? UIState.Confirm : UIState.Cancel;
    }
    private void SetUIToPlayerFront(GameObject ui)
    {
        var player = LevelRunner.Instance.PlayerRig;
        ui.transform.position = player.transform.position + player.transform.forward * 1;
        ui.transform.position = new Vector3(ui.transform.position.x, player.transform.position.y * 0.5f, ui.transform.position.z);
        ui.transform.rotation = Quaternion.Euler(10, 0, 0);
    }
    private void SetUIToPosition(GameObject ui)
    {
        var player = LevelRunner.Instance.PlayerRig;
        ui.transform.position = new Vector3(UITransform.position.x, player.transform.position.y * 0.9f, UITransform.position.z);
        ui.transform.rotation = Quaternion.Euler(10, 0, 0);
    }

    private void UIOpenAnimation(GameObject _object)
    {
        _object.AddComponent<CanvasGroup>();
        _object.GetComponent<CanvasGroup>().alpha = 0;
        _object.GetComponent<CanvasGroup>().DOFade(1, duration);
    }

    private void UICloseAnimation(GameObject _object)
    {
        _object.GetComponent<CanvasGroup>().DOFade(0, duration);
    }
}
