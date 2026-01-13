using UnityEngine;
using System.Collections.Generic;

[System.Serializable] // ★ 반드시 있어야 인스펙터에 나타납니다!


public class MonsterCard : MonoBehaviour
{
    public string cardName;
    public SkillRange attackArea; // 이제 인스펙터에서 보일 것입니다.

    public void PerformAttack(Vector2Int targetPos)
    {
        var offsets = attackArea.GetOffsets();
        foreach (var offset in offsets)
        {
            Vector2Int finalPos = targetPos + offset;
            Debug.Log($"공격 범위 포함 좌표: {finalPos}");
        }
    }
}