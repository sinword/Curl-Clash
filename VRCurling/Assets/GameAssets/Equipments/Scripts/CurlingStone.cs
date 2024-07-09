using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Wave.Essence.Tracker;
using Wave.Essence.Tracker.Model.Demo;


[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(TrackerPose))]
public class CurlingStone : MonoBehaviour
{
    private Vector3 rightHandPos;
    public Rigidbody rb;
    private TrackerPose trackerPose;
    public VelocityCalculator stoneVelocityCalculator;
    public bool IsStopped => rb.velocity.magnitude < 0.1f;
    public bool IsHeld => Vector3.Distance(rightHandPos, transform.position) < 0.3f;
    public bool IgnoreTriggerEvent { get; private set; } = false;
    private void Awake(){
        rb = GetComponent<Rigidbody>();
        trackerPose = GetComponent<TrackerPose>();
        SetBindTracker();
    }
    [ContextMenu("SetBindTracker")]
    public async void SetBindTracker(){
        trackerPose.enabled = true;
        rb.isKinematic = true;
        IgnoreTriggerEvent = true;
        await UniTask.DelayFrame(3);
        IgnoreTriggerEvent = false;
    }
    [ContextMenu("SetUnbindTracker")]
    public async void SetUnbindTracker(){
        trackerPose.enabled = false;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        rb.isKinematic = false;
        IgnoreTriggerEvent = true;
        await UniTask.DelayFrame(3);
        IgnoreTriggerEvent = false;
    }
    public void SetVelocity(Vector3 velocity){
        rb.velocity = velocity;
    }
    public Vector2 Get2DPosition(){
        return new Vector2(transform.position.x, transform.position.z);
    }
    private void Update(){
        if (TrackerManager.Instance != null && TrackerManager.Instance.IsTrackerPoseValid(TrackerId.Tracker0)){
            rightHandPos = TrackerManager.Instance.GetTrackerPosition(TrackerId.Tracker0);
            rightHandPos += trackerPose.positionOffset;
        }
    }
    public void SetToVelocityCalculator(){
        rb.velocity = stoneVelocityCalculator.GetVelocity();
    }
    
}
