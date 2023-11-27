using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualTimerUI : MonoBehaviour
{
    public float timeUI;
    public Text TimerText;
    public Image Fill;
    public float Max;
    public PauseMenuController VarCheck;
    public LevelStage StarResultCheck;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NonPauseTimer();
    }

    void NonPauseTimer()
    {
        if (VarCheck.PauseCheck == 0 && StarResultCheck.star == 0)
        {
            timeUI -= Time.deltaTime;
            TimerText.text = "" + (int)timeUI;
            Fill.fillAmount = timeUI / Max;

            if (timeUI < 0)
            {
                timeUI = 0;
            }
        }
    
    }
}
