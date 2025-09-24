using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    public float deltaX { get; private set; }
    public float deltaZ { get; private set; }

    public bool IsMovingHorizontallyOrVertically { get; private set; }

    public bool IsJump { get; private set; }

    public bool IsLeftShift { get; private set; }

    public Vector3 Direction { get; private set; }

    void Update()
    {
        IsMovingHorizontallyOrVertically = Mathf.Abs(deltaX) > 0 || Mathf.Abs(deltaZ) > 0;

        if (Input.GetButtonDown("Jump"))
        {
            IsJump = true;
        }
        else
        {
            IsJump = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsLeftShift = true;
        }
        else
        {
            IsLeftShift = false;
        }

        deltaX = Input.GetAxis(HorizontalAxis);

        deltaZ = Input.GetAxis(VerticalAxis);

        Direction = new Vector3(deltaX, 0, deltaZ);

        Direction = transform.TransformDirection(Direction);
    }

    //систему событий
}
