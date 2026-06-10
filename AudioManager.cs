using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioObject _prefab;

    [SerializeField] private DefaultSound _defaultSound;
    [SerializeField] private AudioSettings[] _settings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log($"{typeof(AudioManager)}: initialized, GameObject name is {Instance.gameObject.name}");
        }
        else
        {
            Debug.LogWarning($"{typeof(AudioManager)}: GameObject name is {Instance.gameObject.name} destroyed.");
            Destroy(gameObject);
        }
    }

    public void Play(AudioResource resource, TypeVolume typeVolume, float time = -1f, Action action = null)
    {
        AudioObject audio = Instantiate(_prefab, this.transform);

        if (time > 0f && action != null)
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time, action);
            return;
        }
        else if (time > 0f && action == null)
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time);
            return;
        }
        else if (time < 0f && action != null)
        {
            audio.Initialize(resource, GetAudioMixerGroup(typeVolume), action);
            return;
        }
        else if (time < 0f && action == null)
        {
            audio.Initialize(resource, GetAudioMixerGroup(typeVolume));
            return;
        }

        Debug.LogWarning("AudioObject: Is not spawn!");
    }

    public void Play(string idResource, TypeVolume typeVolume, float time = -1f, Action action = null)
    {
        AudioObject audio = Instantiate(_prefab, this.transform);

        if (_defaultSound == null)
        {
            Debug.LogWarning($"{typeof(AudioManager)}: DefaultSound is null");
            Destroy(audio.gameObject);
            return;
        }

        AudioResource resource = _defaultSound.GetSound(idResource);

        if (resource is null)
        {
            Destroy(audio.gameObject);
            return;
        }

        if (time > 0f && action != null)
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time, action);
            return;
        }
        else if (time > 0f && action == null)
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time);
            return;
        }
        else if (time < 0f && action != null)
        {
            audio.Initialize(resource, GetAudioMixerGroup(typeVolume), action);
            return;
        }
        else if (time < 0f && action == null)
        {
            audio.Initialize(resource, GetAudioMixerGroup(typeVolume));
            return;
        }

        Debug.LogWarning("AudioObject: Is not spawn!");
    }

    private AudioMixerGroup GetAudioMixerGroup(TypeVolume typeVolume)
    {
        foreach (var audio in _settings)
        {
            if (audio.TypeVolume == typeVolume)
            {
                return audio.AudioMixerGroup;
            }
        }

        return null;
    }
}
