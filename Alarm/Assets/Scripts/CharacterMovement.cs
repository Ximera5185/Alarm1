using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(InputReader))]

public class CharacterMovement : MonoBehaviour
{
    private const float JumpCoefficient = -2f;

    [SerializeField] private CharacterAnimator _animator;

    [SerializeField] private float _minSpeed = 0;
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _lerpSpeedLeftShift = 3;
    [SerializeField] private float _lerpSpeedInertia = 4f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _gravity = -50f;

    private CharacterController _characterController;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;

    private Vector3 _velocity;

    private bool _isJumping = false;
    private bool _isDownLeftShift = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _groundDetector = GetComponent<GroundDetector>();

        _inputReader = GetComponent<InputReader>();

        _characterController.detectCollisions = true;

        _inputReader.OnJump += HandleJump;

        _inputReader.OnLeftShift += HandleLeftShift;

        _inputReader.OnLeftShiftReleased += HandleLeftShiftReleased;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        Move(deltaTime);

        Jump(deltaTime);
    }

    private void Jump(float deltaTime)
    {
        bool isGrounded = _groundDetector.IsGrounded;

        if (isGrounded)
        {
            if (_velocity.y < 0)
            {
                _velocity.y = 0;
            }
            if (_isJumping)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * JumpCoefficient * _gravity);
            }
        }
        else
        {
            _velocity.y += _gravity * deltaTime;

            _isJumping = false;
        }

        _animator.Jump(_isJumping);
    }

    private void HandleJump()
    {
        _isJumping = true;
    }

    private void HandleLeftShift()
    {
        _isDownLeftShift = true;
    }

    private void HandleLeftShiftReleased()
    {
        _isDownLeftShift = false;
    }

    private void Move(float deltaTime)
    {
        UpdateSpeed(deltaTime);

        _velocity.x = _inputReader.Direction.x * _currentSpeed;

        _velocity.z = _inputReader.Direction.z * _currentSpeed;

        _characterController.Move(_velocity * deltaTime);

        _animator.Move(_inputReader.DeltaX, _inputReader.DeltaZ, _currentSpeed);
    }

    private void UpdateSpeed(float deltaTime)
    {
        float targetSpeed = _inputReader.IsMovingHorizontallyOrVertically ? (_groundDetector.IsGrounded ? (_isDownLeftShift ? _runSpeed : _speed) : _minSpeed) : _minSpeed;

        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, deltaTime * _lerpSpeedLeftShift);
    }

    void OnDestroy()
    {
        _inputReader.OnJump -= HandleJump;

        _inputReader.OnLeftShift -= HandleLeftShift;

        _inputReader.OnLeftShiftReleased -= HandleLeftShiftReleased;
    }
}