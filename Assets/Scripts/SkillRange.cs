using UnityEngine;
using System;
using System.Collections.Generic;

// 인스펙터에 노출하기 위해 필수
[Serializable]
public class SkillRange
{
    // x: 가로 칸 수, y: 세로 칸 수
    public Vector2Int dimensions = new Vector2Int(3, 3);

    // 체크박스 상태를 저장할 리스트
    public List<bool> grid = new List<bool>();

    // 인게임에서 이 범위를 참조할 때 사용할 도우미 함수
    public List<Vector2Int> GetOffsets()
    {
        List<Vector2Int> offsets = new List<Vector2Int>();
        int centerX = dimensions.x / 2;
        int centerY = dimensions.y / 2;

        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i])
            {
                int x = i % dimensions.x;
                int y = i / dimensions.x;
                // 기준점(중앙)으로부터의 상대적 좌표 저장
                offsets.Add(new Vector2Int(x - centerX, y - centerY));
            }
        }
        return offsets;
    }
}