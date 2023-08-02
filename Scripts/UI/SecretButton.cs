using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretButton : MonoBehaviour
{
    public void LoadMiniGame()
    {
        if (FindObjectOfType<CameraSwitcher>().cameraIndex == 7)
        {
            SceneManager.LoadScene(2);
        }
    }
}
