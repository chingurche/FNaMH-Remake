using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class ActionButton : MonoBehaviour
{
    private TMP_Text _buttonText;
    private CanvasGroup _canvasGroup;
    private Button _button;

    private Max _max;
    private Counter _counter;

    private int _currentCameraIndex;

    private void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _button = GetComponent<Button>();

        _max = FindObjectOfType<Max>();
        _counter = FindObjectOfType<Counter>();
    }

    public void PlayAction()
    {
        switch (_currentCameraIndex)
        {
            case 1: case 3: case 5:
                _max.ClickMDSButton(Convert.ToInt32(_currentCameraIndex / 2 + 1f));
                break;
            case 4:
                _counter.RingBell();
                break;
        }
    }

    public void RefreshButton(int currentCameraIndex)
    {
        _currentCameraIndex = currentCameraIndex;

        if (_currentCameraIndex != 2 && _currentCameraIndex <= 5)
        { 
            switch (_currentCameraIndex)
            {
                case 1: case 3: case 5:
                    _buttonText.text = "MDS";
                    break;
                case 4:
                    _buttonText.text = "RING";
                    break;
            }

            _canvasGroup.alpha = 1;
            _button.interactable = true;
        }
        else
        {
            _canvasGroup.alpha = 0;
            _button.interactable = false;
        }
    }
}
