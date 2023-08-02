using UnityEngine;

public class CripTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            Debug.Log("9351");
            Application.Quit();
        }
    }
}
