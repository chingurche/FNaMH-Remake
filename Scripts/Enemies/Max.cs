using UnityEngine;
using System.Collections;

public class Max : MonoBehaviour
{
    private IEnumerator _maxCoroutine;

    private int _attackingMaxIndex;

    [SerializeField] private GameObject[] _maxObjects;
    [SerializeField] private CameraButton[] _cameraButtons;
    [SerializeField] private CanvasGroup _loadingCanvasGroup;
    [SerializeField] private AudioSource _mdtAudioSource;
    [SerializeField] private GameObject _maxScreamObject;

    [SerializeField] private Color _normalButtonColor;
    [SerializeField] private Color _unNormalButtonColor;

    [SerializeField] private float _maxStartTime;
    [SerializeField] private float _minSleepTime;
    [SerializeField] private float _maxSleepTime;
    [SerializeField] private float _maxAttackTime;
    [SerializeField] private float _maxFixTime;
    
    public bool isMaxAttacking { get; private set; }

    private void Start()
    {
        foreach (GameObject maxObject in _maxObjects) { maxObject.gameObject.SetActive(false); }

        isMaxAttacking = false;

        Invoke("StartMax", _maxStartTime);
    }

    private void StartMax()
    {
        _maxCoroutine = MaxSleep();
        StartCoroutine(_maxCoroutine);
    }

    private IEnumerator MaxSleep()
    {
        yield return new WaitForSeconds(Random.Range(_minSleepTime, _maxSleepTime));

        _maxCoroutine = MaxAttack();
        StartCoroutine(_maxCoroutine);
    }

    private IEnumerator MaxAttack()
    {
        isMaxAttacking = true;
        _attackingMaxIndex = Random.Range(1, _maxObjects.Length + 1); Debug.Log(_attackingMaxIndex);
        foreach (CameraButton cameraButton in _cameraButtons)
        {
            cameraButton.ChangeButtonColor(_unNormalButtonColor);
        }
        _mdtAudioSource.Play();

        yield return new WaitForSeconds(_maxAttackTime);

        isMaxAttacking = false;
        _mdtAudioSource.Stop();
        _maxCoroutine = MaxScream();
        StartCoroutine(_maxCoroutine);
    }

    public IEnumerator MaxScream()
    {
        _maxScreamObject.SetActive(true);
        FindAnyObjectByType<PlayerDeath>().Death();

        yield return new WaitForSeconds(1);

        FindObjectOfType<PlayerDeath>().ExitMenu();
    }

    public void ClickMDSButton(int cameraIndex)
    {
        if (!isMaxAttacking) { return; }

        _loadingCanvasGroup.alpha = 0.5f;
        _loadingCanvasGroup.blocksRaycasts = true;
        if (cameraIndex == _attackingMaxIndex) { StartCoroutine(MDSAction(cameraIndex, true)); }
        else { StartCoroutine(MDSAction(cameraIndex, false)); }
    }

    private IEnumerator MDSAction(int cameraIndex, bool isMaxHere)
    {
        yield return new WaitForSeconds(_maxFixTime);

        _loadingCanvasGroup.alpha = 0f;
        _loadingCanvasGroup.blocksRaycasts = false;
        if (isMaxHere)
        { 
            _maxObjects[_attackingMaxIndex-1].SetActive(true);
            _maxObjects[_attackingMaxIndex-1].GetComponent<Animator>().Play("Max");
            foreach (CameraButton cameraButton in _cameraButtons)
            {
                cameraButton.ChangeButtonColor(_normalButtonColor);
            }
            isMaxAttacking = false;

            yield return new WaitForSeconds(1);

            _maxObjects[_attackingMaxIndex-1].SetActive(false);
            _mdtAudioSource.Stop();
            StopCoroutine(_maxCoroutine);
            _maxCoroutine = MaxSleep();
            StartCoroutine(_maxCoroutine);
        }
        else
        {
            _cameraButtons[cameraIndex-1].ChangeButtonColor(_normalButtonColor);
        }
    }
}