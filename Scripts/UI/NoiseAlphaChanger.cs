using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class NoiseAlphaChanger : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private CameraSystemSwitcher _cameraSystemSwitcher;

    [SerializeField] private float _alphaTransitionTime;
    [SerializeField] private float _minAlpha;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _cameraSystemSwitcher = FindObjectOfType<CameraSystemSwitcher>();
    }

    private void Update()
    {
        if (_canvasGroup.alpha > _minAlpha && _cameraSystemSwitcher.isSystemWorking) { _canvasGroup.alpha -= _alphaTransitionTime * Time.deltaTime; }
        else if (_canvasGroup.alpha < _minAlpha){ _canvasGroup.alpha = _minAlpha; }
    }

    public void ResetCanvasGroupAlpha()
    {
        _canvasGroup.alpha = 1;
    }
}
