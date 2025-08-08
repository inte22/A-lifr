using System.Collections.Generic;
using UnityEngine;

public class TargetSelectionUI : MonoBehaviour
{
    public GameObject panel;
    private BattleUnit currentUnit;
    private List<BattleUnit> validTargets;
    private System.Action<BattleUnit> onTargetSelected;

    public void Show(BattleUnit actingUnit, List<BattleUnit> allUnits, System.Action<BattleUnit> callback)
    {
        currentUnit = actingUnit;
        validTargets = allUnits.FindAll(u => u.isPlayerTeam != currentUnit.isPlayerTeam && u.IsAlive());
        onTargetSelected = callback;

        // UI 패널 표시
        panel.SetActive(true);

        // 실제 UI 요소 생성은 외부에서 연결 (예: 버튼 prefab 동적 생성)
        // 여기서는 논리만 구성
    }

    public void SelectTarget(BattleUnit selected)
    {
        if (!validTargets.Contains(selected)) return;

        panel.SetActive(false);
        onTargetSelected?.Invoke(selected);
    }
}
