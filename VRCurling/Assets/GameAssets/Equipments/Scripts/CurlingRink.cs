using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class CurlingRink : MonoBehaviour
{
    public UnityEvent OnStoneOut;
    public UnityEvent OnPlayerEnterHogLine;
    public PhysicMaterial cleanMaterial;
    public PhysicMaterial dirtyMaterial;
    public Collider rinkCollider;

    [SerializeField] private ColliderEventHandler hogLine;

    private void Awake()
    {
        hogLine.OnTriggerEnterEvent.AddListener(OnHogLineEnter);
    }

    void OnTriggerExit(Collider other)
    {
        var stone = other.GetComponentInParent<CurlingStone>();
        if (stone != null && stone.IgnoreTriggerEvent == false)
        {
            OutHandler();
        }
    }
    private bool cold = false;
    private async void OutHandler(){
        if(cold) return;
        OnStoneOut.Invoke();
        AudioClip Outsite = Resources.Load<AudioClip>("Audio/Outsite");
        Outsite.PlaySound(parent: transform);
        cold = true;
        await UniTask.WaitForSeconds(5.0f);
        cold = false;
    }

    void OnHogLineEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            OnPlayerEnterHogLine.Invoke();
        }
    }

    private void OnDestroy()
    {
        hogLine.OnTriggerEnterEvent.RemoveListener(OnHogLineEnter);
    }

    public void SetRinkClean(){
        rinkCollider.material = cleanMaterial;
    }
    public void SetRinkDirty(){
        rinkCollider.material = dirtyMaterial;
    }
    
}
