using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinkBlock : MonoBehaviour
{
    // Start is called before the first frame 
    [SerializeField] private PhysicMaterial rinkMaterial;
    [SerializeField] private PhysicMaterial slipperyMaterial;
    Collider thisCollider;
    void Awake()
    {
        thisCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Broom")
        {
            thisCollider.material = slipperyMaterial;
        }
    }
    void OnTriggerExit (Collider other)
    {
        if(other.tag == "Broom")
        {
            thisCollider.material = rinkMaterial;
        }
    }
}
