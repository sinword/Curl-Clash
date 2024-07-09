using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Transform StoneSpawnPoint;
    public CurlingRunner curlingRunner;


    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 150, 100), "Spawn Stone Running"))
        {
            curlingRunner.SpawnThrowingStone();
        }
    }
}
