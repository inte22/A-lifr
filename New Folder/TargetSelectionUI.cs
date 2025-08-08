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

        // UI �г� ǥ��
        panel.SetActive(true);

        // ���� UI ��� ������ �ܺο��� ���� (��: ��ư prefab ���� ����)
        // ���⼭�� ���� ����
    }

    public void SelectTarget(BattleUnit selected)
    {
        if (!validTargets.Contains(selected)) return;

        panel.SetActive(false);
        onTargetSelected?.Invoke(selected);
    }
}
