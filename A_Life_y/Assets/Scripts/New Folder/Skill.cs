using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Battle/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int damage;
    public string element; // ��: "fire", "ice"

    public void Apply(BattleUnit caster, BattleUnit target)
    {
        target.TakeDamage(damage);
        Debug.Log($"{caster.data.characterName} ���: {name} �� {target.data.characterName} ���� {damage}");
    }
}