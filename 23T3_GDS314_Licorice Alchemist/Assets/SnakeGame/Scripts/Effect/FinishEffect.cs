using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEffect : MonoBehaviour
{
    [HideInInspector] public Color exColorA, exColorB;
    public ParticleSystem ParticleSystem;

    public void SetUp()
    {
        float lighteningPercentage = 0.5f; // 50% lighter
        exColorB = Color.Lerp(exColorA, Color.white, lighteningPercentage);

        // Get all components of type YourComponentName in the children of this GameObject
        ParticleSystem[] components = GetComponentsInChildren<ParticleSystem>();

        // Iterate through each component and modify the value
        foreach (ParticleSystem component in components)
        {
            // Change the start color of the Particle System
            ChangeStartColor(component, exColorA, exColorB);
            //component.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ParticleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    void ChangeStartColor(ParticleSystem pSystem, Color color1, Color color2)
    {
        // Get the main module of the Particle System
        var mainModule = pSystem.main;

        // Set the start color to be a random color between color1 and color2
        mainModule.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
    }
}
