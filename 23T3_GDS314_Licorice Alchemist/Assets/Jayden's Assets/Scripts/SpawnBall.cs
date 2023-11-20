using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ball;
    
    public void InstanciateBall()
    {
        Instantiate(ball, transform);
    }
}
