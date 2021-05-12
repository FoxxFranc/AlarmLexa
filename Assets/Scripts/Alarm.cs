using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _durationVolume;

    private AudioSource _audio;
    private float _currentlVolume, _targetVolume;
    private bool _isWorking;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        StartCoroutine(Ring(_durationVolume));
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

    private IEnumerator Ring(float duration)
    {
        while (true)
        {
            if (_isWorking)
            {
                _audio.volume += Mathf.MoveTowards(_currentlVolume, _targetVolume, 1/duration/duration);
            }
            else
            {
                _audio.volume -= Mathf.MoveTowards(_currentlVolume, _targetVolume, 1 - (1/duration/duration));
            }
            yield return new WaitForSecondsRealtime(1/duration);
        }
    }
}
