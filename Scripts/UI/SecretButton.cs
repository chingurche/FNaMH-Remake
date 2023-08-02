using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretButton : MonoBehaviour
{
    public void LoadMiniGame()
    {
        if (FindObjectOfType<CameraSwitcher>().cameraIndex == 6)
        {
            SceneManager.LoadScene(2);
        }
    }
}
