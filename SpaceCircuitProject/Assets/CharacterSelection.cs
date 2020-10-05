using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject ReadyMessage;
    public GameObject[] Rockets;
    public int selectedRocket = 0;

    private void Start()
    {
        ReadyMessage.SetActive(false);
    }

    IEnumerator BlinkCoroutine()
    {
        int repeats = 3;

        for (int i = 0; i < repeats; i++)
        {
            yield return new WaitForSeconds(0.5f);
            ReadyMessage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            ReadyMessage.SetActive(false);
            //Debug.Log("blinked i = ");
            //Debug.Log(i);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void StartLevel()
    {
        //Debug.Log("Starting level");
        StartCoroutine(BlinkCoroutine());
        PlayerPrefs.SetInt("SelectedRocket", selectedRocket);
    }

    public void NextCharacter()
    {
        //Debug.Log("next");
        Rockets[selectedRocket].SetActive(false);
        selectedRocket = (selectedRocket + 1) % Rockets.Length; //loop through rockets
        Rockets[selectedRocket].SetActive(true);
    }
    public void PreviousCharacter()
    {
        //Debug.Log("previous");
        Rockets[selectedRocket].SetActive(false);
        selectedRocket--;
        if(selectedRocket < 0)
        {
            selectedRocket += Rockets.Length;
        }
        Rockets[selectedRocket].SetActive(true);


    }

}
