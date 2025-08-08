using UnityEngine;

public enum UnitTurnState
{
    Charging,       // 턴 게이지 충전 중
    ReadyToAction,  // 행동할 준비 완료 (시스템이 처리할 차례)
    WaitingForInput // 플레이어의 입력을 기다리는 중
}
public enum TargetingMode
{
    FirstAlive,
    LowestHP
}