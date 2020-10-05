using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CameraMove : MonoBehaviour
{
    private Rigidbody cameraRigidBody;
    public float moveForce = 750.0f;
    public float baseForce = 150.0f;
    public FixedJoystick joystick;
    public GameObject [] CockpitRocketModels;
    private bool fuelAlertOff = true;
    public GameObject[] StagesApollo;
    public GameObject[] StagesDelta;
    public GameObject[] StagesFalcon;
    public GameObject[] CockpitModels;
    public GameObject[] PlanetSongs;

    public GameObject CockpitMsgFuel;
    public GameObject CockpitMsgPlanet;
    public GameObject[] CockpitImgPlanet;



    int SelectedRocket;
    private float FuelAvailable = 0.0f;
    private float FuelAvailableBase = 0.0f;
    private float RocketForce = 1.0f;

    private float lastForceX = 0;
    private float lastForceY = 0;

    private int forwardSteps = 0;

    private void Awake()
    {
        cameraRigidBody = GetComponent<Rigidbody>();
        //Debug.Log("entered awake");
        AssignRocketDetails();
    }
    void MoveFoward()
    {
        if(FuelAvailable > 0)
        {
            UpdateFuelScreen();
            UpdateFog();
            UpdatePlanetScreens();
            UpdateSound();

            this.cameraRigidBody.AddForce(0, 0, baseForce * RocketForce, ForceMode.Force);
            FuelAvailable--;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        //Debug.Log(forwardSteps);
    }

    void UpdateSound()
    {
        int currentPlanet = 0;
        if (cameraRigidBody.transform.position.z < 5993.0f)
        {
            currentPlanet = 0;
            PlanetSongs[currentPlanet].SetActive(true);
            for (int i = 0; i < PlanetSongs.Length; i++)
            {
                if (i != currentPlanet)
                {
                    PlanetSongs[i].SetActive(false);
                }
            }
        }
        else if (cameraRigidBody.transform.position.z < 17963.0f)
        {
            currentPlanet = 1;
            PlanetSongs[currentPlanet].SetActive(true);
            for (int i = 0; i < PlanetSongs.Length; i++)
            {
                if (i != currentPlanet)
                {
                    PlanetSongs[i].SetActive(false);
                }
            }
        }
        else //else (cameraRigidBody.transform.position.z >= 17963.0f)
        {
            currentPlanet = 2;
            PlanetSongs[currentPlanet].SetActive(true);
            for (int i = 0; i < PlanetSongs.Length; i++)
            {
                if (i != currentPlanet)
                {
                    PlanetSongs[i].SetActive(false);
                }
            }
        }
    }

    void UpdatePlanetScreens()
    {
        int currentPlanet = 0;
        if (cameraRigidBody.transform.position.z < 5993.0f)
        {
            CockpitMsgPlanet.GetComponent<TMPro.TextMeshProUGUI>().text = "Approaching: \nMoon";
            currentPlanet = 0;
            CockpitImgPlanet[currentPlanet].SetActive(true);
            for(int i=0; i<CockpitImgPlanet.Length; i++)
            {
                if( i != currentPlanet )
                {
                    CockpitImgPlanet[i].SetActive(false);
                }
            }
        }
        else if (cameraRigidBody.transform.position.z < 17963.0f)
        {
            CockpitMsgPlanet.GetComponent<TMPro.TextMeshProUGUI>().text = "Approaching: \nMars";
            currentPlanet = 1;
            CockpitImgPlanet[currentPlanet].SetActive(true);
            for (int i = 0; i < CockpitImgPlanet.Length; i++)
            {
                if (i != currentPlanet)
                {
                    CockpitImgPlanet[i].SetActive(false);
                }
            }
        }
        else //else (cameraRigidBody.transform.position.z >= 17963.0f)
        {
            CockpitMsgPlanet.GetComponent<TMPro.TextMeshProUGUI>().text = "Approaching: \nJupiter";
            currentPlanet = 2;
            CockpitImgPlanet[currentPlanet].SetActive(true);
            for (int i = 0; i < CockpitImgPlanet.Length; i++)
            {
                if (i != currentPlanet)
                {
                    CockpitImgPlanet[i].SetActive(false);
                }
            }
        }
    }
    void UpdateFog()
    {
        if (RenderSettings.fog == false)
        {
            if (cameraRigidBody.transform.position.z > 17963.0f)
            {
                RenderSettings.fog = true;
            }
        }
    }

    void UpdateFuelScreen()
    {
        if (FuelAvailable < (FuelAvailableBase * 0.3f) && fuelAlertOff)
        {
            fuelAlertOff = false;
            StartCoroutine(BlinkCoroutine());
        }

        //apollo color control
        if (SelectedRocket == 0)
        {
            if (FuelAvailable < (FuelAvailableBase * 0.75))
            {
                StagesApollo[0].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.50))
            {
                StagesApollo[1].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.25))
            {
                StagesApollo[2].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }


        }

        //delta color control
        if (SelectedRocket == 1)
        {
            if (FuelAvailable < (FuelAvailableBase * 0.67))
            {
                StagesDelta[0].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.34))
            {
                StagesDelta[1].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
        }

        //Falcon color control
        if (SelectedRocket == 2)
        {
            if (FuelAvailable < (FuelAvailableBase * 0.84))
            {
                StagesFalcon[0].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.68))
            {
                StagesFalcon[1].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.52))
            {
                StagesFalcon[2].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.36))
            {
                StagesFalcon[3].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (FuelAvailable < (FuelAvailableBase * 0.2))
            {
                StagesFalcon[4].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }

        }

    }

    IEnumerator BlinkCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            CockpitMsgFuel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            CockpitMsgFuel.SetActive(false);
        }
    }

    void MoveAround()
    {
        float localForce = 0;
        if(Input.anyKey)
        {
            localForce = 1;
        }
        else
        {
            this.cameraRigidBody.AddForce((-1) * lastForceX, (-1) * lastForceY, 0, ForceMode.Acceleration);
            //newForceX = 0;
            //newForceY = 0;
        }
        //Debug.Log(joystick.Vertical);
        //Debug.Log(joystick.Horizontal);

        if (joystick.Vertical != 0.0f)
        {
            Debug.Log("VERTICAL");
            this.cameraRigidBody.AddForce(0, moveForce * joystick.Vertical, 0, ForceMode.Acceleration);

        }

        if (joystick.Horizontal != 0.0f)
        {
            Debug.Log("HORIZONTAL");

            this.cameraRigidBody.AddForce(moveForce * joystick.Horizontal, 0, 0, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.cameraRigidBody.AddForce(0, moveForce * localForce, 0, ForceMode.Acceleration);
            //newForceY += moveForce;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.cameraRigidBody.AddForce(0, -moveForce * localForce, 0, ForceMode.Acceleration);
            //newForceY += -moveForce;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.cameraRigidBody.AddForce(-moveForce * localForce, 0, 0, ForceMode.Acceleration);
            //newForceX += -moveForce;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.cameraRigidBody.AddForce(moveForce * localForce, 0, 0, ForceMode.Acceleration);
            //newForceX += moveForce;

        }

        if (Input.GetKey(KeyCode.S))
        {
            this.cameraRigidBody.AddForce(-this.cameraRigidBody.velocity.x, -this.cameraRigidBody.velocity.y, -this.cameraRigidBody.velocity.z, ForceMode.Acceleration);
        }

        //lastForceX = newForceX;
        //lastForceY = newForceY;

    }

    void AssignRocketDetails()
    {
        SelectedRocket = PlayerPrefs.GetInt("SelectedRocket");
        Debug.Log("Selected Rocket = ");
        Debug.Log(SelectedRocket);

        for (int i=0; i < CockpitRocketModels.Length; i++)
        {
            if(i == SelectedRocket)
            {
                //Debug.Log("Setting Rocket = ");
                //Debug.Log(i);
                CockpitRocketModels[i].SetActive(true);
                CockpitModels[i].SetActive(true);
            }
            else
            {
                CockpitRocketModels[i].SetActive(false);
                CockpitModels[i].SetActive(false);

            }
        }
        
        switch(SelectedRocket)
        {
            case 0:
                RocketForce = 1.0f;
                FuelAvailableBase = 440;
                //FuelAvailableBase = 2000;
                FuelAvailable = FuelAvailableBase;

                break;
            case 1:
                RocketForce = 3.0f;
                FuelAvailableBase = 440;
                FuelAvailable = FuelAvailableBase;

                break;
            case 2:
                RocketForce = 5.0f;
                FuelAvailableBase = 480;
                FuelAvailable = FuelAvailableBase;

                break;
        }
        
    }

    void FixedUpdate()
    {
        MoveFoward();
        MoveAround();
    }
}
