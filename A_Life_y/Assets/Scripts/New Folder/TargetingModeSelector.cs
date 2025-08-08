using UnityEngine;

public class TargetingModeSelector : MonoBehaviour
{
    public TargetingMode defaultMode = TargetingMode.FirstAlive;

    public TargetingMode GetMode(BattleUnit unit)
    {
        // �÷��̾� ������ ���� ���� �������, AI�� �ڵ� ���� ���� ����
        if (unit.isPlayerTeam)
        {
            return defaultMode;
        }
        else
        {
            // AI ������ �ٲٰ� ������ ���⼭ ����
            return TargetingMode.LowestHP;
        }
    }
}