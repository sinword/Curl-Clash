using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Essence.Controller.Model;

public class Utils{
    public static async UniTask WaitEvent(UnityEvent unityEvent, CancellationToken cancellationToken = default){
        bool isEventTriggered = false;
        UnityAction action = () => {
            isEventTriggered = true;
        };
        unityEvent.AddListener(action);
        await UniTask.WaitUntil(() => isEventTriggered, cancellationToken: cancellationToken);
        unityEvent.RemoveListener(action);
    }
    public static async UniTask<T> WaitEvent<T>(UnityEvent<T> unityEvent, CancellationToken cancellationToken = default){
        T value = default;
        bool isEventTriggered = false;
        UnityAction<T> action = (v) => {
            value = v;
            isEventTriggered = true;
        };
        unityEvent.AddListener(action);
        await UniTask.WaitUntil(() => isEventTriggered, cancellationToken: cancellationToken);
        unityEvent.RemoveListener(action);
        return value;
    }
}