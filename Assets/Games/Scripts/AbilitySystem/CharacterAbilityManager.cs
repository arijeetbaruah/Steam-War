using Baruah.Characters;
using Baruah.Configs;
using Baruah.Service;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Baruah.AbilitySystem
{
    public class CharacterAbilityManager : MonoBehaviour
    {
        protected Dictionary<System.Type, IAbility> grantedAbilities = new Dictionary<System.Type, IAbility>();
        protected Dictionary<System.Type, IAbility> activeAbilities = new Dictionary<System.Type, IAbility>();
        protected Dictionary<System.Type, IAbility> passiveAbilities = new Dictionary<System.Type, IAbility>();

        [SerializeField] protected CharacterStatsHolder characterStatsHolder;

        public CharacterStatsHolder CharacterStatsHolder => characterStatsHolder;

        public void GrantAbility<TAbility>() where TAbility : class, IAbility, new()
        {
            AbilityDatabase abilityDatabase = ServiceManager.Get<ConfigService>().Get<AbilityDatabase>();
            if (abilityDatabase.database.TryGetValue(typeof(TAbility).FullName, out var abilityData))
            {
                if (abilityData.isPassive)
                {
                    GrantPassiveAbility<TAbility>(abilityData);
                }
                else
                {
                    GrantExecutableAbility<TAbility>(abilityData);
                }
            }
        }

        protected void GrantExecutableAbility<TAbility>(AbiltiyDatabaseElementData data) where TAbility : class, IAbility, new()
        {
            var ability = ServiceManager.Get<AbilityService>().Get<TAbility>(data);
            ability.Initialize(this);
            grantedAbilities.Add(typeof(TAbility), ability);
        }

        protected void GrantPassiveAbility<TAbility>(AbiltiyDatabaseElementData data) where TAbility : class, IAbility, new()
        {
            var ability = ServiceManager.Get<AbilityService>().Get<TAbility>(data);
            ability.Initialize(this);
            passiveAbilities.Add(typeof(TAbility), ability);
            ability.Start();
        }

        public void ExecuteAbilityStart<TAbility>() where TAbility : class, IAbility, new()
        {
            if (!activeAbilities.ContainsKey(typeof(TAbility)) && grantedAbilities.TryGetValue(typeof(TAbility), out var ability))
            {
                activeAbilities.Add(typeof(TAbility), ability);
                ability.Start();
            }
        }

        public void ExecuteAbilityEnd<TAbility>() where TAbility : class, IAbility, new()
        {
            if (activeAbilities.TryGetValue(typeof(TAbility), out var ability))
            {
                ability.End();
                activeAbilities.Remove(typeof(TAbility));
            }
        }

        public void RemoveAbility<TAbility>() where TAbility : class, IAbility, new()
        {
            AbilityDatabase abilityDatabase = ServiceManager.Get<ConfigService>().Get<AbilityDatabase>();
            if (abilityDatabase.database.TryGetValue(typeof(TAbility).FullName, out var abilityData))
            {
                if (abilityData.isPassive)
                {
                    RemovePassiveAbility<TAbility>();
                }
                else
                {
                    RemoveExecutableAbility<TAbility>();
                }
            }
        }

        protected void RemoveExecutableAbility<TAbility>() where TAbility : class, IAbility, new()
        {
            if (grantedAbilities.TryGetValue(typeof(TAbility), out var ability))
            {
                ability.Release();
                grantedAbilities.Remove(typeof(TAbility));
            }
        }

        protected void RemovePassiveAbility<TAbility>() where TAbility : class, IAbility, new()
        {
            if (passiveAbilities.TryGetValue(typeof(TAbility), out var ability))
            {
                ability.End();
                ability.Release();
                passiveAbilities.Remove(typeof(TAbility));
            }
        }

        private void Update()
        {
            var abilities = activeAbilities.Values.Concat(passiveAbilities.Values).ToList();
            foreach (var ability in abilities)            {
                ability.Update(Time.deltaTime);
            }
        }
    }
}
