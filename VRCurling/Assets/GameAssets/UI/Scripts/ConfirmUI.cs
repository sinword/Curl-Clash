using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;


public class ConfirmUI: MonoBehaviour{
    public UnityEvent OnConfirm;
    public UnityEvent OnCancel;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    protected virtual void Start(){
        if(_confirmButton != null)
            _confirmButton.onClick.AddListener(ConfirmHandler);

        if(_cancelButton != null)
            _cancelButton.onClick.AddListener(CancelHandler);
    }

    private void ConfirmHandler(){
        AudioClip buttonAudioClip = Resources.Load<AudioClip>("Audio/Click");
        buttonAudioClip.PlaySound();
        OnConfirm.Invoke();

    }

    private void CancelHandler(){
        AudioClip buttonAudioClip = Resources.Load<AudioClip>("Audio/Click");
        buttonAudioClip.PlaySound();
        OnCancel.Invoke();
        
    }
    

}