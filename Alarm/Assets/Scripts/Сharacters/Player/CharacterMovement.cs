using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(GroundDetector), typeof(KeybordMouseInputReader))]
public class CharacterMovement : MonoBehaviour
{
    private const float JumpCoefficient = -2f;

    private enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [SerializeField] private CharacterAnimator _animator;

    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXandY;
    [SerializeField] private float _minSpeed = 0;
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _lerpSpeed = 3;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _gravity = -50f;
    [SerializeField] private float _minVert = -45f;
    [SerializeField] private float _maxVert = 45f;

    private CharacterController _characterController;
    private GroundDetector _groundDetector;
    private IinputReader _inputReader;

    private Vector3 _velocity;
    private Vector3 _direction;

    private float _rotationX = 0f;

    private bool _isJumping = false;
    private bool _isSprinting = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<KeybordMouseInputReader>();

        _characterController.detectCollisions = true;

        _inputReader.OnMove += HandleMove;
        _inputReader.OnJump += HandleJump;
        _inputReader.OnLook += HandleLookInput;
        _inputReader.OnSprintStart += HandleSprintStart;
        _inputReader.OnSprintStop += HandleSprintStop;
    }

    void Update()
    {
        Move(_direction);

        Jump();
    }

    private void HandleMove(Vector3 direction)
    {
        _direction = direction;
    }

    private void HandleLookInput(float mouseX, float mouseY)
    {
        if (_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, mouseX, 0);
        }
        else
        {
            _rotationX -= mouseY;

            _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);

            float rotationY = transform.localEulerAngles.y;

            if (_axes == RotationAxes.MouseY)
            {
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else
            {
                float delta = mouseX;

                rotationY += delta;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }

    private void Jump()
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
            _velocity.y += _gravity * Time.deltaTime;

            _isJumping = false;
        }

        _animator.Jump(_isJumping);
    }

    private void HandleJump()
    {
        _isJumping = true;
    }

    private void HandleSprintStart()
    {
        _isSprinting = true;
    }

    private void HandleSprintStop()
    {
        _isSprinting = false;
    }

    private void Move(Vector3 direction)
    {
        UpdateSpeed();

        _velocity.x = direction.x * _currentSpeed;
        _velocity.z = direction.z * _currentSpeed;

        _characterController.Move(_velocity * Time.deltaTime);

        _animator.Move(_inputReader.DeltaX, _inputReader.DeltaZ, _currentSpeed);
    }

    private void UpdateSpeed()
    {
        if (_inputReader.IsMoving)
        {
            float targetSpeed = _inputReader.IsMoving ? (_groundDetector.IsGrounded ? (_isSprinting ? _runSpeed : _speed) : _minSpeed) : _minSpeed;

            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * _lerpSpeed);

            //Mathf.SmoothDamp(); использовать для инерции
        }
        else
        {
            _currentSpeed = 0;
        }
    }

    private void OnDestroy()
    {
        _inputReader.OnMove -= HandleMove;
        _inputReader.OnJump -= HandleJump;
        _inputReader.OnLook -= HandleLookInput;
        _inputReader.OnSprintStart -= HandleSprintStart;
        _inputReader.OnSprintStop -= HandleSprintStop;
    }
}