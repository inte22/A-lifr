using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public void ProcessDeaths(List<BattleUnit> units)
    {
        for (int i = units.Count - 1; i >= 0; i--)
        {
            BattleUnit unit = units[i];
            if (unit.data.IsDead && unit.gameObject.activeSelf)
            {
                HandleUnitDeath(unit);
            }
        }
    }

    private void HandleUnitDeath(BattleUnit unit)
    {
        unit.gameObject.SetActive(false);
        Debug.Log($"{unit.data.characterName} ��� ó����");

        // �ʿ� �� �� ��ġ���� death �̺�Ʈ Ʈ���� ���� (UI, �ִϸ��̼� ���� �ܺο��� ����)
    }
}
