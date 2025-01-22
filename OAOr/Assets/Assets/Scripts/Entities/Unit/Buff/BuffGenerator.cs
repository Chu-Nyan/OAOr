using Library.DesignPattern;
using System;
using System.Collections.Generic;

public class BuffGenerator : Singleton<BuffGenerator>
{
    private readonly ObjectPooling<Buff> _pool;
    private Dictionary<BuffType, BuffData> _data;

    private event Action<Buff> Generated;

    public BuffGenerator() : base()
    {
        _pool = new(() => new());
    }

    public void Init()
    {
        _data = DataManager.Instance.BuffDataContainer;
    }

    public Buff Generate(BuffType skillType)
    {
        var item = _pool.Dequeue();
        var data = _data[skillType];
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