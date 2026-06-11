using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private float _additionalTime = 3f;

    public void Initialize(AudioResource audioResource, AudioMixerGroup audioMixerGroup)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.Play();
        
        if(_audioSource.clip == null)
        {
            Destroy(this.gameObject, _additionalTime);
            return;
        }

        Destroy(this.gameObject, _audioSource.clip.length + _additionalTime);
    }

    public void Initialize(AudioResource audioResource, AudioMixerGroup audioMixerGroup, Action action = null)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.Play();
        action?.Invoke();

        if (_audioSource.clip == null)
        {
            Destroy(this.gameObject, _additionalTime);
            return;
        }

        Destroy(this.gameObject, _audioSource.clip.length + _additionalTime);
    }

    public async void InitializeWaitForSeconds(AudioResource audioResource, AudioMixerGroup audioMixerGroup, float time)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        await Awaitable.WaitForSecondsAsync(time);
        _audioSource.Play();

        if (_audioSource.clip == null)
        {
            Destroy(this.gameObject, _additionalTime);
            return;
        }

        Destroy(this.gameObject, _audioSource.clip.length + _additionalTime);
    }

    public async void InitializeWaitForSeconds(AudioResource audioResource, AudioMixerGroup audioMixerGroup, float time, Action action = null)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        await Awaitable.WaitForSecondsAsync(time);
        _audioSource.Play();
        action?.Invoke();

        if (_audioSource.clip == null)
        {
            Destroy(this.gameObject, _additionalTime);
            return;
        }

        Destroy(this.gameObject, _audioSource.clip.length + _additionalTime);
    }
}
