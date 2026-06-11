using UnityEngine;

public class AudioDebugger : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    [ContextMenu("play")]
    public void Play()
    {
        AudioManager.Instance.Play("sadfas", TypeVolume.SFX);
    }

    [ContextMenu("play time")]
    public void PlayTime()
    {
        AudioManager.Instance.Play(_audioClip, TypeVolume.SFX, 2f);
    }
}
