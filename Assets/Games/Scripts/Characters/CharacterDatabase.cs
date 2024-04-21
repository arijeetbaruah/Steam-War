using Baruah.Configs;
using System.Linq;
using UnityEngine;

namespace Baruah.Characters
{
    public class CharacterDatabase : ScriptableObject, IConfig
    {
        public string ID => typeof(CharacterDatabase).FullName;

        public CharacterDatabaseDataRegistry database;

        public static CharacterDatabase GetDatabase()
        {
#if UNITY_EDITOR
            var guid = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(CharacterDatabase)}").FirstOrDefault();
            return UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterDatabase>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid));
#else
            return null;
#endif
        }
    }

    [System.Serializable]
    public class CharacterDatabaseData
    {
        public string characterName;
        public CharacterStats characterStats;
    }

    [System.Serializable]
    public class CharacterDatabaseDataRegistry : UnitySerializedDictionary<string, CharacterDatabaseData>
    {
    }
}
