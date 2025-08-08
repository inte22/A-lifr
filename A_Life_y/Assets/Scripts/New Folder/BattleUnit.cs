using UnityEngine;
using UnityEngine.UI; // UI.Button을 사용하기 위해 추가

public class BattleUnit : MonoBehaviour
{
    public CharacterData data;
    public bool isPlayerTeam;

    // 각 유닛이 자신의 행동 버튼을 직접 참조합니다.
    public Button actionButton;

    private TurnManager turnManager;

    // [신규] 유닛이 클릭 가능하도록 Collider가 필요합니다.
    // 이 함수는 유닛에 Collider(예: BoxCollider2D)가 있고, 마우스로 클릭될 때 호출됩니다.
    void OnMouseDown()
    {
        // TurnManager가 타겟팅 모드일 때, 그리고 이 유닛이 적일 때만 반응합니다.
        if (turnManager != null && turnManager.IsTargetingModeActive() && !isPlayerTeam)
        {
            turnManager.TargetSelected(this);
        }
    }

    void Awake()
    {
        // 시작할 때 자신의 버튼은 숨겨둡니다.
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
        }
    }

    // 플레이어 유닛의 턴이 시작될 때 TurnManager가 호출
    public void BeginPlayerTurn(TurnManager tm)
    {
        Debug.Log($"{data.characterName}의 턴 시작! 버튼이 활성화됩니다.");
        this.turnManager = tm;
        data.currentState = UnitTurnState.WaitingForInput;

        // 자신의 행동 버튼을 활성화하고, 클릭 이벤트를 연결합니다.
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(true);
            actionButton.onClick.RemoveAllListeners(); // 이전 리스너를 깨끗하게 제거
            actionButton.onClick.AddListener(OnMyAttackButtonClick);
        }
    }

    // [수정] 공격 버튼을 누르면, 이제 공격을 바로 실행하는 대신 타겟팅 모드로 진입합니다.
    public void OnMyAttackButtonClick()
    {
        if (data.currentState != UnitTurnState.WaitingForInput) return;

        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
        }

        // TurnManager에게 타겟팅 모드 진입을 요청합니다.
        turnManager.EnterTargetingMode(this);
    }

    // [신규] TurnManager가 타겟이 확정되었을 때 호출해 줄 함수
    public void ConfirmAttack(BattleUnit target)
    {
        Debug.Log($"{data.characterName}이(가) {target.data.characterName} (으)로 공격을 확정합니다.");
        PerformAttack(target);

        // 공격 후 턴을 종료합니다.
        turnManager.OnUnitTurnFinished(this);
    }

    // 적 AI의 턴 처리 (기존과 동일)
    public void ExecuteAITurn(TurnManager tm)
    {
        Debug.Log($"{data.characterName}의 AI 턴 시작.");
        this.turnManager = tm;
        BattleUnit target = turnManager.FindTargetFor(this);
        if (target != null)
        {
            PerformAttack(target);
        }
        turnManager.OnUnitTurnFinished(this);
    }

    public Skill equippedSkill; // 인스펙터에서 설정

    public void PerformAttack(BattleUnit target)
    {
        if (equippedSkill != null)
        {
            // 설정된 스킬이 있으면 스킬 사용
            equippedSkill.Apply(this, target);
        }
        else
        {
            // 없으면 기본 공격
            int damage = 20;
            Debug.Log($"{data.characterName}이(가) {target.data.characterName}을(를) 공격!");
            target.TakeDamage(damage);
        }
    }
    // 데미지를 받는 기능 (기존과 동일)
    public void TakeDamage(int damage)
    {
        data.currentHP -= damage;
        if (data.currentHP < 0) data.currentHP = 0;
        Debug.Log($"{data.characterName}가 {damage}의 피해를 입었습니다. (남은 HP: {data.currentHP})");
    }

    // 살아있는지 확인 (기존과 동일)
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
