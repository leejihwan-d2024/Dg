using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public FieldManager fieldManager;
    public MonsterData monsterToSpawn;

    [Header("소환 설정")]
    public int minSpawnCount = 1; // 최소 소환 마리수
    public int maxSpawnCount = 3; // 최대 소환 마리수

    // UI Button 연결 함수
    public void OnClickSpawnButton()
    {
        if (fieldManager != null && monsterToSpawn != null)
        {
            // min과 max 사이의 랜덤한 숫자를 결정
            int randomCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

            // 결정된 마리수만큼 소환 요청
            fieldManager.SpawnMonsters(monsterToSpawn, randomCount);
        }
    }
}