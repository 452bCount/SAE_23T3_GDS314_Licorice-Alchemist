using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private GameObject obj_Game_Manager;                                            // Manager_Game GameObject
    private Manager_Game gameManager;                                           // access Manager_Game component from Manager_Game GameObject on the hierarchy

    public Camera_Movement camera_Movement;
    public string flavourToWin;
    public TextMeshPro textMesh;
    public Color textColour;
    public Color winColour;

    private void Start()
    {
        if (obj_Game_Manager == null)                                                   // Connect the Mission to the gameObject : "Manager_Game"
            obj_Game_Manager = GameObject.Find("Manager_Game");

        gameManager = obj_Game_Manager.GetComponent<Manager_Game>();                    // Access Manager_Game gameComponent from obj_Game_Manager

        GameObject[] gos = GameObject.FindGameObjectsWithTag("MainCamera");         // Connect the main camera
        foreach (GameObject go_ in gos)
        {
            if (go_.GetComponent<Camera_Movement>())
            {
                //Camera_Board = go_;
                camera_Movement = go_.GetComponent<Camera_Movement>();
            }
        }

        textMesh.text = flavourToWin; textMesh.color = textColour;
    }

    private void OnTriggerEnter(Collider other)
    {
        string var = other.gameObject.GetComponent<Ball>().flavour;
        if(var == flavourToWin)
        {
            textMesh.text = "You Win!"; textMesh.color = winColour;
            Destroy(other.gameObject);
            //Next Level
        }
        else
        {
            gameManager.gamePlay(other.gameObject);
            if (camera_Movement) camera_Movement.Selected_Cam(1);
        }
    }
}
