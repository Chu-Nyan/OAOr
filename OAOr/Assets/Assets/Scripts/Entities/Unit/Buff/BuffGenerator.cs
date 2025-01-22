using Library.DesignPattern;
using System;
using System.Collections.Generic;

public class BuffGenerator : Singleton<BuffGenerator>
{
    private readonly ObjectPooling<Buff> _pool;

    private event Action<Buff> Generated;

    public BuffGenerator() : base()
    {
        _pool = new(() => new());
    }

    public Buff Generate(BuffType skillType, StatType statType, ProcessType processType, ModificationType modifyType, float value, float durationTime, bool isReturnValue)
    {
        var item = _pool.Dequeue();
        var data = new BuffData();
        data.Init(skillType, statType, processType, modifyType, value, durationTime, isReturnValue);
        item.Init(data);
        item.RegisterEnded(AddPool);
        Generated?.Invoke(item);
        return item;
    }

    public void RegisterGenerated(Action<Buff> action)
    {
        Generated += action;
    }

    private void AddPool(Buff buff)
    {
        _pool.Enqueue(buff);
    }
}