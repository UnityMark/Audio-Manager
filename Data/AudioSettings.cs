using UnityEngine;
using UnityEngine.Audio;

public enum TypeVolume
{
    SFX,
    MFX
}

public class AudioSettings : MonoBehaviour
{
    public AudioMixerGroup AudioMixerGroup;
    public TypeVolume TypeVolume;
}
