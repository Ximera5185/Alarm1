using System;
using UnityEngine;

public interface IinputReader
{
    public event Action OnJump;
    public event Action OnSprintStart;
    public event Action OnSprintStop;
    public event Action<float, float> OnLook;
    public event Action<Vector3> OnMove;

    public float DeltaX { get; }
    public float DeltaZ { get; }
    public bool IsMoving { get; }
}