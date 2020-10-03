using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLimits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
