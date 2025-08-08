using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPredictionDisplay : MonoBehaviour
{
    public TurnManager turnManager;
    public GameObject slotPrefab;  // 텍스트나 아이콘용 프리팹
    public Transform container;    // UI 부모 객체
    public int predictCount = 6;

    private List<GameObject> slots = new List<GameObject>();

    void Update()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        ClearSlots();

        var predicted = TurnPredictionSystem.Predict(turnManager.allUnits, predictCount);

        foreach (var unit in predicted)
        {
            GameObject slot = Instantiate(slotPrefab, container);
            Text label = slot.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = unit.data.characterName;
            }
            slots.Add(slot);
        }
    }

    void ClearSlots()
    {
        foreach (var slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();
    }
}
