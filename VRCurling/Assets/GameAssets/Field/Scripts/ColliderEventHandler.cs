using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventHandler : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerEnterEvent;
    public UnityEvent<Collider> OnTriggerExitEvent;
    public UnityEvent<Collider> OnTriggerStayEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent.Invoke(other);
    }
}
