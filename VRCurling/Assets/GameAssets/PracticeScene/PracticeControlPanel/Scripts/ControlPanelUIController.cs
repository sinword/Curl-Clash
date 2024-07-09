using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlPanelUIController : MonoBehaviour
{
    public UnityEvent OnGetAStone;
    public UnityEvent OnClearStones;

    public void GetAStone()
    {
        OnGetAStone.Invoke();
    }

    public void ClearStones()
    {
        OnClearStones.Invoke();
    }
}
