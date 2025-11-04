using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _minVert = -45f;
    [SerializeField] private float _maxVert = 45f;
    private float _rotationX = 0f;
    // Start is called before the first frame update
    private enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXandY;
    void Start()
    {
        //_inputReader = GetComponent<InputReader>();
    _inputReader.OnLook += HandleLookInput;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
