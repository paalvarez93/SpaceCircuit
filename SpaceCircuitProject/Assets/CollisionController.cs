using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT SOMETHING");

        if (collision.collider.CompareTag("EnemyObject"))
        {
            Debug.Log("HIT ENEMY");
            SceneManager.LoadScene("SampleScene");
        }
        //GetComponent<Rigidbody>().freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
