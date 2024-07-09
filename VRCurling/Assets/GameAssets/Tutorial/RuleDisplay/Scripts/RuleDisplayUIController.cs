using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RuleDisplayUIController : MonoBehaviour
{
    public UnityEvent OnDisplayEnd; // Make it public
    [SerializeField] private Button skipButton;

    [ContextMenu("StartDisplay")]
    public void StartDisplay()
    {
    }

    private void Start()
    {
        skipButton.onClick.AddListener(InvokeOnDisplayEnd);
    }

    private void InvokeOnDisplayEnd()
    {
        // PreFab的...
        OnDisplayEnd.Invoke();
    }
}
