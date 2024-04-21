using Baruah.AbilitySystem;
using Baruah.Service;
using System.Collections;
using UnityEngine;

public class AbilityTester : MonoBehaviour
{
    private CharacterAbilityManager characterAbilityManager;

    private IEnumerator Start()
    {
        characterAbilityManager = GetComponent<CharacterAbilityManager>();
        
        yield return new WaitUntil(() => ServiceManager.isReady);
        
        Debug.Log("ready");
        characterAbilityManager.GrantAbility<DamageOvertimeAbility>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            characterAbilityManager.ExecuteAbilityStart<DamageOvertimeAbility>();
        }
    }
}
