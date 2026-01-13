using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public GameObject occupyingMonster;

    private Renderer rend;
    private Color originalColor; // 원래 색상을 저장할 변수

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // 시작할 때 머티리얼의 원래 색상을 저장해둡니다.
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void OnAttacked()
    {
        StopAllCoroutines();
        StartCoroutine(FlashEffect());

        if (occupyingMonster != null)
        {
            Debug.Log($"{gridPos} 타일의 몬스터를 처치했습니다!");
            Destroy(occupyingMonster);
            occupyingMonster = null;
        }
    }

    IEnumerator FlashEffect()
    {
        if (rend == null) yield break;

        // 공격받았을 때 빨간색으로 변경
        rend.material.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        // 미리 저장해두었던 원래 색상으로 복구
        rend.material.color = originalColor;
    }
}