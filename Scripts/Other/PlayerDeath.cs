using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour, IOffable
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Death()
    {
        _animator.SetTrigger("ZoomOutTrigger");
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene(0);
    }
}
