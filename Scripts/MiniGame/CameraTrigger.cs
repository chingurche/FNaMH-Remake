using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Camera _camera;
    private Collider2D _collider;

    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private Vector2 _cameraPosition;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _camera = FindObjectOfType<Camera>();
        _collider = GetComponent<Collider2D>();
    }

    public void ActivateTrigger()
    {
        if (_playerPosition.x == 0) { _playerPosition.x = _playerMovement.transform.position.x; }
        if (_playerPosition.y == 0) { _playerPosition.y = _playerMovement.transform.position.y; }

        _playerMovement.transform.position = _playerPosition;
        _camera.transform.position = new Vector3(_cameraPosition.x, _cameraPosition.y, -10f);
        _collider.isTrigger = false;
    }
}
