using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking.Types;

class ArrowManager: MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject currentArrow;
    public void SpawnArrow(Vector3 position)
    {
        currentArrow = Instantiate(arrowPrefab, position, Quaternion.identity);
    }

    public void MoveArrow(Vector3 position)
    {
        currentArrow.transform.position = position;
    }
    

    public void DestroyCurrentArrow()
    {
        Destroy(currentArrow);
    }


}
