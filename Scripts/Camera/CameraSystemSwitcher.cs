using UnityEngine;

public class CameraSystemSwitcher : MonoBehaviour
{
    private CameraSwitcher _cameraSwitcher;
    private Mops _mops;
    [HideInInspector] public Animator _mainCameraAnimator;
    [HideInInspector] public CameraRotater _mainCameraRotater;

    public Camera _mainCamera;
    [SerializeField] private Canvas _cameraSystemCanvas;

    public bool isSystemWorking { get; private set; }

    private void Awake()
    {
        _cameraSwitcher = GetComponent<CameraSwitcher>();
        _mops = FindObjectOfType<Mops>();

        _mainCameraAnimator = _mainCamera.GetComponent<Animator>();
        _mainCameraRotater = _mainCamera.GetComponent<CameraRotater>();

        SwitchSystemStatus(false);
    }

    public void SwitchSystemStatus(bool systemStatus)
    {
        if (systemStatus == isSystemWorking || (_mops.isMopsStaying && systemStatus)) { return; }

        isSystemWorking = systemStatus;

        if (isSystemWorking)
        {
            _mops.waitingCoroutine = _mops.WaitMops();
            StartCoroutine(_mops.waitingCoroutine);

            _mainCameraAnimator.Play("CameraZoom");
            _mainCamera.transform.Translate(0, 1, 0);
        }
        else
        {
            StopCoroutine(_mops.waitingCoroutine);

            _mainCameraAnimator.Play("CameraZoomOut");
            _mainCamera.transform.Translate(0, -1, 0);
        }

        _mainCameraRotater.enabled = !isSystemWorking;
        _mainCamera.transform.localEulerAngles = Vector3.up * 270f;
        _cameraSwitcher.ChangeCamera(isSystemWorking);
    }
}
