using UnityEngine;

namespace Baruah.Characters
{
    [System.Serializable]
    public struct CharacterStats
    {
        public int maxHealth;
        public int maxShield;
        public float speed;
        public float defense;
        public float critRate;

        public float CalculateDamage(float attack)
        {
            return Mathf.Max(attack - defense, 0);
        }

        public bool IsCrit()
        {
            float value = Random.Range(0f, 100f);
            return value <= critRate;
        }
    }
}
