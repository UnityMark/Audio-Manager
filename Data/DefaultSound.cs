using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Default Sounds")]
public class DefaultSound : SerializedScriptableObject
{
    [SerializeField] private Dictionary<string, AudioResource> soundMap;
    public Dictionary<string, AudioResource> SoundMap => soundMap;

    public AudioResource GetSound(string id)
    {
        if (soundMap.TryGetValue(id, out AudioResource resource))
            return resource;

        Debug.LogWarning($"Sound with ID '{id}' not found.");
        return null;
    }
}
