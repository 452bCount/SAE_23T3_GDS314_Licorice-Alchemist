using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

/* We keep to use three stars rating system for the Snake levels.

Zero star is fail the level, player haven't collected the correct amount of flavour dots, then timer countdown to zero;

One star is collected the right amount of flavour dots, but the flavour sequence of snake's body is incorrect; This can happened at any time.

Two stars is collected the right amount of flavour dots, and the flavour sequence of snake's body is 100% correct. But the player spend over 30 seconds to complete the level.

Three stars is almost the same condition of two stars, but player spend less than 30 seconds to complete the level. */

[System.Serializable]
public class FoodRequireList
{
    public FoodBase food;
    [ReadOnly] public bool check;
}

[System.Serializable]
public class FoodGotten
{
    [ReadOnly] public FoodBase food;
    [ReadOnly] public int value;
}

public class LevelStage : MonoBehaviour
{
    [Header("RATING SYSTEM")]
    [ReadOnly] public int star = 0;

    [Separator]

    [Header("TIMER SYSTEM")]
    public float countDown;
    [ReadOnly] public float currentCountDown;
    public bool countDownRun = false;

    [Separator]

    [Header("FOOD REQUIREMENT")]
    public FoodRequireList[] foodRequireList;
    [ReadOnly] public List<FoodBase> foodGottenList;
    [ReadOnly] public FoodGotten[] foodRequire;
    [ReadOnly] public FoodGotten[] foodGotten;
    [ReadOnly] public bool FoodAmountRequireCheck;
    [ReadOnly] public bool FoodSequenceRequireCheck;

    public void SetStart()
    {
        currentCountDown = countDown;
    }


    public void SetUpdate()
    {
        CheckTimer();
    }

    public void SetEaten()
    {
        CheckRequireList();

        // Convert the List<FoodBase> to FoodGotten[]
        ConvertListToArray(foodGottenList, ref foodGotten);

        // Convert the FoodRequireList[] to FoodGotten[]
        ConvertArrayToArray(foodRequireList, ref foodRequire);

        // Compare foodGotten and foodRequire
        CompareFoodArrays();

        // Check the condition
        CheckStar();
    }

    void CheckStar()
    {
        if (!FoodAmountRequireCheck && !FoodSequenceRequireCheck && currentCountDown == 0)
        {
            star = 0;
        }

        else if (FoodAmountRequireCheck && !FoodSequenceRequireCheck)
        {
            star = 1;
        }
        else if (FoodSequenceRequireCheck && currentCountDown <= countDown / 2)
        {
            star = 2;
        }
        else if (FoodSequenceRequireCheck && currentCountDown > countDown / 2)
        {
            star = 3;
        }

        /*
         else if (FoodAmountRequireCheck)
         {
             if (FoodSequenceRequireCheck)
             {
                 if (currentCountDown <= countDown / 2)
                 {
                     star = 2;
                 }

                 else if (currentCountDown > countDown / 2)
                 {
                     star = 3;
                 }
             }

             else
             {
                 star = 1;
             }

         }
         */



    }

    void CheckTimer()
    {
        if (countDownRun)
        {
            if (currentCountDown > 0)
            {
                currentCountDown -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                currentCountDown = 0;
                countDownRun = false;
            }
        }
    }

    void CheckRequireList()
    {
        // Determine the minimum length of the two collections
        int minLength = Mathf.Min(foodRequireList.Length, foodGottenList.Count);

        for (int i = 0; i < minLength; i++)
        {
            // Check if the food at the same index is the same in both arrays
            if (foodRequireList[i].food == foodGottenList[i])
            {
                foodRequireList[i].check = true;
            }
        }

        // Check if all items are checked using LINQ
        FoodSequenceRequireCheck = foodRequireList.All(item => item.check);
    }

    void ConvertListToArray(List<FoodBase> sourceList, ref FoodGotten[] destinationArray)
    {
        // Group the items in sourceList by their food type
        var groupedFood = sourceList
            .GroupBy(item => item)
            .Select(group => new FoodGotten
            {
                food = group.Key,
                value = group.Count()
            })
            .ToArray();

        // Assign the result to the destinationArray
        destinationArray = groupedFood;
    }

    void ConvertArrayToArray(FoodRequireList[] sourceArray, ref FoodGotten[] destinationArray)
    {
        // Group the items in sourceArray by their food type
        var groupedFood = sourceArray
            .GroupBy(item => item.food)
            .Select(group => new FoodGotten
            {
                food = group.Key,
                value = group.Count()
            })
            .ToArray();

        // Assign the result to the destinationArray
        destinationArray = groupedFood;
    }

    void CompareFoodArrays()
    {
        if (foodRequire != null && foodGotten != null)
        {
            // Assume initially that the requirements are met
            bool requirementsMet = true;

            // Iterate through each item in foodRequire
            foreach (var requiredFood in foodRequire)
            {
                // Find the corresponding item in foodGotten (if exists)
                var gottenFood = foodGotten.FirstOrDefault(item => item.food == requiredFood.food);

                // Check if the required food is present in foodGotten
                if (gottenFood != null)
                {
                    // Check if the value in foodGotten is less than the value in foodRequire
                    if (gottenFood.value < requiredFood.value)
                    {
                        // The requirements are not met
                        requirementsMet = false;
                        break; // Exit the loop, no need to check further
                    }
                }
                else
                {
                    // The required food is not present in foodGotten
                    requirementsMet = false;
                    break; // Exit the loop, no need to check further
                }
            }

            // Check the overall result
            if (requirementsMet)
            {
                // Do something when requirements are met
                Debug.Log("Requirements are met!");
                FoodAmountRequireCheck = true;
            }
            else
            {
                // Do something when requirements are not met
                Debug.Log("Requirements are not met!");
                FoodAmountRequireCheck = false;
            }

        }
    }
}
