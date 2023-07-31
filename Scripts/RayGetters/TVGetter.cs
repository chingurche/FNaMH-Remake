using UnityEngine;

public class TVGetter : MonoBehaviour, IRayGetter
{
    private CameraSystemSwitcher _cameraSystemSwitcher;

    private void Awake()
    {
        _cameraSystemSwitcher = FindObjectOfType<CameraSystemSwitcher>();
    }

    void IRayGetter.SendToReceiver()
    {
        if (!_cameraSystemSwitcher.isSystemWorking)
        { 
            _cameraSystemSwitcher.SwitchSystemStatus(true);
        }
    }
}
