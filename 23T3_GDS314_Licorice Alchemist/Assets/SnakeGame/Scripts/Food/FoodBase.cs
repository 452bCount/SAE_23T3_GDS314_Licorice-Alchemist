using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodBase", menuName = "SnakeGame/Create New Food")]
public class FoodBase : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] GameObject asset;
    [SerializeField] Color _colorEx;

    public string Name { get { return _name; } }
    public GameObject Asset { get { return asset; } }
    public Color ColorEx { get { return _colorEx; } }
}
