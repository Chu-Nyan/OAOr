using System.Collections.Generic;

public class UnitStatus
{
    private readonly int _id;
    private UnitType _type;
    private readonly Dictionary<StatType, Stat> _stats;
    private readonly List<Buff> _buffs;

    public int ID
    {
        get => _id;
    }

    public Stat this[StatType type] 
    { 
        get => _stats[type];
    }

    public UnitStatus(int id)
    {
        _buffs = new();
        _stats = Utilities.GenerateStats();
        _id = id;
    }

    public void Init(UnitType type)
    {
        _type = type;
    }

    public void Hit(List<BuffType> buffs)
    {
        foreach (var type in buffs)
        {
            var buff = BuffGenerator.Instance.Generate(type);
            ApplyBuff(buff);
        }
    }

    public void Hit(float dmg, List<BuffType> buffs)
    {
        Hit(buffs);
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
            BuffTimer.Instance.AddBuff(buff);
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
