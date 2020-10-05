using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    public GameObject CockpitMsgCollision;
    public GameObject [] CockpitRocketModelsCollision;



    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("HIT SOMETHING");

        if (collision.collider.CompareTag("EnemyObject"))
        {
            //Debug.Log("HIT ENEMY");
            for(int i =0; i < CockpitRocketModelsCollision.Length; i++) //turn off all models on cockpit screen
            {
                CockpitRocketModelsCollision[i].SetActive(false);
            }
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        int repeats = 3;

        for (int i = 0; i < repeats; i++)
        {
            yield return new WaitForSeconds(0.1f);
            CockpitMsgCollision.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            CockpitMsgCollision.SetActive(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
