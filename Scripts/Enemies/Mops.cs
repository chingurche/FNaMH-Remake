using UnityEngine;
using System.Collections;

public class Mops : MonoBehaviour
{
    public IEnumerator waitingCoroutine;

    private Animator _animator;
    private Renderer _renderer;
    private CameraSystemSwitcher _cameraSystemSwitcher;

    [SerializeField] private float mopsWaitTime;
    [SerializeField] private float mopsTime;

    public bool isMopsStaying { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _cameraSystemSwitcher = FindObjectOfType<CameraSystemSwitcher>();

        waitingCoroutine = WaitMops();
    }

    public IEnumerator WaitMops()
    {
        yield return new WaitForSeconds(mopsWaitTime);
        StartCoroutine(SpawnMops());
    }

    private IEnumerator SpawnMops()
    {
        isMopsStaying = true;
        _cameraSystemSwitcher.SwitchSystemStatus(false);
        _animator.Play("MopsSpawn");
        yield return new WaitForSeconds(mopsTime);
        _animator.Play("MopsOff");
        isMopsStaying = false;
    }
}
