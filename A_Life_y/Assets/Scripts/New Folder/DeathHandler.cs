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
        Debug.Log($"{unit.data.characterName} 사망 처리됨");

        // 필요 시 이 위치에서 death 이벤트 트리거 가능 (UI, 애니메이션 등은 외부에서 구독)
    }
}
