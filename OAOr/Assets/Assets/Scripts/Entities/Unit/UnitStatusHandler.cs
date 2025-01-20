using System;
using System.Collections.Generic;

public class UnitStatus
{
    private static readonly StatType[] _types = (StatType[])Enum.GetValues(typeof(StatType));
    private static BuffTimer _buffTimer;

    private readonly Dictionary<StatType, Stat> _stats;
    private readonly List<Buff> _buffs;

    public Stat this[StatType type] 
    { 
        get => _stats[type];
    }

    public UnitStatus(BuffTimer timer)
    {
        _stats = new();
        _buffs = new();
        _buffTimer = timer;

        for (int i = 0; i < _types.Length; i++)
        {
            var type = _types[i];
            _stats.Add(type, new(type, 10, 10));
        }
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
