using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    [Range(0, 0.5f)]
    [SerializeField] private float _rotationScreenPart;
    [SerializeField] private float _rotationSpeed;
    [Tooltip("X - minimal value, Y - maximum value")]
    [SerializeField] private Vector2 _turnBorders;

    private float _startRotationY;

    private void Awake()
    {
        _startRotationY = transform.localEulerAngles.y;
    }

    private void Update()
    {
        if (Input.mousePosition.x < Screen.width * _rotationScreenPart
            && transform.localEulerAngles.y > _turnBorders.x) { RotateCamera(-1); }
        else if (Input.mousePosition.x > Screen.width * (1 - _rotationScreenPart)
            && transform.localEulerAngles.y < _turnBorders.y) { RotateCamera(1); }
    }

    private void RotateCamera(int rotationDirection)
    {
        transform.Rotate(rotationDirection * Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
