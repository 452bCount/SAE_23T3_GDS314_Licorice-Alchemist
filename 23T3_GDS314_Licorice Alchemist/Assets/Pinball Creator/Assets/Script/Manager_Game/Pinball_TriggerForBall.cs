﻿// Pinball_TriggerForBall : Description : Detect when a ball is lost : Use by Out_Hole_TriggerDestroyBall gameObject on the hierarchy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball_TriggerForBall : MonoBehaviour {

	private GameObject obj_Game_Manager;											// Manager_Game GameObject
	private Manager_Game gameManager;                                           // access Manager_Game component from Manager_Game GameObject on the hierarchy
	private float timeRemaining = 1f;
	private bool timerIsRunning = false;
	public GameObject newBallPos;
	public Camera_Movement camera_Movement;

	void Start(){																	// --> Function Start
		if (obj_Game_Manager == null)													// Connect the Mission to the gameObject : "Manager_Game"
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
    }

	public void Update()
	{
		if (timerIsRunning)
		{
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
			else
			{
				Debug.Log("Timer Done");
				timeRemaining = 0;
				timerIsRunning = false;
			}
        }
	}

	void OnTriggerEnter (Collider other) {										// --> Function OnTriggerEnter
		if(other.transform.tag == "Ball"){												// If it's a ball 
			//gameManager.gamePlay(other.gameObject);										// Send Message to the obj_Game_Manager.
			if (camera_Movement) camera_Movement.Selected_Cam(2);
			timerIsRunning = true;
			
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Ball")
		{
            if (timeRemaining == 0)
            {
                other.transform.position = newBallPos.transform.position;
            }
        }
    }

}
