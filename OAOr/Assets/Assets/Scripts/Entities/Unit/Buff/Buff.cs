using System;
using System.Collections.Generic;
using UnityEngine;

public class Buff : IPrioritizable<float>
{
    private const float CycleTime = 1.0f;
    protected BuffData _data;
    public float EndTime;
    public int _index = -1;
    public int cccount;

    private event Action<Buff> Runed;
    private event Action<Buff> Ended;
    private event Action<Buff> Refreshed;

    public int Index
    {
        get => _index; 
        set => _index = value;
    }

    public float Priority
    {
        get => EndTime;
    }

    public BuffData Data
    {
        get => _data;
    }

    public static bool TryGetSameBuff(IList<Buff> list, Buff buff, out Buff find)
    {
        find = null;
        for (int i = 0; i < list.Count; i++)
        {
            if (IsSameBuff(list[i], buff) == true)
            {
                find = list[i];
                break;
            }
        }

        return find != null;
    }

    public static bool IsSameBuff(Buff a, Buff b)
    {
        return a._data.BuffType == b._data.BuffType && a._data.StatType == b._data.StatType;
    }

    public void Init(BuffData data)
    {
        _data = data;
        EndTime = Time.time + data.DurationTime;
        if (data.ProcessType == ProcessType.Loop)
        {
            data.NextRunTime += 1.0f;
            Runed += AddNextTime;
        }
    }

    public void Run()
    {
        cccount++;
        Runed?.Invoke(this);
    }

    public void Refresh()
    {
        EndTime = Time.time + _data.DurationTime;
        Refreshed?.Invoke(this);
    }

    public void Destroy()
    {
        Ended?.Invoke(this);
        Ended = null;
        Debug.Log($"지속 시간 : {Data.DurationTime}, 반복 횟수 : {cccount}");
    }

    public void RegisterRuned(Action<Buff> buff)
    {
        Runed += buff;
    }

    public void RegisterEnded(Action<Buff> action)
    {
        Ended += action;
    }

    public void RegisterRefreshed(Action<Buff> action)
    {
        Refreshed += action;
    }

    private void AddNextTime(Buff buff)
    {
        buff.Data.NextRunTime += CycleTime;
    }
}
