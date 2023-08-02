using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    internal bool _isCanMove;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private Animator _cripAnimator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _isCanMove = true;
    }

    private void FixedUpdate()
    {
        if (!_isCanMove) { return; }

        _rigidbody.MovePosition(new Vector2(
            Input.GetAxisRaw("Horizontal") * _playerSpeed * Time.fixedDeltaTime + _rigidbody.position.x,
            Input.GetAxisRaw("Vertical") * _playerSpeed * Time.fixedDeltaTime + _rigidbody.position.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CameraTrigger>(out CameraTrigger cameraTrigger))
        {
            cameraTrigger.ActivateTrigger();
        }
        if (other.GetComponent<StopTrigger>())
        {
            GetComponent<Collider2D>().isTrigger = true;
            _cripAnimator.Play("CripTransform", 1);
            PlayerPrefs.SetInt("isDead", 1);
            _isCanMove = false;
        }
        if (other.GetComponent<WindowTrigger>())
        {
            Debug.Log("9351");
            Application.Quit();
        }
    }
}
