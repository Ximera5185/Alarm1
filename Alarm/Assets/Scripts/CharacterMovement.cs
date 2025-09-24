using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(InputReader))]
public class Mover : MonoBehaviour
{
    private CharacterController _characterController;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private Vector3 _velocity;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private float _minSpeed = 0;
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _lerpSpeedLeftShift = 3;
    [SerializeField] private float _lerpSpeedInertia = 4f;
    [SerializeField] private bool _isJumping = false;


     [SerializeField] private float _jumpHeight = 1f;
     [SerializeField] private float _gravity = -50f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _groundDetector = GetComponent<GroundDetector>();

        _inputReader = GetComponent<InputReader>();

        _characterController.detectCollisions = true;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        Move(deltaTime);

        Jump(deltaTime);
    }

    public void Jump(float deltaTime)
    {
        bool isGrounded = _groundDetector.IsGrounded;

        if (isGrounded)
        {
            if ( _velocity.y < 0)
            {
                _velocity.y = 0;
            }
            if (_inputReader.IsJump)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

                _isJumping = true; 
            }
            else
            {
                _isJumping = false; 
            }
        }
        else
        {
            _velocity.y += _gravity * deltaTime;
        }

        _animator.Jump(_isJumping);
    }

    public void Move(float deltaTime)
    {
        UpdateSpeed(deltaTime);

        _velocity.x = _inputReader.Direction.x * _currentSpeed;

        _velocity.z = _inputReader.Direction.z * _currentSpeed;

        _characterController.Move(_velocity * deltaTime);

        _animator.Move(_inputReader.deltaX, _inputReader.deltaZ, _currentSpeed);
    }

    private void UpdateSpeed(float deltaTime)
    {
        float targetSpeed = _inputReader.IsMovingHorizontallyOrVertically ? (_groundDetector.IsGrounded ? (_inputReader.IsLeftShift ? _runSpeed : _speed) : _minSpeed) : _minSpeed;

        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, deltaTime* _lerpSpeedLeftShift);
    }
}