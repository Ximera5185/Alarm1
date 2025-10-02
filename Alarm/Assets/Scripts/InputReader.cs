using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string JumpButtonName = "Jump";

    public event Action OnJump;
    public event Action OnLeftShift;
    public event Action OnLeftShiftReleased;

    public float DeltaX { get; private set; }
    public float DeltaZ { get; private set; }
    public bool IsMovingHorizontallyOrVertically { get; private set; }

    public Vector3 Direction { get; private set; }

    void Update()
    {
        IsMovingHorizontallyOrVertically = Mathf.Abs(DeltaX) > 0 || Mathf.Abs(DeltaZ) > 0;

        CheckJumpInput();

        CheckCurrentStateLeftShift();
        DeltaX = Input.GetAxis(HorizontalAxis);
        DeltaZ = Input.GetAxis(VerticalAxis);

        Direction = new Vector3(DeltaX, 0, DeltaZ);

        Direction = transform.TransformDirection(Direction);
    }

    private void CheckJumpInput()
    {
        if (Input.GetButtonDown(JumpButtonName))
        {
            OnJump?.Invoke();
        }
    }

    private void CheckCurrentStateLeftShift() 
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnLeftShift?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnLeftShiftReleased?.Invoke();
        }
    }
}