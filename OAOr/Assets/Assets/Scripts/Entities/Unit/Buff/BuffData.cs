public class BuffData
{
    public SkillType SkillType;
    public StatType StatType;
    public ProcessType ProcessType;
    public ModificationType ModificationType;
    public float Value;
    public float DurationTime;
    public bool IsReturnValue;

    public float NextRunTime;

    public void Init(SkillType skillType, StatType statType, ProcessType processType, ModificationType modifyType, float value, float durationTime, bool isReturnValue)
    {
        SkillType = skillType;
        StatType = statType;
        ProcessType = processType;
        ModificationType = modifyType;
        Value = value;
        DurationTime = durationTime;
        IsReturnValue = isReturnValue;
    }
}
