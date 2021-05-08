using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _durationVolume;

    private AudioSource _audio;
    private float _runningTime, _endingTime, _currentlVolume, _targetVolume;
    private bool _isWorking;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audio.Play();
        _isWorking = true;
        _targetVolume = 1;
        _currentlVolume = 0;
    }

    public void StopSound()
    {
        _isWorking = false;
        _targetVolume = 0;
        _currentlVolume = 1;
    }

    private void Update()
    {      
        if (_isWorking)
        {
            Sound();
        }
        else
        {
            if(_audio.volume > 0)
            {
                Sound();
                return;
            } 
            if (_audio.volume == 0)
            {
                _runningTime = 0;
            }
        }
    }

    private void Sound()
    {
        _runningTime += Time.deltaTime / _durationVolume;
        _audio.volume = Mathf.MoveTowards(_currentlVolume, _targetVolume, _runningTime);
    }
}
