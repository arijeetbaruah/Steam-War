using Baruah.AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOvertimeAbility : IAbility
{
    private CharacterAbilityManager characterAbilityManager;

    public float damageOverTick;
    public float duration;

    private float timer;

    public void Initialize(CharacterAbilityManager characterAbilityManager)
    {
        this.characterAbilityManager = characterAbilityManager;
    }

    public void Start()
    {
        timer = duration;
    }

    public void Update(float deltaTime)
    {
        timer -= deltaTime;
        Debug.Log(damageOverTick);
        if (timer <= 0)
        {
            characterAbilityManager.RemoveAbility<DamageOvertimeAbility>();
        }
    }

    public void End()
    {
        
    }

    public void Release()
    {
        characterAbilityManager = null;
    }
}
