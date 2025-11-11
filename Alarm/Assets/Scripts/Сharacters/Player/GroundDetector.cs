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
        capsuleRadius,Vector3.down, out RaycastHit hit, _groundDistance,_groundMask);

    private void OnDrawGizmos()
    {
        // Устанавливаем цвет Gizmo
        Gizmos.color = Color.red;

        // Вычисляем верхнюю и нижнюю точки капсулы
        Vector3 top = transform.position + Vector3.up * (capsuleHeight * CapsuleOffset + capsuleRadius);
        Vector3 bottom = transform.position + Vector3.up * (capsuleHeight * CapsuleOffset - capsuleRadius);

        // Рисуем капсулу
        Gizmos.DrawWireSphere(top, capsuleRadius);
        Gizmos.DrawWireSphere(bottom, capsuleRadius);
        Gizmos.DrawLine(top, bottom);
    }
}