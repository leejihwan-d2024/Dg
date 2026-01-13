using UnityEngine;

// 이 파일의 이름은 반드시 MonsterData.cs 여야 합니다.
[CreateAssetMenu(fileName = "NewMonster", menuName = "RPG/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public GameObject monsterPrefab;
    public int dungeonGroup;
}