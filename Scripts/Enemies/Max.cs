using UnityEngine;
using System.Collections;

public class Max : MonoBehaviour
{
    private IEnumerator _maxCoroutine;

    private int _attackingMaxIndex;

    [SerializeField] private GameObject[] _maxObjects;
    [SerializeField] private CanvasGroup _loadingCanvasGroup;

    [SerializeField] private float _maxStartTime;
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
        yield return new WaitForSeconds(_maxSleepTime);

        _maxCoroutine = MaxAttack();
        StartCoroutine(_maxCoroutine);
    }

    private IEnumerator MaxAttack()
    {
        isMaxAttacking = true;
        _attackingMaxIndex = Random.Range(1, _maxObjects.Length + 1); Debug.Log(_attackingMaxIndex);
        for (int i = 1; i <= _maxObjects.Length; i++)
        {
            if (i == _attackingMaxIndex) { _maxObjects[i - 1].gameObject.SetActive(true); }
            else { _maxObjects[i - 1].gameObject.SetActive(false); }
        }

        yield return new WaitForSeconds(_maxAttackTime);

        isMaxAttacking = false;
    }

    public void ClickFixButton(int cameraIndex)
    {
        if (!isMaxAttacking) { return; }

        _loadingCanvasGroup.alpha = 1f;
        _loadingCanvasGroup.blocksRaycasts = true;
        if (cameraIndex == _attackingMaxIndex) { StartCoroutine(FixCamera(cameraIndex, true)); }
        else { StartCoroutine(FixCamera(cameraIndex, false)); }
    }

    private IEnumerator FixCamera(int cameraIndex, bool isMaxHere)
    {
        yield return new WaitForSeconds(_maxFixTime);

        _loadingCanvasGroup.alpha = 0f;
        _loadingCanvasGroup.blocksRaycasts = false;
        if (isMaxHere) { _maxObjects[cameraIndex-1].GetComponent<Animator>().Play("Max"); }
    }
}