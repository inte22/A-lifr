using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public TurnManager turnManager;

    private float timer = 0f;
    public float tickInterval = 1f; // 1�ʿ� �� �� 'ƽ' �߻�

    void Start()
    {
        if (turnManager == null)
        {
            Debug.LogError("TurnManager�� BattleSystem�� ������� �ʾҽ��ϴ�!");
            return;
        }
        turnManager.InitializeBattle();
    }

    void Update()
    {
        if (turnManager == null) return;

        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            timer -= tickInterval; // timer = 0f ��� -= tickInterval�� ����ϸ� �ð��� �� ��Ȯ�����ϴ�.

            // "1�� �������ϴ�!" ��� TurnManager���� �˸�
            turnManager.ProcessTick();
        }
    }
}

//// BattleSystem.cs
//using UnityEngine;

//public class BattleSystem : MonoBehaviour
//{
//    public TurnManager turnManager;

//    private float timer = 0f;
//    public float tickInterval = 1f; // 1�ʿ� �� �� 'ƽ' �߻�

//    void Start()
//    {
//        if (turnManager == null)
//        {
//            Debug.LogError("TurnManager�� BattleSystem�� ������� �ʾҽ��ϴ�!");
//            return;
//        }
//        turnManager.InitializeBattle();
//    }

//    void Update()
//    {
//        if (turnManager == null) return;

//        timer += Time.deltaTime;
//        if (timer >= tickInterval)
//        {
//            timer -= tickInterval; // timer = 0f ��� -= tickInterval�� ����ϸ� �ð��� �� ��Ȯ�����ϴ�.

//            // "1�� �������ϴ�!" ��� TurnManager���� �˸�
//            turnManager.ProcessTick();
//        }
//    }
//}