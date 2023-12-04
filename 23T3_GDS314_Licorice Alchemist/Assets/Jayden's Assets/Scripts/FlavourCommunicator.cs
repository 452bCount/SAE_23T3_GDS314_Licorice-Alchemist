using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlavourCommunicator : MonoBehaviour
{
    private TextMeshProUGUI textObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponentInChildren<TextMeshProUGUI>();
        textObject.text = "No Flavour";
        textObject.color = Color.black;
    }

    public void UpdateText(string ballFlavour, Color colour)
    {
        textObject.text = ballFlavour;
        textObject.color = colour;
        Debug.Log(colour);
    }
}
