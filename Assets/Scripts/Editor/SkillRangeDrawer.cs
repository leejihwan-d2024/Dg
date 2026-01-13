//Assets/Scripts/Editor/SkillRangeDrawer.cs
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SkillRange))]
public class SkillRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // PropertyDrawer 시작
        EditorGUI.BeginProperty(position, label, property);

        // 1. 프로퍼티 찾기
        SerializedProperty dimProp = property.FindPropertyRelative("dimensions");
        SerializedProperty gridProp = property.FindPropertyRelative("grid");

        // 2. 제목(Label) 그리기
        Rect labelRect = new Rect(position.x, position.y, position.width, 18f);
        EditorGUI.LabelField(labelRect, label, EditorStyles.boldLabel);

        // 3. Dimensions (가로, 세로) 입력 필드
        Rect dimRect = new Rect(position.x, position.y + 20f, position.width, 18f);
        dimProp.vector2IntValue = EditorGUI.Vector2IntField(dimRect, "Grid Size (X:Width, Y:Height)", dimProp.vector2IntValue);

        int width = Mathf.Max(1, dimProp.vector2IntValue.x);
        int height = Mathf.Max(1, dimProp.vector2IntValue.y);

        // 4. 데이터 리스트 크기 동기화
        if (gridProp.arraySize != width * height)
        {
            gridProp.arraySize = width * height;
        }

        // 5. 격자(Checkboxes) 그리기
        float cellSize = 25f;
        float startY = position.y + 45f;

        int centerX = width / 2;
        int centerY = height / 2;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                // 격자 위치 계산
                Rect cellRect = new Rect(position.x + (x * cellSize), startY + (y * cellSize), cellSize - 2, cellSize - 2);

                // 기준점(커서가 위치할 중앙)은 노란색으로 강조
                if (x == centerX && y == centerY) GUI.color = Color.yellow;

                // 체크박스 그리기
                if (index < gridProp.arraySize)
                {
                    SerializedProperty element = gridProp.GetArrayElementAtIndex(index);
                    element.boolValue = EditorGUI.Toggle(cellRect, element.boolValue);
                }

                GUI.color = Color.white;
            }
        }

        EditorGUI.EndProperty();
    }

    // 인스펙터에서 이 프로퍼티가 차지할 전체 높이 계산
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty dimProp = property.FindPropertyRelative("dimensions");
        int height = Mathf.Max(1, dimProp.vector2IntValue.y);
        // 레이블(20) + 입력필드(25) + (격자 높이) + 여백
        return 50f + (height * 25f) + 10f;
    }
}