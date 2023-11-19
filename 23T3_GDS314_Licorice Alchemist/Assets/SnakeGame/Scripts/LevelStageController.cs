using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageController : MonoBehaviour
{
    public static LevelStageController Instance { get; private set; }

    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }

    [Header("AUTO REFRENCE DRAG & DROP")]
    [ReadOnly] public Snake snake;
    [ReadOnly] public LevelStage levelStage;

    [Separator]

    [Header("REFRENCE DRAG & DROP")]
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] public GameObject foodRequireListPanel;
    [SerializeField] public GameObject foodGottenListPanel;
    [SerializeField] GameObject foodRequireUI;
    [SerializeField] GameObject foodGottenUI;

    [Header("READ-ONLY STUFF")]
    [ReadOnly] [SerializeField] bool gameOver;

    private void Start()
    {
        if (snake == null) { snake = FindObjectOfType<Snake>(); }
        if (levelStage == null) { levelStage = FindObjectOfType<LevelStage>(); }
        if (snake != null) { snake.SetStart(); }
        if (levelStage != null) { levelStage.SetStart(); }
        // InstantiateFoodRequireListPanel();
    }

    private void Update()
    {
        if (!gameOver && snake == null)
        {
            gameOver = snake._isDead;
            snake = null;
        }

        if (levelStage != null)
        {
            levelStage.SetUpdate();
        }

        if (!gameOver && snake != null)
        {
            snake.SetUpdate();
        }

        if (gameOver)
        {
            // Lose State
            if (levelStage.star == 0)
            { 
            
            }
        }
    }

    public void InstantiateFoodRequireListPanel()
    {
        // Clear existing UI elements in the panel
        foreach (Transform child in foodRequireListPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < levelStage.foodRequireList.Length; i++)
        {
            // Instantiate the foodRequireUIPrefab
            GameObject uiElement = Instantiate(foodRequireUI, foodRequireListPanel.transform);
            uiElement.GetComponent<Image>().color = levelStage.foodRequireList[i].food.ColorEx;
            uiElement.name = $"{ levelStage.foodRequireList[i].food.name } UI";
        }
    }

    public void InstantiateGottenRequireListPanel()
    {

    }
}
