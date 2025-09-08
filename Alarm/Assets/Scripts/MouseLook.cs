using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXandY;

    [SerializeField] private float _sensitivityHor = 9f;
    [SerializeField] private float _sensitivityVert = 9f;
    [SerializeField] private float _minVert = -45f;
    [SerializeField] private float _maxVert = 45f;

    private float _rotationX = 0f;

    private void Update()
    {
        HandleMouseLook();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivityHor;

        float mouseY = Input.GetAxis("Mouse Y") * _sensitivityVert;

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