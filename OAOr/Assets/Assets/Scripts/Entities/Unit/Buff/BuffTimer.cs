using Library.DesignPattern;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimer : MonoBehaviourSingleton<BuffTimer>
{
    private const float RefreshCycle = 0.1f;
    private float _refreshTime;
    private PriorityQueue<Buff, float> _singleBuff;
    private HashSet<Buff> _loopBuff;

    protected override void Awake()
    {
        base.Awake();
        _singleBuff = new(64);
        _loopBuff = new(64);
    }

    private void Update()
    {
        if (_singleBuff.Count == 0)
            return;

        _refreshTime += Time.deltaTime;
        if (_refreshTime >= RefreshCycle)
        {
            _refreshTime = 0;
            var currentTime = Time.time;

            foreach (var item in _loopBuff)
            {
                if (item.Data.NextRunTime < currentTime)
                    item.Run();
            }

            while (_singleBuff.Count > 0 && _singleBuff.Peek().EndTime <= currentTime)
            {
                var buff = _singleBuff.Dequeue();
                if (buff.Data.ProcessType == ProcessType.Loop)
                    _loopBuff.Remove(buff);

                buff.Destroy();
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        buff.RegisterRefreshed(_singleBuff.Refresh);
        _singleBuff.Enqueue(buff);
        if (buff.Data.ProcessType == ProcessType.Loop)
            _loopBuff.Add(buff);
        buff.Run();
    }
}
