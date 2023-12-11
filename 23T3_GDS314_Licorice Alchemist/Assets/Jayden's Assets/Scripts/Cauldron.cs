using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private GameObject obj_Game_Manager;                                            // Manager_Game GameObject
    private Manager_Game gameManager;                                           // access Manager_Game component from Manager_Game GameObject on the hierarchy

    public Camera_Movement camera_Movement;
    public bool multiFlavour;
    public List<string> winFlavours;
    public string flavourToWin;
    public TextMeshPro textMesh;
    public Color textColour;
    public Color winColour;
    public SoundPlayer player;

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

        if (multiFlavour)
        {
            textMesh.text = winFlavours[0].ToString()  + " + " + winFlavours[1].ToString();
        }
        else
        {
            textMesh.text = flavourToWin; textMesh.color = textColour;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        string var = other.gameObject.GetComponent<Ball>().flavour;
        if (var == flavourToWin && !multiFlavour)
        {
            textMesh.text = "You Win!"; textMesh.color = winColour;
            player.PlayVictorySound();
            Destroy(other.gameObject);
            //Change level from tutorial to level 1 here
        }
        else if(var != flavourToWin && !multiFlavour)
        {
            gameManager.gamePlay(other.gameObject);
            if (camera_Movement) camera_Movement.Selected_Cam(1);
        }
        
        if (multiFlavour && winFlavours.Count <= 2)
        {
            foreach(string flavour in winFlavours.ToList())
            {
                if (flavour == var)
                {
                    winFlavours.Remove(flavour);
                    print("1");
                    
                    if(winFlavours.Count == 0)
                    {
                        print("2");
                        textMesh.text = "You Win!"; textMesh.color = winColour;
                        player.PlayVictorySound();
                        Destroy(other.gameObject);
                        // change scene after level 1
                        return;
                    }
                    else
                    {
                        print("3");
                        textMesh.text = winFlavours[0].ToString();
                        gameManager.gamePlay(other.gameObject);
                        if (camera_Movement) camera_Movement.Selected_Cam(1);
                        return;
                    }
                }
                else if(flavour != var)
                {
                    gameManager.gamePlay(other.gameObject);
                    if (camera_Movement) camera_Movement.Selected_Cam(1);
                    return;
                }
            }
        }

        if(var == "No Flavour" && multiFlavour)
        {
            gameManager.gamePlay(other.gameObject);
            if (camera_Movement) camera_Movement.Selected_Cam(1);
        }
    }
}
