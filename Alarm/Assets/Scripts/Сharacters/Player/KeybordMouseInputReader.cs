using System;
using UnityEngine;

public class KeybordMouseInputReader : MonoBehaviour, IinputReader
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string JumpButtonName = "Jump";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private float _sensitivityHor = 9f;
    [SerializeField] private float _sensitivityVert = 9f;

    private float _previousMouseX;
    private float _previousMouseY;

    private const KeyCode LeftShiftKey = KeyCode.LeftShift;

    public event Action OnJump;
    public event Action OnSprintStart;
    public event Action OnSprintStop;
    public event Action<float, float> OnLook;
    public event Action<Vector3> OnMove;

    public float DeltaX { get; private set; }
    public float DeltaZ { get; private set; }
    public bool IsMoving { get; private set; }

    public Vector3 Direction { get; private set; }

    private void Update()
    {
        ReadMovementInput();
        ReadLookInput();
        ReadJumpInput();
        ReadCurrentStateLeftShift(LeftShiftKey);

    }

    private void ReadMovementInput()
    {
        DeltaX = Input.GetAxis(HorizontalAxis);
        DeltaZ = Input.GetAxis(VerticalAxis);

        IsMoving = Mathf.Abs(DeltaX) > 0 || Mathf.Abs(DeltaZ) > 0;

        Direction = new Vector3(DeltaX, 0, DeltaZ).normalized;
        Direction = transform.TransformDirection(Direction);

        if (IsMoving)
        {
            OnMove?.Invoke(Direction);
        }
    }

    private void ReadLookInput()
    {
        float mouseX = Input.GetAxis(MouseX) * _sensitivityHor;
        float mouseY = Input.GetAxis(MouseY) * _sensitivityVert;

        if (mouseX != _previousMouseX || mouseY != _previousMouseY)
        {
            OnLook?.Invoke(mouseX, mouseY);

            _previousMouseX = mouseX;
            _previousMouseY = mouseY;
        }
    }

    private void ReadJumpInput()
    {
        if (Input.GetButtonDown(JumpButtonName))
        {
            OnJump?.Invoke();
        }
    }

    private void ReadCurrentStateLeftShift(KeyCode LeftShiftKey)
    {
        if (Input.GetKey(LeftShiftKey))
        {
            OnSprintStart?.Invoke();
        }

        if (Input.GetKeyUp(LeftShiftKey))
        {
            OnSprintStop?.Invoke();
        }
    }
}