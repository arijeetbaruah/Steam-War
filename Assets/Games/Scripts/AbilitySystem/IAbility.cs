using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baruah.AbilitySystem
{
    public interface IAbility
    {
        void Initialize(CharacterAbilityManager characterAbilityManager);
        void Start();
        void Update(float deltaTime);
        void End();
        void Release();
    }
}
