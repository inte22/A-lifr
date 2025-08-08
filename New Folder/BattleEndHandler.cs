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
            Debug.Log("�¸�!"); // <- �̰� �� ������ �ִٸ� ����
            onWin.Invoke();
        }
        else if (!playerAlive && enemyAlive)
        {
            Debug.Log("�й�!");
            onLose.Invoke();
        }
        // �� �� ����ִٸ� �ƹ� �� ����
    }
}
