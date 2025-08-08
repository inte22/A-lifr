using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Battle/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int damage;
    public string element; // 예: "fire", "ice"

    public void Apply(BattleUnit caster, BattleUnit target)
    {
        target.TakeDamage(damage);
        Debug.Log($"{caster.data.characterName} 사용: {name} → {target.data.characterName} 피해 {damage}");
    }
}