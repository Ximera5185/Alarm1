using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _groundDistance = 0.1f;
    [SerializeField] private float _sphereRadius = 0.4f;
    [SerializeField] private float _heightOffset = 5f;

    [SerializeField] private float capsuleHeight = 2f; // Высота капсулы
    [SerializeField] private float capsuleRadius = 0.5f; // Радиус капсулы
                                                         //public bool IsGrounded => Physics.CapsuleCast(transform.position, Vector3.down, _groundDistance, _groundMask);

    // public bool IsGrounded => Physics.SphereCast(transform.position + Vector3.up * _heightOffset , _sphereRadius, Vector3.down, out RaycastHit hit, _groundDistance, _groundMask);

    /* private void OnDrawGizmos()
     {
         // Устанавливаем цвет Gizmos (например, зеленый для "земли")
         Gizmos.color = Color.green;

         // Рисуем сферу в позиции объекта с заданным радиусом
         Gizmos.DrawWireSphere(transform.position + Vector3.up * _heightOffset, _sphereRadius);

         // Рисуем линию вниз, чтобы визуализировать направление SphereCast
         Gizmos.DrawLine(transform.position + Vector3.up * _heightOffset, transform.position + Vector3.down * (_groundDistance + _sphereRadius));
     }*/
    

    public bool IsGrounded => Physics.CapsuleCast(transform.position + Vector3.up * (capsuleHeight / 2 + capsuleRadius), transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius), capsuleRadius, Vector3.down, out RaycastHit hit, _groundDistance, _groundMask);

    private void OnDrawGizmos()
    {
        // Визуализируем капсулу в редакторе
        Gizmos.color = Color.green;
        Vector3 bottom = transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
        Vector3 top = transform.position + Vector3.up * (capsuleHeight / 2 + capsuleRadius);
        Gizmos.DrawWireSphere(top, capsuleRadius);
        Gizmos.DrawWireSphere(bottom, capsuleRadius);
        Gizmos.DrawLine(top, bottom);
        
    }
}