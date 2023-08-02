using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private Animator _cripAnimator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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
            _cripAnimator.Play("Crip");
            this.enabled = false;
        }
    }
}
