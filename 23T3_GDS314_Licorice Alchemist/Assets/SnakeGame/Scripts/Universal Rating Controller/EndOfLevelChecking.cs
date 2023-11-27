using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelChecking : MonoBehaviour
{
    public int RatingResult;
    public LevelStage StarResult;
    public GameObject RatingStarUI1;
    public GameObject RatingStarUI2;
    public GameObject RatingStarUI3;
    public float EndOfLevelTimer;
    public GameObject NotificationUISets;
    void Start()
    {
        RatingResult = 4; // 4 means not triggered statement;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTheLevelEnd();
        NotificationUI();

        if (EndOfLevelTimer <= 0)
        {
            ShowTheRatingResultUI();
        }
    }

    void CheckTheLevelEnd()
    {
        if (StarResult.star != 0)
        {
            RatingResult = StarResult.star;

            Time.timeScale = 0; //Pause the scene;
        }
    }

    void NotificationUI()
    {
        if (StarResult.star != 0 && EndOfLevelTimer > 0)
        {
           EndOfLevelTimer = EndOfLevelTimer - 0.1f;
           NotificationUISets.SetActive(true);

            if (EndOfLevelTimer < 0) 
            {
                EndOfLevelTimer = 0;
            }
        }

        else if (StarResult.star != 0 && EndOfLevelTimer <= 0) 
        {
            NotificationUISets.SetActive(false);
        }
    }

    void ShowTheRatingResultUI()
    {
        if (RatingResult == 1)
        {
            RatingStarUI1.SetActive(true);
        }

        else if (RatingResult == 2)
        {
            RatingStarUI2.SetActive(true);
        }

        else if (RatingResult == 3)
        {
            RatingStarUI3.SetActive(true);
        }

    }

}
