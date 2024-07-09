using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public bool InActiceWholeObject = true;
    public MonoBehaviour[] ComponentsToDisable;
    private void Awake(){
        if (Application.isEditor && InActiceWholeObject)
        {
            gameObject.SetActive(false);
        }

        foreach (var component in ComponentsToDisable)
        {
            component.enabled = false;
        }
    }
}
