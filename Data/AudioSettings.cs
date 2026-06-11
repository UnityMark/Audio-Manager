using System;
using UnityEngine.Audio;

public enum TypeVolume
{
    SFX,
    MFX
}

[Serializable]
public class AudioSettings
{
    public AudioMixerGroup AudioMixerGroup;
    public TypeVolume TypeVolume;
}
