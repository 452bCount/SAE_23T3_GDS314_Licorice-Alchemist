using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect = null;
    [HideInInspector] [SerializeField] public Color exColor;

    public void DeathExplode()
    {
        // play graphics
        GameObject explodeAsset;
        explodeAsset = Instantiate(explosionEffect, this.transform.position, this.transform.rotation);
        explodeAsset.GetComponent<FinishEffect>().exColorA = exColor;
        explodeAsset.GetComponent<FinishEffect>().SetUp();
        // play sound
    }
}
