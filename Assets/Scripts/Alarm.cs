using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _durationVolume;

    private AudioSource _audio;
    private float _currentlVolume, _targetVolume, _runningTime;
    private bool isWorking = false;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audio.Play();
        _targetVolume = 1;
        _currentlVolume = 0;
        isWorking = true;
        StartCoroutine(Ring(_durationVolume));
    }

    public void StopSound()
    {
        _targetVolume = 0;
        _currentlVolume = 1;
        isWorking = false;
        StartCoroutine(QuietDown(_durationVolume));
    }

    private IEnumerator Ring(float duration)
    {
        while (isWorking)
        {
            _runningTime += Time.deltaTime / _durationVolume;
                _audio.volume += Mathf.MoveTowards(_currentlVolume, _targetVolume, _runningTime/_durationVolume);
            yield return new WaitForSecondsRealtime(_runningTime);
        }
    }

    private IEnumerator QuietDown(float duration)
    {
        while (isWorking == false)
        {
            _runningTime += Time.deltaTime;
            _audio.volume -= Mathf.MoveTowards(_currentlVolume, _targetVolume, _currentlVolume - _runningTime/_durationVolume);
            yield return new WaitForSecondsRealtime(_runningTime);
        }
    }
}
