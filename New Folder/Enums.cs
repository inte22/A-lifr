using UnityEngine;

public enum UnitTurnState
{
    Charging,       // �� ������ ���� ��
    ReadyToAction,  // �ൿ�� �غ� �Ϸ� (�ý����� ó���� ����)
    WaitingForInput // �÷��̾��� �Է��� ��ٸ��� ��
}
public enum TargetingMode
{
    FirstAlive,
    LowestHP
}