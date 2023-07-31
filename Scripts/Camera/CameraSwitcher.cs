using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private ActionButton _actionButton;
    private NoiseAlphaChanger _noiseAlphaChanger;

    [SerializeField] private Camera[] _systemCameras;

    public int cameraIndex { get; private set; }

    private void Awake()
    {
        _actionButton = FindObjectOfType<ActionButton>();
        _noiseAlphaChanger = FindObjectOfType<NoiseAlphaChanger>();

        cameraIndex = 1;
    }

    public void ChangeCameraIndex(int newCameraIndex)
    {
        if (cameraIndex == newCameraIndex) { return; }
        
        cameraIndex = newCameraIndex;
        ChangeCamera(true);
    }

    public void ChangeCamera(bool _isOn)
    {
        for (int i = 0; i < _systemCameras.Length; i++)
        {
            if (i+1 == cameraIndex && _isOn) { _systemCameras[i].enabled = true; }
            else { _systemCameras[i].enabled = false;}
        }

        _actionButton.RefreshButton(cameraIndex);
        _noiseAlphaChanger.ResetCanvasGroupAlpha();
    }
}
