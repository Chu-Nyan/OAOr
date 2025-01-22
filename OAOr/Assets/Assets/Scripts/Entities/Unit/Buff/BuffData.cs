public class BuffData
{
    public BuffType BuffType;
    public StatType StatType;
    public ProcessType ProcessType;
    public ModificationType ModificationType;
    public float Value;
    public float DurationTime;
    public bool IsReturnValue;

    public float NextRunTime;

    public void Init(BuffType buffType, StatType statType, ProcessType processType, ModificationType modifyType, float value, float durationTime, bool isReturnValue)
    {
        BuffType = buffType;
        StatType = statType;
        ProcessType = processType;
        ModificationType = modifyType;
        Value = value;
        DurationTime = durationTime;
        IsReturnValue = isReturnValue;
    }
}
