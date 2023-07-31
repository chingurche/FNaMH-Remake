using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Counter : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private NoiseAlphaChanger _noiseAlphaChanger;
    private CameraSwitcher _cameraSwitcher;
    private IEnumerator _counterCoroutine;

    private bool is_Attacking;

    [SerializeField] private AudioClip _ringClip;
    [SerializeField] private AudioClip _oretClip;

    [SerializeField] private float _startTime;
    [SerializeField] private float _sleepingTime;
    [SerializeField] private float _attackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _noiseAlphaChanger = FindObjectOfType<NoiseAlphaChanger>();
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();

        is_Attacking = false;

        Invoke("StartCounter", _startTime);
    }

    private void StartCounter()
    {
        _counterCoroutine = CounterSleep();
        StartCoroutine(_counterCoroutine);
    }

    private IEnumerator CounterSleep()
    {
        _animator.Play("Lying", 0);
        _animator.Play("LyingTransform", 1);

        yield return new WaitForSeconds(_sleepingTime);

        _counterCoroutine = CounterAttack();
        StartCoroutine(_counterCoroutine);
    }

    private IEnumerator CounterAttack()
    {
        is_Attacking = true;
        _audioSource.Play();
        if (_cameraSwitcher.cameraIndex == 4) { _noiseAlphaChanger.ResetCanvasGroupAlpha(); }
        _animator.Play("Jump", 0);
        _animator.Play("JumpTransform", 1);
        
        yield return new WaitForSeconds(_attackTime);

        if (_cameraSwitcher.cameraIndex == 4) { _noiseAlphaChanger.ResetCanvasGroupAlpha(); }
        _animator.Play("Attack", 0);
        _animator.Play("AttackTransform", 1);
        _audioSource.Stop();
        _audioSource.PlayOneShot(_oretClip);
        is_Attacking = false;
    }

    public void RingBell()
    {
        _audioSource.Stop();

        if (is_Attacking)
        {
            if (_cameraSwitcher.cameraIndex == 4) { _noiseAlphaChanger.ResetCanvasGroupAlpha(); }
            StopCoroutine(_counterCoroutine);
            _counterCoroutine = CounterSleep();
            StartCoroutine(_counterCoroutine);
            is_Attacking = false;
        }
        
        _audioSource.PlayOneShot(_ringClip);
    }
}
