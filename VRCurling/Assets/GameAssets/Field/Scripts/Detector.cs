using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    public UnityEvent OnDetectStoneStay1;
    public UnityEvent OnDetectStoneStay2;
    public UnityEvent OnDetectStoneStay3;

    [SerializeField] private ColliderEventHandler collider1;
    [SerializeField] private ColliderEventHandler collider2;
    [SerializeField] private ColliderEventHandler collider3;

    private HashSet<Collider> stonesInCollider1 = new HashSet<Collider>();
    private HashSet<Collider> stonesInCollider2 = new HashSet<Collider>();
    private HashSet<Collider> stonesInCollider3 = new HashSet<Collider>();

    private void Start()
    {
        collider1.OnTriggerEnterEvent.AddListener(EnterHandler1);
        collider1.OnTriggerExitEvent.AddListener(ExitHandler1);
        collider2.OnTriggerEnterEvent.AddListener(EnterHandler2);
        collider2.OnTriggerExitEvent.AddListener(ExitHandler2);
        collider3.OnTriggerEnterEvent.AddListener(EnterHandler3);
        collider3.OnTriggerExitEvent.AddListener(ExitHandler3);
    }

    private void EnterHandler1(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider1.Add(other);
        }
    }

    private void ExitHandler1(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider1.Remove(other);
        }
    }

    private void EnterHandler2(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider2.Add(other);
        }
    }

    private void ExitHandler2(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider2.Remove(other);
        }
    }

    private void EnterHandler3(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider3.Add(other);
        }
    }

    private void ExitHandler3(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            stonesInCollider3.Remove(other);
        }
    }

    private void Update()
    {
        CheckStoneStay(stonesInCollider3, OnDetectStoneStay3);
        CheckStoneStay(stonesInCollider2, OnDetectStoneStay2);
        CheckStoneStay(stonesInCollider1, OnDetectStoneStay1);
    }

    private void CheckStoneStay(HashSet<Collider> stones, UnityEvent unityEvent)
    {
        foreach (var stone in stones)
        {
            Rigidbody rb = stone.GetComponent<Rigidbody>();
            if (rb != null && rb.velocity.magnitude < 0.1f)
            {
                unityEvent.Invoke();
                stones.Remove(stone);
            }
        }
    }
}
