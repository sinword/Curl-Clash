using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class AimingManager : MonoBehaviour
{
    public UnityEvent OnAimingStart;
    public UnityEvent<float> OnAimingEnd;
    [SerializeField] private Transform Center;
    [SerializeField] private float targetRadius;
    [SerializeField] private float countRadius;
    [SerializeField] private Transform StoneSpawnPoint;

    private Rigidbody stone;
    private Vector3 targetPoint;


    private async void StartAiming(){
        targetPoint = GetRandomPoint();
        stone = EquipmentManager.Instance.SpawnCurlingStone(StoneSpawnPoint.position, StoneSpawnPoint.rotation).GetComponent<Rigidbody>();

        OnAimingStart.Invoke();

        await UniTask.WaitUntil(
            () =>
            Vector3.Distance(stone.position, Center.position) < countRadius
            && stone.velocity.magnitude < 0.1f
        );

        await UniTask.Delay(1000);

        OnAimingEnd.Invoke(GetDistance());
    }


    public float GetDistance()
    {
        return Vector3.Distance(stone.position, targetPoint);
    }

    private Vector3 GetRandomPoint()
    {
        return Random.insideUnitSphere * targetRadius + Center.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Center.position, targetRadius);
    }
}
