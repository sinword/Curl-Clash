using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offsetter : MonoBehaviour
{
    public Vector3 AdditionalPositionOffset;
    public Quaternion AdditionalRotationOffset;


    public void SetPositionOffset(Vector3 offset)
    {
        transform.position = offset + AdditionalPositionOffset;
    }

    public void SetRotationOffset(Quaternion offset)
    {
        transform.rotation = offset * AdditionalRotationOffset;
    }
}
