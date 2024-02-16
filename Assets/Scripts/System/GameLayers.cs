using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] LayerMask interactableLayer;

    public static GameLayers instance { get; set; }

    private void Awake()
    {
        instance = this;
    }
    public LayerMask CollisionLayer
    {
        get { return collisionLayer; }
    }
    public LayerMask InteractableLayer
    {
        get { return interactableLayer; }
    }
}
