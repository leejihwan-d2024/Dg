using UnityEngine;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour
{
    [Header("Field Settings")]
    public int width = 2;
    public int height = 3;
    public GameObject tilePrefab;
    public float spacing = 1.2f;

    private Tile[,] allTiles;

    void Start()
    {
        CreateField();
    }

    void CreateField()
    {
        allTiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * spacing, y * spacing, 0);
                GameObject go = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                Tile tile = go.GetComponent<Tile>();
                tile.gridPos = new Vector2Int(x, y);
                allTiles[x, y] = tile;
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * spacing, gridPos.y * spacing, 0);
    }

    // ★ 수정된 함수: 소환 전 기존 몬스터 제거 로직 추가
    public void SpawnMonsterAtRandom(MonsterData data)
    {
        if (data == null) return;

        // 1. 기존에 소환된 모든 몬스터 제거 및 타일 정보 초기화
        foreach (Tile t in allTiles)
        {
            if (t.occupyingMonster != null)
            {
                Destroy(t.occupyingMonster); // 실제 게임 오브젝트 삭제
                t.occupyingMonster = null;   // 타일의 참조 초기화
            }
        }

        // 2. 다시 모든 타일을 빈 타일 리스트에 담기 (이제 모두 비어있음)
        List<Tile> emptyTiles = new List<Tile>();
        foreach (Tile t in allTiles)
        {
            emptyTiles.Add(t);
        }

        // 3. 랜덤한 위치에 하나 소환
        if (emptyTiles.Count > 0)
        {
            Tile targetTile = emptyTiles[Random.Range(0, emptyTiles.Count)];

            // 1. 타일의 위치를 가져옵니다.
            Vector3 spawnPos = targetTile.transform.position;

            // 2. Z값을 -0.5f로 설정 (카메라가 -10에 있다면 -0.5는 0보다 카메라에 더 가깝습니다)
            spawnPos.z = -0.5f;

            GameObject newMonster = Instantiate(data.monsterPrefab, spawnPos, Quaternion.identity);
            newMonster.name = data.monsterName;
            targetTile.occupyingMonster = newMonster;
        }
    }

    public void CastSkill(Vector2Int centerPos, SkillRange skillRange)
    {
        List<Vector2Int> offsets = skillRange.GetOffsets();

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int targetPos = centerPos + offset;

            if (targetPos.x >= 0 && targetPos.x < width &&
                targetPos.y >= 0 && targetPos.y < height)
            {
                allTiles[targetPos.x, targetPos.y].OnAttacked();
            }
        }
    }
    public void SpawnMonsters(MonsterData data, int count)
    {
        if (data == null) return;

        // 1. 기존 몬스터 싹 다 제거 (새로 소환할 때마다 초기화하고 싶다면 유지)
        ClearAllMonsters();

        // 2. 현재 비어있는 모든 타일을 리스트에 수집
        List<Tile> emptyTiles = new List<Tile>();
        foreach (Tile t in allTiles)
        {
            if (t.occupyingMonster == null) emptyTiles.Add(t);
        }

        // 3. 요청받은 마리수(count)만큼 반복하며 소환
        // 단, 남은 빈 타일 수보다 많이 소환할 수는 없음
        int spawnAmount = Mathf.Min(count, emptyTiles.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            // 랜덤 타일 선택
            int randomIndex = Random.Range(0, emptyTiles.Count);
            Tile targetTile = emptyTiles[randomIndex];

            // 몬스터 생성 위치 설정 (Z축을 -0.5f로 당겨서 큐브 위로 띄움)
            Vector3 spawnPos = targetTile.transform.position;
            spawnPos.z = -0.5f;

            GameObject newMonster = Instantiate(data.monsterPrefab, spawnPos, Quaternion.identity);
            newMonster.name = data.monsterName;
            targetTile.occupyingMonster = newMonster;

            // 소환된 타일은 리스트에서 제거 (중복 소환 방지)
            emptyTiles.RemoveAt(randomIndex);
        }

        Debug.Log($"{spawnAmount} 마리의 몬스터를 소환했습니다.");
    }

    // 필드 위의 모든 몬스터를 지우는 보조 함수
    public void ClearAllMonsters()
    {
        foreach (Tile t in allTiles)
        {
            if (t.occupyingMonster != null)
            {
                Destroy(t.occupyingMonster);
                t.occupyingMonster = null;
            }
        }
    }
}