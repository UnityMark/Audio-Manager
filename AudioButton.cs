using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [ValueDropdown("GetAudioKeys")]
    [SerializeField] private string Key;

    private void Awake()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(Play);
    }

    private void Play() => AudioManager.Instance.Play(Key, TypeVolume.SFX);

    private void OnValidate()
    {
#if UNITY_EDITOR
        // Если ключ пустой или равен дефолтному, пробуем подставить первый валидный
        if (string.IsNullOrEmpty(Key) || Key == "ClickUI")
        {
            var keys = GetAudioKeys() as IEnumerable;
            if (keys != null)
            {
                // Берем самый первый ключ из доступных в словаре
                string firstKey = keys.Cast<string>().FirstOrDefault();

                if (!string.IsNullOrEmpty(firstKey))
                {
                    Key = firstKey;
                }
            }
        }
#endif
    }


    // Этот метод вызывается Odin-ом исключительно внутри редактора Unity
    private IEnumerable GetAudioKeys()
    {
#if UNITY_EDITOR
        // Ищем GUID ассетов с типом DefaultSound встроенными средствами Unity
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:DefaultSound");

        if (guids != null && guids.Length > 0)
        {
            // Берем путь к первому найденному файлу
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

            // Загружаем сам ассет
            DefaultSound settings = UnityEditor.AssetDatabase.LoadAssetAtPath<DefaultSound>(path);

            if (settings != null && settings.SoundMap != null)
            {
                return settings.SoundMap.Keys;
            }
        }
#endif
        return new string[] { };
    }
}
