using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Get the data from the database
    [SerializeField] public FoodBase _base;
    [HideInInspector] public GameObject asset;

    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        // Set the Name of the object
        this.gameObject.name = _base.Name;

        // Instantiate baseObject and store the reference in instaObject
        asset = Instantiate(_base.Asset, transform.position, transform.rotation);
        // Make sure it doesn't clutter the hierarchy
        asset.transform.SetParent(this.transform);

        // Set the name for the model
        asset.name = "modelAsset";
    }

    private void OnDrawGizmos()
    {
        if (_base)
        {
            Color gizmoColor = _base.ColorEx;
            gizmoColor.a = 0.5f; // Set alpha to 50%
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(this.transform.position, this.transform.localScale);

            float lighteningPercentage = 0.7f; // 50% lighter
            Color lightenedColor = Color.Lerp(gizmoColor, Color.white, lighteningPercentage);
            Gizmos.color = lightenedColor;
            Gizmos.DrawWireCube(this.transform.position, this.transform.localScale);
        }
    }
}
