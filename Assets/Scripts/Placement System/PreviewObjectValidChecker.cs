using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewObjectValidChecker : MonoBehaviour
{
    [SerializeField] private LayerMask invalidLayers;
    public bool IsValid { get; private set; } = true;
    private List<Collider> collidingObjects = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        // use bitwise operations to check if the colliding layer is in our invalid layers
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Add(other);
            Debug.Log($"Colliding with {other.name}");
            IsValid = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // use bitwise operations to check if the colliding layer is in our invalid layers
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Remove(other);
            IsValid = collidingObjects.Count <= 0;
        }
    }

}
