using System;

public class Stat
{
    public readonly StatType Type;
    public float DefalutValue;
    public float ModificationValue;

    private float _plusValue = 0;
    private float _multiplyValue = 1;

    public Stat(StatType type)
    {
        Type = type;
    }

    public Stat(StatType type, float defalutValue)
    {
        Type = type;
        DefalutValue = defalutValue;
        Refresh();
    }

    public void ChangeDefaultValue(float value)
    {
        DefalutValue = value;
        Refresh();
    }

    public void AddModificationValue(BuffData data)
    {
        if (data.ModificationType == ModificationType.Plus)
            _plusValue += data.Value;
        else
            _multiplyValue *= data.Value;

        Refresh();
    }

    public void RemoveModificationValue(BuffData data)
    {
        if (data.ModificationType == ModificationType.Plus)
            _plusValue -= data.Value;
        else
            _multiplyValue /= data.Value;

        Refresh();
    }

    private void Refresh()
    {
        ModificationValue = MathF.Round((DefalutValue + _plusValue) * _multiplyValue, 1);
        //UnityEngine.Debug.Log($"최종값 : {ModificationValue} 기본값 : {DefalutValue} 합연산 : {_plusValue} 곱연산 : {_multiplyValue}");
    }
}
