using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Telegram : MonoBehaviour
{
    private CameraSystemSwitcher _cameraSystemSwitcher;
    private IEnumerator _telegramCoroutine;

    private int _messangesClosed;
    private bool _isAttacking;

    [SerializeField] private Canvas _screamMessangeCanvas;
    [SerializeField] private GameObject _screamMessangePrefab;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RawImage[] _messanges;
    
    [SerializeField] private float _startTime;
    [SerializeField] private float _sleepTime;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _screamMessangeSpawnTime;
    [SerializeField] private float _screamMessangeEndTime;
    
    public bool isTelegramWorking { get; private set; }

    private void Start()
    {
        _cameraSystemSwitcher = FindObjectOfType<CameraSystemSwitcher>();

        Invoke("StartTelegram", _startTime);
    }

    private void StartTelegram()
    {
        _telegramCoroutine = TelegramSleep();
        StartCoroutine(_telegramCoroutine);
    }

    private IEnumerator TelegramSleep()
    {
        yield return new WaitForSeconds(_sleepTime);

        _telegramCoroutine = TelegramAttack();
        StartCoroutine(_telegramCoroutine);
    }

    private IEnumerator TelegramAttack()
    {
        _isAttacking = true;

        _messangesClosed = 0;
        foreach (RawImage messange in _messanges)
        {
            messange.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(_attackTime);

        _screamMessangeCanvas.GetComponent<AudioSource>().Play();
        _telegramCoroutine = TelegramSpawnScreamMessange(0);
        StartCoroutine(_telegramCoroutine);
        _isAttacking = false;
    }

    private IEnumerator TelegramSpawnScreamMessange(int messangeNumber)
    {
        var screamMessange = Instantiate(_screamMessangePrefab, _screamMessangeCanvas.transform) as GameObject;
        RawImage screamMessangeImage = screamMessange.GetComponent<RawImage>();
        screamMessangeImage.texture = _messanges[Random.Range(0, _messanges.Length)].texture;
        RectTransform screamMessangeTransform = screamMessange.GetComponent<RectTransform>();
        screamMessangeTransform.anchorMax = new Vector2(1f, 1f);
        screamMessangeTransform.pivot = new Vector2(0.5f, 0.5f);
        screamMessangeTransform.anchoredPosition = new Vector2(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height));
        screamMessangeImage.SetNativeSize();

        yield return new WaitForSeconds(_screamMessangeSpawnTime);

        if (messangeNumber >= _screamMessangeEndTime / _screamMessangeSpawnTime)
        {
            FindObjectOfType<PlayerDeath>().ExitMenu();
        }
        
        _telegramCoroutine = TelegramSpawnScreamMessange(++messangeNumber);
        StartCoroutine(_telegramCoroutine);
    }

    public void CloseMessange(int messangeIndex)
    {
        if (!isTelegramWorking) { return; }

        _messanges[messangeIndex].gameObject.SetActive(false);

        _messangesClosed++;
        if (_messangesClosed == _messanges.Length)
        {
            _isAttacking = false;
            SwitchTelegramStatus(false);

            StopCoroutine(_telegramCoroutine);
            _telegramCoroutine = TelegramSleep();
            StartCoroutine(_telegramCoroutine);
        }
    }
    
    public void SwitchTelegramStatus(bool telegramStatus)
    {
        if (isTelegramWorking == telegramStatus || (!_isAttacking && !isTelegramWorking)) { return; }

        isTelegramWorking = telegramStatus;

        if (isTelegramWorking)
        {
            _cameraSystemSwitcher._mainCameraAnimator.Play("CameraZoom");
            _cameraSystemSwitcher._mainCamera.transform.Translate(0, -0.6f, 0);
        }
        else
        {
            _cameraSystemSwitcher._mainCameraAnimator.Play("CameraZoomOut");
            _cameraSystemSwitcher._mainCamera.transform.Translate(0, 0.6f, 0);
        }

        _cameraSystemSwitcher._mainCameraRotater.enabled = !isTelegramWorking;
        _cameraSystemSwitcher._mainCamera.transform.localEulerAngles = Vector3.up * 323f;
    }
}
