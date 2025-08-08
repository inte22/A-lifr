using UnityEngine;
using UnityEngine.UI; // UI.Button�� ����ϱ� ���� �߰�

public class BattleUnit : MonoBehaviour
{
    public CharacterData data;
    public bool isPlayerTeam;

    // �� ������ �ڽ��� �ൿ ��ư�� ���� �����մϴ�.
    public Button actionButton;

    private TurnManager turnManager;

    // [�ű�] ������ Ŭ�� �����ϵ��� Collider�� �ʿ��մϴ�.
    // �� �Լ��� ���ֿ� Collider(��: BoxCollider2D)�� �ְ�, ���콺�� Ŭ���� �� ȣ��˴ϴ�.
    void OnMouseDown()
    {
        // TurnManager�� Ÿ���� ����� ��, �׸��� �� ������ ���� ���� �����մϴ�.
        if (turnManager != null && turnManager.IsTargetingModeActive() && !isPlayerTeam)
        {
            turnManager.TargetSelected(this);
        }
    }

    void Awake()
    {
        // ������ �� �ڽ��� ��ư�� ���ܵӴϴ�.
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
        }
    }

    // �÷��̾� ������ ���� ���۵� �� TurnManager�� ȣ��
    public void BeginPlayerTurn(TurnManager tm)
    {
        Debug.Log($"{data.characterName}�� �� ����! ��ư�� Ȱ��ȭ�˴ϴ�.");
        this.turnManager = tm;
        data.currentState = UnitTurnState.WaitingForInput;

        // �ڽ��� �ൿ ��ư�� Ȱ��ȭ�ϰ�, Ŭ�� �̺�Ʈ�� �����մϴ�.
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(true);
            actionButton.onClick.RemoveAllListeners(); // ���� �����ʸ� �����ϰ� ����
            actionButton.onClick.AddListener(OnMyAttackButtonClick);
        }
    }

    // [����] ���� ��ư�� ������, ���� ������ �ٷ� �����ϴ� ��� Ÿ���� ���� �����մϴ�.
    public void OnMyAttackButtonClick()
    {
        if (data.currentState != UnitTurnState.WaitingForInput) return;

        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
        }

        // TurnManager���� Ÿ���� ��� ������ ��û�մϴ�.
        turnManager.EnterTargetingMode(this);
    }

    // [�ű�] TurnManager�� Ÿ���� Ȯ���Ǿ��� �� ȣ���� �� �Լ�
    public void ConfirmAttack(BattleUnit target)
    {
        Debug.Log($"{data.characterName}��(��) {target.data.characterName} (��)�� ������ Ȯ���մϴ�.");
        PerformAttack(target);

        // ���� �� ���� �����մϴ�.
        turnManager.OnUnitTurnFinished(this);
    }

    // �� AI�� �� ó�� (������ ����)
    public void ExecuteAITurn(TurnManager tm)
    {
        Debug.Log($"{data.characterName}�� AI �� ����.");
        this.turnManager = tm;
        BattleUnit target = turnManager.FindTargetFor(this);
        if (target != null)
        {
            PerformAttack(target);
        }
        turnManager.OnUnitTurnFinished(this);
    }

    public Skill equippedSkill; // �ν����Ϳ��� ����

    public void PerformAttack(BattleUnit target)
    {
        if (equippedSkill != null)
        {
            // ������ ��ų�� ������ ��ų ���
            equippedSkill.Apply(this, target);
        }
        else
        {
            // ������ �⺻ ����
            int damage = 20;
            Debug.Log($"{data.characterName}��(��) {target.data.characterName}��(��) ����!");
            target.TakeDamage(damage);
        }
    }
    // �������� �޴� ��� (������ ����)
    public void TakeDamage(int damage)
    {
        data.currentHP -= damage;
        if (data.currentHP < 0) data.currentHP = 0;
        Debug.Log($"{data.characterName}�� {damage}�� ���ظ� �Ծ����ϴ�. (���� HP: {data.currentHP})");
    }

    // ����ִ��� Ȯ�� (������ ����)
    public bool IsAlive()
    {
        return !data.IsDead;
    }

    public void Act(BattleUnit target)
    {
        if (equippedSkill != null && target != null)
        {
            equippedSkill.Apply(this, target);
        }
    }
}
