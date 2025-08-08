using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEndHandler : MonoBehaviour
{
    public UnityEvent onWin;
    public UnityEvent onLose;

    public void CheckBattleEnd(List<BattleUnit> units)
    {
        bool playerAlive = units.Exists(u => u.isPlayerTeam && u.IsAlive());
        bool enemyAlive = units.Exists(u => !u.isPlayerTeam && u.IsAlive());

        if (!enemyAlive && playerAlive)
        {
            Debug.Log("승리!"); // <- 이게 안 찍히고 있다면 문제
            onWin.Invoke();
        }
        else if (!playerAlive && enemyAlive)
        {
            Debug.Log("패배!");
            onLose.Invoke();
        }
        // 둘 다 살아있다면 아무 일 없음
    }
}
