using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

public class EquipmentManager : Singleton<EquipmentManager>
{
    [SerializeField] private GameObject CurlingBroom;
    [SerializeField] private GameObject CurlingStone;
    public HashSet<GameObject> Stones = new HashSet<GameObject>();

    public GameObject SpawnCurlingStone(Vector3 position, Quaternion rotation){
        var equipment = Instantiate(CurlingStone, position, rotation);
        Stones.Add(equipment);
        return equipment;
    }
    public GameObject SpawnCurlingBroom(Vector3 position, Quaternion rotation){
        var equipment = Instantiate(CurlingBroom, position, rotation);
        return equipment;
    }
    public GameObject RemoveCurlingStone(GameObject stone){
        Assert.IsTrue(Stones.Contains(stone));
        Stones.Remove(stone);
        Destroy(stone);
        return stone;
    }

    public void RemoveAllStones(){
        foreach(var stone in Stones){
            Destroy(stone);
        }
        Stones.Clear();
    }
}
