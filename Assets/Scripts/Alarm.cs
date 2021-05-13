using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _durationVolume;

    private AudioSource _audio;
    private float _currentlVolume, _targetVolume;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audio.Play();
        _targetVolume = 1;
        _currentlVolume = 0;
        StopAllCoroutines();
        StartCoroutine(Ring(_durationVolume));
    }

    public void StopSound()
    {
        _targetVolume = 0;
        _currentlVolume = 1;
        StopAllCoroutines();
        StartCoroutine(QuietDown(_durationVolume));
    }

    private IEnumerator Ring(float duration)
    {
        while (true)
        {
                _audio.volume += Mathf.MoveTowards(_currentlVolume, _targetVolume, 1/Mathf.Pow(duration,2));
            yield return new WaitForSecondsRealtime(1/duration);
        }
    }

    private IEnumerator QuietDown(float duration)
    {
        while (true)
        {
            _audio.volume -= Mathf.MoveTowards(_currentlVolume, _targetVolume, 1 - 1 / Mathf.Pow(duration, 2));
            yield return new WaitForSecondsRealtime(1 / duration);
        }
    }
}
