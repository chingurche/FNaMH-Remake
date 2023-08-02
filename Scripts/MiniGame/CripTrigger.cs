using UnityEngine;

public class CripTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _deadCrip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement._isCanMove = true;
            playerMovement.GetComponent<Collider2D>().isTrigger = false;
            _deadCrip.SetActive(true);
            _deadCrip.GetComponent<Animator>().Play("Crip", 0);
            Destroy(gameObject);
        }
    }
}
