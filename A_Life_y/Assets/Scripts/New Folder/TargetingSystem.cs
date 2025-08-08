using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem
{
    public static BattleUnit ChooseTarget(BattleUnit requester, List<BattleUnit> candidates, TargetingMode mode)
    {
        List<BattleUnit> filtered = candidates.FindAll(u =>
            u.isPlayerTeam != requester.isPlayerTeam && u.IsAlive());

        if (filtered.Count == 0) return null;

        switch (mode)
        {
            case TargetingMode.FirstAlive:
                return filtered[0];

            case TargetingMode.LowestHP:
                return filtered.Find(u => u.data.currentHP == MinHP(filtered));

            default:
                return filtered[0];
        }
    }

    private static int MinHP(List<BattleUnit> units)
    {
        int min = int.MaxValue;
        foreach (var u in units)
        {
            if (u.data.currentHP < min) min = u.data.currentHP;
        }
        return min;
    }
}

