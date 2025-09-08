using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSInput : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 7;   // Рекомендуемое значение 8
    [SerializeField] private float _speed = 3;      // Рекомендуемое значение 5
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _lerpSpeedLeftShift = 3;  // Рекомендуемое значение 3
    [SerializeField] private float _lerpSpeedInertia = 4f;
    [SerializeField] private float _groundCheckDistance = 0.5f; // Дистанция проверки земли
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private LayerMask _groundMask;


    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _velocity;
    [SerializeField] private bool _isJumping = false;
    private Vector3 movement;

    float deltaX ;
    float deltaZ;
    
    private void Awake ()
    {
        _characterController = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Debug.Log("Character Controller initialized.");
        Debug.Log("Current Speed: " + _currentSpeed);
    }
    private void Update()
    {
        HandleMovement();
    }

    private void CheckGrounded()
    {
        // Проверяем, находится ли персонаж на каком-либо коллайдере
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance,_groundMask);
    }

    private void HandleMovement()
    {
        CheckGrounded();

         
        if (_isGrounded)
        {
            // Сбрасываем вертикальную скорость, если на земле
            _velocity.y = 0;

            // Проверяем, удерживается ли Shift, и плавно изменяем скорость
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, _runSpeed, Time.fixedDeltaTime * _lerpSpeedLeftShift); // Увеличиваем скорость
            }
            else
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, _speed, Time.fixedDeltaTime * _lerpSpeedLeftShift); // Уменьшаем скорость
            }

            // Обработка прыжка
            if (Input.GetButtonDown("Jump") && _isGrounded == true)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

                _isJumping = true; // Устанавливаем состояние прыжка
                _animator.SetBool("Jumping", _isJumping);
            }
            else
            {
                _isJumping = false; // Если не прыгаем, сбрасываем состояние
                _animator.SetBool("Jumping", _isJumping);
            }
        }
        else
        {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.fixedDeltaTime * _lerpSpeedInertia); // Уменьшаем скорость
            // Если не на земле, добавляем гравитацию
            _velocity.y += _gravity * Time.deltaTime;

            

            _animator.SetBool("Jumping", _isJumping); // Убедитесь, что анимация прыжка активна, когда не на земле
        }
        deltaX = Input.GetAxis("Horizontal") * _currentSpeed;
        Debug.Log(Input.GetAxis("Horizontal"));
       deltaZ = Input.GetAxis("Vertical") * _currentSpeed;
        Debug.Log(Input.GetAxis("Vertical"));

       /* if (_isGrounded == false)
        {
            deltaX = 0f;
            deltaZ = 0f;
        }*/


        movement = new Vector3(deltaX, 0, deltaZ);
             movement = transform.TransformDirection(movement);
       

        _velocity.x = movement.x;
        _velocity.z = movement.z;

        // Перемещение персонажа
        _characterController.Move(_velocity * Time.deltaTime);
        // Установка параметров анимации
        _animator.SetFloat("Speed", movement.magnitude);
        _animator.SetFloat("HorizontalMove", deltaX);
        _animator.SetFloat("VerticalMove", deltaZ);

        // Логирование состояния isGrounded
    }
}