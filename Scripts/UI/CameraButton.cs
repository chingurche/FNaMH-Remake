using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraButton : MonoBehaviour
{
    private Image _buttonImage;
    private TMP_Text _buttonText;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeButtonColor(Color color)
    {
        _buttonImage.color = color;
        _buttonText.color = color;
    }
}
