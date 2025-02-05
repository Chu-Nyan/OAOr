using Library.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : Singleton<NPCGenerator>
{
    private readonly ObjectPooling<NPCController> _pool;
    private IDHandler _idHandler;
    private Dictionary<UnitType, UnitStatusDTO> _container;
    private NPCController _new;

    private event Action<NPCController> Generated;

    public NPCGenerator() : base()
    {
        _idHandler = new(Const.NPCID);
        _pool = new(() =>
        {
            var newNpc = GameObject.Instantiate(DataManager.Instance.LoadAsset<NPCController>(Const.NPCPrefab));
            newNpc.Init(_idHandler.GetNewID());
            return newNpc;
        });
    }


    public NPCGenerator Ready(UnitType type)
    {
        if (type == UnitType.Player)
            throw new Exception("NPC빌더가 Player를 생성 중");

        _new = _pool.Dequeue();
        _new.Status.InitData(DataManager.Instance.UnitDataContainer[type]);
        return this;
    }

    public NPCController Generate()
    {
        Generated?.Invoke(_new);
        return _new;
    }
}

public class IDHandler
{
    private int _nextID;

    public IDHandler(int IDStartNumber)
    {
        _nextID = IDStartNumber;
    }

    public int GetNewID()
    {
        var next = _nextID;
        _nextID++;

        return next;
    }
}