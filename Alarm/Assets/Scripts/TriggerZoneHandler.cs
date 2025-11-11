using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZoneHandler : MonoBehaviour
{
    private const string LayerName = "TrigerZona";

    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerExited;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;

        //gameObject.layer = LayerMask.NameToLayer(LayerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke(other);
    }
}