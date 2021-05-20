using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _speedSoundChange;

    private AudioSource _audio;
    private float _currentlVolume, _targetVolume, _runningTime;
    private bool isWorking = false;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _runningTime = 0;
        _audio.Play();
        _targetVolume = 1;
        _currentlVolume = 0;
        isWorking = true;
        StartCoroutine(Ring(_speedSoundChange));
    }

    public void StopSound()
    {
        _runningTime = 0;
        _targetVolume = 0;
        _currentlVolume = _audio.volume;
        isWorking = false;
        StartCoroutine(QuietDown(_speedSoundChange));
    }

    private IEnumerator Ring(float duration)
    {
        while (isWorking)
        {
            ChangeVolume();
            yield return new WaitForSecondsRealtime(_runningTime);
        }
    }

    private IEnumerator QuietDown(float duration)
    {
        while (isWorking == false)
        {
            ChangeVolume();
            yield return new WaitForSecondsRealtime(_runningTime);
            if (_audio.volume == 0)
            {
                break;
            }
        }
    }

    private void ChangeVolume()
    {
        _runningTime += (Time.deltaTime);
        _audio.volume = Mathf.MoveTowards(_currentlVolume, _targetVolume, _runningTime * _speedSoundChange);
    }
}
