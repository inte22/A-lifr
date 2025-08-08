using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private bool gameEnded = false; // 🔹 이 필드를 추가
    public List<BattleUnit> allUnits;
    private bool isTargetingMode = false;
    private BattleUnit targetingAttacker;
    private void Start()
    {
        InitializeBattle();
    }

    public void InitializeBattle()
    {
        foreach (var unit in allUnits)
        {
            if (unit != null)
            {
                unit.data.turnCounter = unit.data.speed;
                unit.data.currentState = UnitTurnState.Charging;
            }
        }
    }

    public void ProcessTick()
    {
        if (IsGameOver()) return;

        foreach (var unit in allUnits)
        {
            if (unit.IsAlive() && unit.data.currentState == UnitTurnState.Charging)
            {
                unit.data.turnCounter--;
                if (unit.data.turnCounter <= 0)
                {
                    unit.data.currentState = UnitTurnState.ReadyToAction;
                }
            }
        }

        var readyUnits = allUnits
            .Where(u => u.IsAlive() && u.data.currentState == UnitTurnState.ReadyToAction)
            .ToList();

        foreach (var readyUnit in readyUnits)
        {
            StartUnitTurn(readyUnit);
        }
    }

    public void EnterTargetingMode(BattleUnit attacker)
    {
        isTargetingMode = true;
        targetingAttacker = attacker;
    }

    public bool IsTargetingModeActive()
    {
        return isTargetingMode;
    }

    public void TargetSelected(BattleUnit target)
    {
        if (isTargetingMode && targetingAttacker != null)
        {
            targetingAttacker.ConfirmAttack(target);
            isTargetingMode = false;
            targetingAttacker = null;
        }
    }

    private void StartUnitTurn(BattleUnit unit)
    {
        unit.data.currentState = UnitTurnState.WaitingForInput;

        if (unit.isPlayerTeam)
        {
            unit.BeginPlayerTurn(this);
        }
        else
        {
            unit.ExecuteAITurn(this);
        }
    }

    public void OnUnitTurnFinished(BattleUnit unit)
    {
        if (unit == null || !unit.IsAlive()) return;

        unit.data.turnCounter = unit.data.speed;
        unit.data.currentState = UnitTurnState.Charging;

        // 사망 유닛 UI 정리
        Object.FindFirstObjectByType<DeathHandler>()?.ProcessDeaths(allUnits);
    }

    private bool IsGameOver()
    {
        if (gameEnded) return true;

        bool playerAlive = allUnits.Any(u => u.isPlayerTeam && u.IsAlive());
        bool enemyAlive = allUnits.Any(u => !u.isPlayerTeam && u.IsAlive());

        if (!playerAlive || !enemyAlive)
        {
            Object.FindFirstObjectByType<BattleEndHandler>()?.CheckBattleEnd(allUnits);
            return true;
        }

        return false;
    }
    public BattleUnit FindTargetFor(BattleUnit attacker)
    {
        var selector = Object.FindFirstObjectByType<TargetingModeSelector>();
        TargetingMode mode = selector != null ? selector.GetMode(attacker) : TargetingMode.FirstAlive;
        return TargetingSystem.ChooseTarget(attacker, allUnits, mode);
    }
}

//// TurnManager.cs
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class TurnManager : MonoBehaviour
//{
//    public List<BattleUnit> allUnits;
//    public PlayerInputHandler playerInputHandler; // 플레이어 입력 담당자 참조

//    // 전투 시작 시 BattleSystem이 호출
//    public void InitializeBattle()
//    {
//        foreach (var unit in allUnits)
//        {
//            if (unit != null)
//            {
//                unit.data.turnCounter = unit.data.speed;
//                unit.data.currentState = UnitTurnState.Charging;
//            }
//        }
//        // 플레이어 입력 담당자에게 자신을 알려주며 초기 설정
//        playerInputHandler.Setup(this);
//    }

//    // 매초 BattleSystem이 호출하는 핵심 로직
//    public void ProcessTick()
//    {
//        // 1. '충전 중'인 모든 유닛의 턴 게이지를 채웁니다.
//        foreach (var unit in allUnits)
//        {
//            if (unit.IsAlive() && unit.data.currentState == UnitTurnState.Charging)
//            {
//                unit.data.turnCounter--;
//                if (unit.data.turnCounter <= 0)
//                {
//                    unit.data.currentState = UnitTurnState.ReadyToAction;
//                }
//            }
//        }

//        // 2. 행동할 준비가 된 유닛이 있는지 확인합니다.
//        // FirstOrDefault: 여러 유닛이 동시에 준비되면 리스트 순서상 앞서는 유닛부터 처리
//        var readyUnit = allUnits.FirstOrDefault(u => u.IsAlive() && u.data.currentState == UnitTurnState.ReadyToAction);

//        if (readyUnit != null)
//        {
//            ProcessReadyUnit(readyUnit);
//        }
//    }

//    // 3. 준비된 유닛의 행동을 각 담당자에게 위임합니다.
//    private void ProcessReadyUnit(BattleUnit unit)
//    {
//        // 유닛의 상태를 변경하여 중복 처리를 방지
//        unit.data.currentState = UnitTurnState.WaitingForInput; // 플레이어, AI 모두 일단 이 상태로 변경

//        if (unit.isPlayerTeam)
//        {
//            // 플레이어라면: PlayerInputHandler에게 입력 처리를 요청
//            playerInputHandler.RequestPlayerInput(unit);
//        }
//        else
//        {
//            // 적이라면: BattleUnit 스스로 AI 행동을 실행하도록 지시
//            unit.ExecuteAITurn(this);
//        }
//    }

//    // 4. 유닛의 턴이 완전히 끝났을 때 호출됩니다 (AI 또는 플레이어 핸들러가 호출).
//    public void OnUnitTurnFinished(BattleUnit unit)
//    {
//        if (unit == null || !unit.IsAlive()) return;

//        Debug.Log($"{unit.data.characterName}의 턴 종료. 다시 충전을 시작합니다.");
//        unit.data.turnCounter = unit.data.speed;
//        unit.data.currentState = UnitTurnState.Charging;
//    }

//    // 5. 공격 대상 탐색 기능을 중앙에서 제공합니다.
//    public BattleUnit FindTargetFor(BattleUnit attacker)
//    {
//        // 반대편 팀에서 살아있는 유닛을 찾아 반환
//        return allUnits.FirstOrDefault(u => u.isPlayerTeam != attacker.isPlayerTeam && u.IsAlive());
//    }
//}
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//// 유닛의 턴 상태를 나타내는 enum
//public enum UnitTurnState
//{
//    Charging,       // 턴 카운터 충전 중
//    Ready,          // 행동 준비 완료
//    WaitingForInput // 플레이어 입력 대기 중
//}

//public class TurnManager : MonoBehaviour
//{
//    public List<BattleUnit> turnQueue;
//    public GameObject actionPanel; // 플레이어 행동 UI (버튼이 있는 패널)
//    private BattleUnit playerUnitWaitingForInput; // 현재 입력을 기다리는 플레이어 유닛

//    public void InitializeQueue()
//    {
//        foreach (var unit in turnQueue)
//        {
//            unit.data.turnCounter = unit.data.speed;
//            unit.data.currentState = UnitTurnState.Charging;
//        }
//        actionPanel.SetActive(false);
//    }

//    // BattleSystem이 매초 호출하는 메인 로직
//    public void UpdateTurnLogic()
//    {
//        // '충전 중'인 유닛들의 카운터 감소
//        foreach (var unit in turnQueue)
//        {
//            if (unit.IsAlive() && unit.data.currentState == UnitTurnState.Charging)
//            {
//                unit.data.turnCounter--;
//                if (unit.data.turnCounter <= 0)
//                {
//                    unit.data.currentState = UnitTurnState.Ready;
//                }
//            }
//        }

//        // '준비 완료' 상태인 유닛 처리
//        var readyUnit = turnQueue.FirstOrDefault(u => u.IsAlive() && u.data.currentState == UnitTurnState.Ready);
//        if (readyUnit != null)
//        {
//            ProcessReadyUnit(readyUnit);
//        }
//    }

//    private void ProcessReadyUnit(BattleUnit unit)
//    {
//        if (unit.isPlayerTeam)
//        {
//            // 플레이어 턴: '입력 대기' 상태로 변경하고 UI 켜기
//            unit.data.currentState = UnitTurnState.WaitingForInput;
//            playerUnitWaitingForInput = unit;
//            actionPanel.SetActive(true);
//        }
//        else
//        {
//            // 적 턴: 즉시 자동 행동 실행
//            ExecuteEnemyTurn(unit);
//        }
//    }

//    private void ExecuteEnemyTurn(BattleUnit enemy)
//    {
//        var target = turnQueue.FirstOrDefault(u => u.isPlayerTeam && u.IsAlive());
//        if (target != null)
//        {
//            enemy.Act(target);
//        }

//        // 행동 후 다시 '충전 중' 상태로
//        enemy.data.turnCounter = enemy.data.speed;
//        enemy.data.currentState = UnitTurnState.Charging;
//    }
//    public void OnPlayerAttackButtonClick()    {
//        // 입력 대기 중인 플레이어가 없으면 아무것도 안 함
//        if (playerUnitWaitingForInput == null) return;

//        actionPanel.SetActive(false); // UI 끄기

//        var target = turnQueue.FirstOrDefault(u => !u.isPlayerTeam && u.IsAlive());
//        if (target != null)
//        {
//            playerUnitWaitingForInput.Act(target);
//        }

//        // 행동 후 다시 '충전 중' 상태로
//        playerUnitWaitingForInput.data.turnCounter = playerUnitWaitingForInput.data.speed;
//        playerUnitWaitingForInput.data.currentState = UnitTurnState.Charging;
//        playerUnitWaitingForInput = null; // 대기 유닛 정보 초기화
//    }
//}