using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Baruah.Configs
{
    [CreateAssetMenu]
    public class ConfigRegistry : ScriptableObject
    {
        [FolderPath]
        public string configPath;
        public ConfigDictionary registry = new ConfigDictionary();

        [Button]
        private void Refresh()
        {
#if UNITY_EDITOR
            var configs = UnityEditor.TypeCache.GetTypesDerivedFrom<IConfig>().Where(c => !c.IsAbstract && !c.IsGenericType);
            foreach (var config in configs)
            {
                if (registry.ContainsKey(config.FullName))
                {
                    continue;
                }

                ScriptableObject so = ScriptableObject.CreateInstance(config);
                string filePath = Path.Combine(configPath, $"{config.Name}.asset");
                UnityEditor.AssetDatabase.CreateAsset(so, $"{filePath}");

                registry.Add(config.FullName, so);
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssets();
            }
#endif
        }
    }

    [System.Serializable]
    public class ConfigDictionary : UnitySerializedDictionary<string, ScriptableObject>
    {
    }

    public interface IConfig
    {
        string ID { get; }
    }
}
