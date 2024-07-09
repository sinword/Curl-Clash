using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.Video;

//public TutorialManager RuleDisplayUIController;
public class TutorialDisplayController : MonoBehaviour
{
    private void StopPrefab()
    {
        // Accessing the RuleDisplayUIController component
        RuleDisplayUIController rdc = GetComponentInParent<RuleDisplayUIController>();

        // Check if the controller is found
        if (rdc != null)
        {
            // Invoke the OnDisplayEnd event
            rdc.OnDisplayEnd.Invoke();
        }
    }
    [SerializeField] private MessageList TutorialMessages;
    private Message[] tutorialMessages;
    public UnityEvent OnTutorialEnd;

    [Header("References")]
    [SerializeField] private TMP_Text message;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private VideoPlayer videoPlayer;

    private int currentMessageIndex = 0;

    private void Awake()
    {
        Assert.IsNotNull(this.message);
        Assert.IsNotNull(this.nextButton);
        Assert.IsNotNull(this.previousButton);
        Assert.IsNotNull(this.videoPlayer);
        Assert.IsNotNull(this.TutorialMessages);
    }

    private void Start()
    {
        tutorialMessages = TutorialMessages.messages.ToArray();
        applyMessage(tutorialMessages[currentMessageIndex]);
        nextButton.onClick.AddListener(nextMessage);
        previousButton.onClick.AddListener(previousMessage);
    }


    private void OnDestroy()
    {
        nextButton.onClick.RemoveListener(nextMessage);
        previousButton.onClick.RemoveListener(previousMessage);
    }

    [ContextMenu("Click Next")]
    private void nextMessage()
    {
        Debug.Log("Next button clicked");
        if (currentMessageIndex < tutorialMessages.Length - 1)
        {
            currentMessageIndex++;
            Debug.Log("Current message index: " + currentMessageIndex);
            applyMessage(tutorialMessages[currentMessageIndex]);
        }
        else
        {
            OnTutorialEnd.Invoke();
        }
    }

    [ContextMenu("Click Previous")]
    private void previousMessage()
    {
        if (currentMessageIndex > 0)
        {
            currentMessageIndex--;
            applyMessage(tutorialMessages[currentMessageIndex]);
        }
    }

    private void applyMessage(Message message)
    {
        this.message.text = message.message;
        this.videoPlayer.clip = message.videoClip;

        this.message.gameObject.SetActive(!string.IsNullOrEmpty(message.message));
        this.videoPlayer.gameObject.SetActive(message.videoClip != null);

        if (message.videoClip != null)
        {
            this.videoPlayer.Play();
        }
        else
        {
            this.videoPlayer.Stop();
        }
        if (currentMessageIndex == tutorialMessages.Length - 1)
        {
            nextButton.onClick.AddListener(StopPrefab);
        }
    }
}