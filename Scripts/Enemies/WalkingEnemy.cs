using UnityEngine;
using System.Collections;

public class WalkingEnemy : MonoBehaviour, IOffable
{
    private CameraSwitcher _cameraSwitcher;
    private NoiseAlphaChanger _noiseAlphaChanger;

    private int _currentPhase;
    private IEnumerator _phaseCoroutine;
    private IEnumerator _lastPhaseCoroutine;

    [Header("Data")]
    [SerializeField] private Rotator _enemyRotator;
    [SerializeField] private int _rollbackPhase;
    [SerializeField] private float _enemyStartPhaseTime;
    [SerializeField] private float _enemyLastPhaseTime;
    [SerializeField] private float _enemyPhaseMinTime;
    [SerializeField] private float _enemyPhaseMaxTime;
    [SerializeField] private EnemyPhase[] _enemyPhases;

    private void Awake()
    {
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        _noiseAlphaChanger = FindObjectOfType<NoiseAlphaChanger>();
    }

    private void Start()
    {  
        foreach (EnemyPhase enemyPhase in _enemyPhases)
        {
            enemyPhase.enemyGameObject.SetActive(false);
        }
        _enemyPhases[0].enemyGameObject.SetActive(true);

        Invoke("ResetPhaseEnumerator", _enemyStartPhaseTime);
    }

    private void ResetPhaseEnumerator()
    {
        _phaseCoroutine = PhaseEnumerator();
        StartCoroutine(_phaseCoroutine);
    }

    private IEnumerator PhaseEnumerator()
    {
        yield return new WaitForSeconds(Random.Range(_enemyPhaseMinTime, _enemyPhaseMaxTime));

        ChangePhase(_currentPhase + 1);

        SetCameraNoise(_enemyPhases[_currentPhase].previousCamera);
        SetCameraNoise(_enemyPhases[_currentPhase].currentCamera);

        if (_currentPhase == _enemyPhases.Length - 2)
        {
            _lastPhaseCoroutine = LastPhaseEnumerator();
            StartCoroutine(_lastPhaseCoroutine);
        }
        else
        {
            ResetPhaseEnumerator();
        }
    }

    private IEnumerator LastPhaseEnumerator()
    {
        _enemyRotator.isOpening = true;
        yield return new WaitForSeconds(_enemyLastPhaseTime);

        ChangePhase(_currentPhase + 1);
    }

    public void StopLastPhase()
    {
        StopCoroutine(_lastPhaseCoroutine);
        ChangePhase(_rollbackPhase);
        ResetPhaseEnumerator();
    }

    private void ChangePhase(int nextPhase)
    {
        SwitchPhase(_enemyPhases[_currentPhase], false);
        _currentPhase = nextPhase;
        SwitchPhase(_enemyPhases[_currentPhase], true);
    }

    private void SwitchPhase(EnemyPhase enemyPhase, bool phaseState)
    {
        enemyPhase.enemyGameObject.SetActive(phaseState);
    }

    private void SetCameraNoise(int cameraIndex)
    {
        if (_cameraSwitcher.cameraIndex == cameraIndex)
        {
            _noiseAlphaChanger.ResetCanvasGroupAlpha();
        }
    }

    [System.Serializable]
    public class EnemyPhase
    {
        public GameObject enemyGameObject;
        public int previousCamera, currentCamera;
    }
}
