using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent OnTutorialEnd;
    [SerializeField] private RuleDisplayUIController ruleDisplayUIController;
    [SerializeField] private TutorController tutorController;
    [SerializeField] private GameObject replayPanel;
    [SerializeField] private Transform PlayerCamera;
    private void Awake()
    {
        ruleDisplayUIController.gameObject.SetActive(false);
        tutorController.gameObject.SetActive(false);
    }
    private void Start(){
    }
    [ContextMenu("StartTutorial")]
    public void StartTutorial()
    {
        ruleDisplayUIController.gameObject.SetActive(true);
        ruleDisplayUIController.StartDisplay();
        ruleDisplayUIController.OnDisplayEnd.AddListener(ruleDisplayEndHandler);
        //Set player position
        PlayerCamera.position = new Vector3(0.0f, -6.4f, 11.01f);
    }
    private void ruleDisplayEndHandler(){
        ruleDisplayUIController.gameObject.SetActive(false);
        tutorController.gameObject.SetActive(true);
        tutorController.StartTutorial();
        tutorController.OnTutorialEnd.AddListener(tutorEndHandler);
        //Set player position
        PlayerCamera.position = new Vector3(3.44f, -6.4f, 6.178f);
    }
    [ContextMenu("EndTutorial")]
    private void tutorEndHandler(){
        tutorController.gameObject.SetActive(false);
        replayPanel.SetActive(true);
        replayPanel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;
        replayPanel.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 8);
    }

    public void ReplayTutorial(){
        replayPanel.SetActive(false);
        ruleDisplayUIController.gameObject.SetActive(false);
        tutorController.gameObject.SetActive(false);
        StartTutorial();
    }

    public void Exit(){
        OnTutorialEnd.Invoke();
    }

}