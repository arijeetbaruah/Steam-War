using Baruah.Configs;
using Baruah.Service;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Baruah.Characters
{
    public class CharacterStatsHolder : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;

        public CharacterStats CharacterStats => characterStats;

        [ValueDropdown(nameof(GetCharacterIDs))]
        public string characterID;

        public int currentHealth { get; private set; }
        public int currentShield { get; private set; }

        public Action<int, int> OnHealthUpdate;
        public Action<int, int> OnShieldUpdate;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => ServiceManager.isReady);

            CharacterDatabase characterDatabase = ServiceManager.Get<ConfigService>().Get<CharacterDatabase>();

            if (characterDatabase.database.TryGetValue(characterID, out var characterDatabaseData))
            {
                characterStats = characterDatabaseData.characterStats;

                currentHealth = CharacterStats.maxHealth;
                currentShield = CharacterStats.maxShield;
            }    
        }

        private IEnumerable GetCharacterIDs()
        {
            return CharacterDatabase.GetDatabase().database.Select(cd => new ValueDropdownItem(cd.Value.characterName, cd.Key));
        }

        public void TakeDamage(int damage)
        {
            if (currentShield > 0)
            {
                currentShield = Mathf.Min(currentShield - damage, 0);
                OnShieldUpdate?.Invoke(currentShield, damage);

                damage = Mathf.Abs(currentShield - damage);
            }

            currentHealth = Mathf.Max(currentHealth - damage, 0);
            OnHealthUpdate?.Invoke(currentHealth, damage);
        }

        public void RegerateShield(int damage)
        {
            currentShield = Mathf.Min(currentShield + damage, CharacterStats.maxShield);
        }

        public void Heal(int damage)
        {
            currentHealth = Mathf.Min(currentHealth + damage, CharacterStats.maxHealth);
        }
    }
}
