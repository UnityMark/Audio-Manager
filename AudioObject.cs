using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void Initialize(AudioResource audioResource, AudioMixerGroup audioMixerGroup)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.Play();
        Destroy(this.gameObject, _audioSource.clip.length);
    }

    public void Initialize(AudioResource audioResource, AudioMixerGroup audioMixerGroup, Action action = null)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.Play();
        action?.Invoke();
        Destroy(this.gameObject, _audioSource.clip.length);
    }

    public async void InitializeWaitForSeconds(AudioResource audioResource, AudioMixerGroup audioMixerGroup, float time)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        await Awaitable.WaitForSecondsAsync(time);
        _audioSource.Play();
        Destroy(this.gameObject, time + _audioSource.clip.length);
    }

    public async void InitializeWaitForSeconds(AudioResource audioResource, AudioMixerGroup audioMixerGroup, float time, Action action = null)
    {
        _audioSource.resource = audioResource;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        await Awaitable.WaitForSecondsAsync(time);
        _audioSource.Play();
        action?.Invoke();
        Destroy(this.gameObject, time + _audioSource.clip.length);
    }
}
