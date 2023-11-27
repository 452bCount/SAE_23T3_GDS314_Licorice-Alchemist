using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public int PauseCheck;
    public GameObject PauseMenuUI;
    void Start()
    {
        PauseCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PauseTheScene();
    }

    void PauseTheScene()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PauseCheck == 0)
            {
                Time.timeScale = 0; //Pause the scene;
                PauseCheck = 1;
                PauseMenuUI.SetActive(true);
            }

            else
            {
                PauseMenuUI.SetActive(false);
                PauseCheck = 0;
                Time.timeScale = 1; //Resume the scene;
                
            }
        }
    }

    public void ButtonClickMenu()
    {
        if (PauseCheck == 0)
        {
            Time.timeScale = 0; //Pause the scene;
            PauseCheck = 1;
            PauseMenuUI.SetActive(true);
        }

        else
        {
            PauseMenuUI.SetActive(false);
            PauseCheck = 0;
            Time.timeScale = 1; //Resume the scene;

        }
    }
}
