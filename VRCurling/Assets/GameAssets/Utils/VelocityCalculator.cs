using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    private Vector3 lastPosition = Vector3.zero;
    private Queue<Vector3> lastPos = new Queue<Vector3>();
    public Vector3 GetVelocity(){
        // calculate the average velocity on the plane
        if (lastPos.Count < 2)
        {
            return Vector3.zero;
        }
        Vector3[] lastPosVec = lastPos.ToArray();
        Vector3 totalShift = lastPosVec[lastPos.Count - 1] - lastPosVec[0];
        totalShift = Vector3.ProjectOnPlane(totalShift, Vector3.up); // get projection on plane
        return totalShift / (Time.fixedDeltaTime * lastPos.Count);
    }

    void FixedUpdate()
    {
        lastPos.Enqueue(transform.position);
        if(lastPos.Count > 100)
        {
            lastPos.Dequeue();
        }
    }
    /*
    */
}