[System.Serializable]
public class CharacterData
{
    public string characterName;
    public int maxHP = 100;
    public int currentHP = 100;
    public int speed = 50;

    // 턴 관련 데이터
    public int turnCounter = 0;
    public UnitTurnState currentState;

    public bool IsDead => currentHP <= 0;
}