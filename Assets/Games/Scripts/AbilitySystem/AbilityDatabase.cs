using Sirenix.OdinInspector;
using Newtonsoft.Json;
using Baruah.Configs;
using System.Linq;
using UnityEngine;

namespace Baruah.AbilitySystem
{
    public class AbilityDatabase : ScriptableObject, IConfig
    {
        public string ID => typeof(AbilityDatabase).FullName;

        public AbilityDatabaseElement database = new AbilityDatabaseElement();

        [Button]
        private void Refresh()
        {
#if UNITY_EDITOR
            var abilityTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<IAbility>().Where(a => !a.IsAbstract && !a.IsGenericType);
            foreach(var abilityType in abilityTypes)
            {
                if (database.ContainsKey(abilityType.FullName))
                {
                    continue;
                }

                database.Add(abilityType.FullName, new AbiltiyDatabaseElementData
                {
                    type  = JsonConvert.SerializeObject(abilityType)
                });
            }

            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }

    [System.Serializable]
    public struct AbiltiyDatabaseElementData
    {
        [HideInInspector]
        public string type;
        [HideInInspector]
        public string additionalData;
        [ShowInInspector, OnValueChanged(nameof(OnChange), true)]
        public IAbility ability;
        public bool isPassive;

        private void OnChange()
        {
            System.Type dataType = JsonConvert.DeserializeObject<System.Type>(type);
            additionalData = JsonConvert.SerializeObject(ability, dataType, null);
        }

        [OnInspectorInit]
        private void OnInitialize()
        {
            System.Type dataType = JsonConvert.DeserializeObject<System.Type>(type);
            additionalData = string.IsNullOrEmpty(additionalData) ? "{}" : additionalData;
            ability = (IAbility) JsonConvert.DeserializeObject(additionalData, dataType);
        }
    }

    [System.Serializable]
    public class AbilityDatabaseElement : UnitySerializedDictionary<string, AbiltiyDatabaseElementData> { }
}
