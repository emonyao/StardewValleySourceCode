using UnityEngine;

public class ConditionAttribute : PropertyAttribute
{
    public string ConditionField;
    public object ConditionValue;

    public ConditionAttribute(string conditionField, object conditionValue)
    {
        ConditionField = conditionField;
        ConditionValue = conditionValue;
    }
}