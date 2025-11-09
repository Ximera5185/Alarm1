using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _groundDistance = 0.1f;
    [SerializeField] private float capsuleHeight = 2f;
    [SerializeField] private float capsuleRadius = 0.5f;

    private const float CapsuleOffset = 0.5f;

    public bool IsGrounded => Physics.CapsuleCast(
        transform.position + Vector3.up * (capsuleHeight * CapsuleOffset + capsuleRadius),
        transform.position + Vector3.up * (capsuleHeight * CapsuleOffset - capsuleRadius),
        capsuleRadius,
        Vector3.down,
        out RaycastHit hit,
        _groundDistance,
        _groundMask);
}