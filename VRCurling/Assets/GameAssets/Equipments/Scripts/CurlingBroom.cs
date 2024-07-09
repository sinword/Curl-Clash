using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Wave.Essence.Tracker.Model.Demo;

[RequireComponent(typeof(TrackerPose))]
public class CurlingBroom : MonoBehaviour
{
    public bool IsOnRink = false;
    private TrackerPose trackerPose;
    private Vector3 posOffset = new Vector3(0.5f, 0.0f, 1.0f);
    public bool IsSweeping => trackerPose.rotate90;

    public void SetOffset(Vector2 offset)
    {
        trackerPose.positionOffset = new Vector3(offset.x, 0.0f, offset.y) + posOffset;
        trackerPose.rotationOffset = Quaternion.Euler(0, -90, 0);
        trackerPose.rotate90 = true;
    }
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;

    private void LateUpdate(){
        lastPosition = currentPosition;
    }
    public void ResetOffset()
    {
        trackerPose.positionOffset = Vector3.zero;
        trackerPose.rotationOffset = Quaternion.Euler(0, 0, 0);
        trackerPose.rotate90 = false;
    }
    private void Awake(){
        trackerPose = GetComponent<TrackerPose>();
    }
    private float GetVelocityMagnitude()
    {
        currentPosition = transform.position - trackerPose.positionOffset;
        return (currentPosition - lastPosition).magnitude / Time.deltaTime;
    }

    int stopTimes = 0;
    void OnTriggerStay(Collider other){
        if (other.tag == "Rink")
        {
            if(GetVelocityMagnitude() > 0.1f){
                IsOnRink = true;
                stopTimes = 0;
            }else if(stopTimes > 10){
                IsOnRink = false;
            }else{
                stopTimes++;
            }
        }
    }
    void OnTriggerExit(Collider other){
        if (other.tag == "Rink")
        {
            IsOnRink = false;
        }
    }
}
