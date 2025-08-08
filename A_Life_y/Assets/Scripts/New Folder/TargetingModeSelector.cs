using UnityEngine;

public class TargetingModeSelector : MonoBehaviour
{
    public TargetingMode defaultMode = TargetingMode.FirstAlive;

    public TargetingMode GetMode(BattleUnit unit)
    {
        // 플레이어 유닛은 수동 지정 기반으로, AI는 자동 전략 적용 가능
        if (unit.isPlayerTeam)
        {
            return defaultMode;
        }
        else
        {
            // AI 전략을 바꾸고 싶으면 여기서 조절
            return TargetingMode.LowestHP;
        }
    }
}