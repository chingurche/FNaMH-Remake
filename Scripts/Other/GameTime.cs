using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTime : MonoBehaviour
{
    private bool _isTimeRandom;

    [SerializeField] TMP_Text _timeText;
    [SerializeField] AudioSource _endMusic;
    
    private void Start()
    {
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
        yield return new WaitForSeconds(180);
        SceneManager.LoadScene(0);
    }
}