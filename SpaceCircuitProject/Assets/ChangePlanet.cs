using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlanet : MonoBehaviour
{
    //Shader shader;
    public Material myMaterial;

    GameObject planebackground;

    //GameObject planetBackground;
    // Start is called before the first frame update
    void Start()
    {
        //shader = GetComponent<Shader>();

        planebackground = GameObject.Find("PlaneBackground");


        planebackground.GetComponent<MeshRenderer>().material = myMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
