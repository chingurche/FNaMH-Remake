using UnityEngine;

public class Rotator : MonoBehaviour, IOffable
{
    private Vector3 _startRotation;

    [HideInInspector] public bool isClosing;
    [HideInInspector] public bool isOpening;

    [SerializeField] private WalkingEnemy _walkingEnemy;

    [SerializeField] private int _rotationDirection;
    [SerializeField] private float _openingTime;
    [SerializeField] private float _closingTime;
    [SerializeField] private float _startAngle;
    [SerializeField] private float _endAngle;

    private void Awake()
    {
        _startRotation = transform.localEulerAngles;
        isOpening = false;
    }

    private void Update()
    {
        if (!isOpening) { return; }

        if (_rotationDirection == 1)
        {
            if (isClosing && transform.localEulerAngles.y > _startRotation.y)
            {
                RotateObject(-_closingTime);
            }
            if (isClosing && transform.localEulerAngles.y < _startRotation.y)
            {
                _walkingEnemy.StopLastPhase();
                isOpening = false;
                isClosing = false;
                SetStartRotation();
            }

            if (transform.localEulerAngles.y < _endAngle)
            {
                RotateObject(_openingTime);
            }
        }
        else if (_rotationDirection == -1)
        {
            if (isClosing && transform.localEulerAngles.y < _startRotation.y)
            {
                RotateObject(-_closingTime);
            }
            if (isClosing && transform.localEulerAngles.y > _startRotation.y)
            {
                _walkingEnemy.StopLastPhase();
                isOpening = false;
                isClosing = false;
                SetStartRotation();
            }

            if(transform.localEulerAngles.y > _endAngle)
            {
                RotateObject(_openingTime);
            }
        }
    }

    private void RotateObject(float openingTime)
    {
        transform.Rotate((_endAngle - _startRotation.y) * Vector3.up * Time.deltaTime / openingTime);
    }

    private void SetStartRotation()
    {
        transform.localEulerAngles = _startRotation;
    }
}
