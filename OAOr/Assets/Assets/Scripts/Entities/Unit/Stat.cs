using System;

public class Stat
{
    public readonly StatType Type;
    public float DefalutValue;
    public float ModificationValue;

    private float _plusValue = 0;
    private float _multiplyValue = 1;

    public Stat(StatType type, float defalutValue, float value)
    {
        Type = type;
        DefalutValue = defalutValue;
        ModificationValue = value;
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
    }
}
