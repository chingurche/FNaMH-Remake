using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTime : MonoBehaviour
{
    private bool _isTimeRandom;

    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private AudioSource _endMusic;
    [SerializeField] private GameObject _secretObject;
    [SerializeField] private GameObject _secretButton;
    [SerializeField] private GameObject _blackPanel;
    [SerializeField] private GameObject _deadNote;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("isDead") == 1) { _deadNote.SetActive(true); }
        _isTimeRandom = false;
        StartCoroutine(TimeEnumerator());
    }

    private void FixedUpdate()
    {
        if (_isTimeRandom) { _timeText.text = $"{UnityEngine.Random.Range(1, 10000)} AM"; }
    }

    private IEnumerator TimeEnumerator()
    {
        _timeText.text = "12 PM";
        yield return new WaitForSeconds(120);
        _timeText.text = "1 AM";
        yield return new WaitForSeconds(120);
        _timeText.text = "2 AM";
        yield return new WaitForSeconds(120);
        _timeText.text = "3 AM";
        yield return new WaitForSeconds(120);
        _timeText.text = "4 AM";
        yield return new WaitForSeconds(60);
        _isTimeRandom = true;
        _endMusic.Play();
        yield return new WaitForSeconds(170);
        _secretObject.SetActive(true);
        _secretButton.SetActive(true);
        yield return new WaitForSeconds(10);
        _blackPanel.SetActive(true);
        foreach (MonoBehaviour script in FindObjectsOfType<MonoBehaviour>()) 
        {
            if (script.TryGetComponent<IOffable>(out IOffable offableScript))
            {
                script.enabled = false;
            }
        }
        yield return new WaitForSeconds(18);
        SceneManager.LoadScene(0);
    }
}
