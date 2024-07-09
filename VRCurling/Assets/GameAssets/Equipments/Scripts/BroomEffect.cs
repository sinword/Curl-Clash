using UnityEngine;

public class BroomEffect : MonoBehaviour
{   
    public CurlingBroom broom;
    public GameObject sweepEffect; 
    public GameObject floorEffect; 
    public float yOffset = 0.3f;
    public float xOffset = 0.3f;
    public float zOffset = 0.1f;
    public float effectInterval = 0.5f; // 時間間隔（秒）
    private float effectTimer;

    void Start()
    {
        sweepEffect.SetActive(false);
        effectTimer = 0f;
    }

    void Update()
    {
        effectTimer -= Time.deltaTime;

        if (broom.IsSweeping)
        {
            if (broom.IsOnRink)
            {   
                sweepEffect.SetActive(true);

                if (effectTimer <= 0f)
                {
                    Vector3 adjustedPosition = broom.transform.position;
                    // adjustedPosition.x += xOffset;
                    // adjustedPosition.z -= zOffset;
                    adjustedPosition.y = yOffset;
                    GameObject flooreffect = Instantiate(floorEffect, adjustedPosition, Quaternion.identity);
                    Destroy(flooreffect, 10);
                    effectTimer = effectInterval; // 重置計時器
                }
            }
            else
            {
                sweepEffect.SetActive(false);
            }
        }
        else
        {
            sweepEffect.SetActive(false);
        }
    }
}