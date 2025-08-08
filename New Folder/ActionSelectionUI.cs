using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionSelectionUI : MonoBehaviour
{
    public GameObject panel;
    public Button actionButton;
    private BattleUnit currentUnit;
    private Action onActionConfirmed;

    public void Show(BattleUnit unit, Action onConfirmed)
    {
        currentUnit = unit;
        onActionConfirmed = onConfirmed;

        panel.SetActive(true);
        SetupButtons();
    }

    void SetupButtons()
    {
        Text btnText = actionButton.GetComponentInChildren<Text>();
        if (btnText != null)
        {
            btnText.text = currentUnit.equippedSkill != null ? currentUnit.equippedSkill.name : "기본공격";
        }

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            onActionConfirmed?.Invoke();
        });
    }
}

