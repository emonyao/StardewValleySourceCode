using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionAttribute))]
public class ConditionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionAttribute condition = (ConditionAttribute)attribute;
        SerializedProperty conditionField = property.serializedObject.FindProperty(condition.ConditionField);

        if (conditionField != null && conditionField.enumValueIndex == (int)condition.ConditionValue)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionAttribute condition = (ConditionAttribute)attribute;
        SerializedProperty conditionField = property.serializedObject.FindProperty(condition.ConditionField);

        if (conditionField != null && conditionField.enumValueIndex == (int)condition.ConditionValue)
        {
            return EditorGUI.GetPropertyHeight(property);
        }

        return 0; // 隐藏字段时高度为 0
    }
}