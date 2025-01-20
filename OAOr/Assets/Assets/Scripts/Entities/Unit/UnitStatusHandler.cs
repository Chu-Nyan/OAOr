using System.Collections.Generic;

public class UnitStatus
{
    private static BuffTimer _buffTimer;

    private UnitType _type;
    private readonly Dictionary<StatType, Stat> _stats;
    private readonly List<Buff> _buffs;

    public Stat this[StatType type] 
    { 
        get => _stats[type];
    }

    public UnitStatus(UnitType type)
    {
        _buffs = new();
        _stats = Utilities.GenerateStats();
    }

    public static void InitBuffTimer(BuffTimer buffTimer)
    {
        _buffTimer = buffTimer;
    }

    public void InitData(UnitStatusDTO dto)
    {
        _type = dto.type;
        dto.ConvertData(_stats);
    }

    public void ApplyBuff(Buff buff)
    {
        if (Buff.TryGetSameBuff(_buffs, buff, out var item) == true)
        {
            item.Refresh();
        }
        else
        {
            _buffs.Add(buff);
            buff.RegisterRuned(AddModificationValue);
            buff.RegisterEnded(RemoveModificationValue);
            _buffTimer.AddBuff(buff);
        }
    }

    private void AddModificationValue(Buff buff)
    {
        var data = buff.Data;
        _stats[data.StatType].AddModificationValue(data);
    }

    private void RemoveModificationValue(Buff buff)
    {
        _buffs.Remove(buff);
        var data = buff.Data;
        if (data.IsReturnValue == true)
            _stats[data.StatType].RemoveModificationValue(data);
    }
}
