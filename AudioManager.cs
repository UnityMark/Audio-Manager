using System;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioObject _prefab;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField, Range(0f,1f)] private float _musicFadeDuration = 1f;

    [SerializeField] private DefaultSound _defaultSound;
    [SerializeField] private AudioSettings[] _settings;

    private void Awake()
    {
        if(Instance == null)
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

    public void PlayMusic(AudioResource resource, float volume = 1f)
    {
        if(resource == null)
        {
            Debug.LogWarning($"{typeof(AudioManager)}: Music resource is null");
            return;
        }

        _musicSource.DOKill();

        _musicSource.resource = resource;
        _musicSource.outputAudioMixerGroup = GetAudioMixerGroup(TypeVolume.MFX);
        _musicSource.loop = true;
        _musicSource.volume = 0f;
        _musicSource.Play();

        _musicSource.DOFade(volume, _musicFadeDuration).SetUpdate(true);
    }

    public void StopMusic()
    {
        _musicSource.DOKill();

        _musicSource
            .DOFade(0f, _musicFadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _musicSource.Stop();
                _musicSource.resource = null;
            });
    }

    public void ChangeMusic(AudioResource resource, float volume = 1f)
    {
        if (resource == null)
        {
            Debug.LogWarning($"{typeof(AudioManager)}: Music resource is null");
            return;
        }

        _musicSource.DOKill();

        _musicSource
            .DOFade(0f, _musicFadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _musicSource.resource = resource;
                _musicSource.outputAudioMixerGroup = GetAudioMixerGroup(TypeVolume.MFX);
                _musicSource.loop = true;
                _musicSource.volume = 0f;
                _musicSource.Play();

                _musicSource
                    .DOFade(volume, _musicFadeDuration)
                    .SetUpdate(true);
            });
    }

    public void Play(AudioResource resource, TypeVolume typeVolume, float time = -1f, Action action = null)
    {
        AudioObject audio = Instantiate(_prefab, this.transform);

        if (time > 0f && action != null) 
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time, action);
            return;
        }
        else if(time > 0f && action == null)
        {
            audio.InitializeWaitForSeconds(resource, GetAudioMixerGroup(typeVolume), time);
            return;
        }
        else if(time < 0f && action != null)
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

        if(_defaultSound == null)
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
        foreach(var audio in _settings)
        {
            if(audio.TypeVolume == typeVolume)
            {
                return audio.AudioMixerGroup;
            }
        }

        return null;
    }
}
