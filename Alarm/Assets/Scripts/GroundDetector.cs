using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _groundDistance = 0.1f; 

    public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, _groundDistance, _groundMask);
}