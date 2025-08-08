using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public TurnManager turnManager;

    private float timer = 0f;
    public float tickInterval = 1f; // 1초에 한 번 '틱' 발생

    void Start()
    {
        if (turnManager == null)
        {
            Debug.LogError("TurnManager가 BattleSystem에 연결되지 않았습니다!");
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
            timer -= tickInterval; // timer = 0f 대신 -= tickInterval을 사용하면 시간이 더 정확해집니다.

            // "1초 지났습니다!" 라고 TurnManager에게 알림
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
//    public float tickInterval = 1f; // 1초에 한 번 '틱' 발생

//    void Start()
//    {
//        if (turnManager == null)
//        {
//            Debug.LogError("TurnManager가 BattleSystem에 연결되지 않았습니다!");
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
//            timer -= tickInterval; // timer = 0f 대신 -= tickInterval을 사용하면 시간이 더 정확해집니다.

//            // "1초 지났습니다!" 라고 TurnManager에게 알림
//            turnManager.ProcessTick();
//        }
//    }
//}