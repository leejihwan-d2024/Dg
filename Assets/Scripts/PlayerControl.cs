using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public FieldManager fieldManager;
    public MonsterCard testCard; // 인스펙터에서 카드를 할당하세요.

    void Update()
    {
        // 마우스 왼쪽 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 타일 하나하나 검사해서 클릭된 타일 찾기
            // (실제 게임에선 레이캐스트를 쓰지만, 테스트용으로 거리 계산을 사용합니다)
            for (int x = 0; x < fieldManager.width; x++)
            {
                for (int y = 0; y < fieldManager.height; y++)
                {
                    Vector2Int gridPos = new Vector2Int(x, y);
                    Vector3 tileWorldPos = fieldManager.GetWorldPosition(gridPos);

                    // 타일 중심과 마우스 클릭 위치가 가까우면 클릭한 것으로 간주
                    if (Vector2.Distance(mousePos, tileWorldPos) < 0.5f)
                    {
                        Debug.Log($"{gridPos} 타일 클릭! 스킬 발동");

                        // ★ 여기가 핵심: 클릭한 좌표와 카드의 범위를 전달합니다.
                        fieldManager.CastSkill(gridPos, testCard.attackArea);
                    }
                }
            }
        }
    }
}