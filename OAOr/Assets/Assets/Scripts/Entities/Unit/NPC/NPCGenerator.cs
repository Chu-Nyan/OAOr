using Library.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : Singleton<NPCGenerator>
{
    private readonly ObjectPooling<NPCController> _pool;
    private Dictionary<UnitType, UnitStatusDTO> _container;
    private NPCController _new;

    private event Action<NPCController> Generated;

    public NPCGenerator() : base()
    {
        _pool = new(() =>
        {
            return GameObject.Instantiate(DataManager.Instance.LoadAsset<NPCController>(Const.NPCPrefab));
        });
    }

    public void Init(Dictionary<UnitType, UnitStatusDTO> unitDataContrainer)
    {
        _container = unitDataContrainer;
    }

    public void Ready(UnitType type)
    {
        if (type == UnitType.Player)
            return;

        _new = _pool.Dequeue();
        _new.Status.InitData(_container[type]);
    }

    public NPCController Generate()
    {
        return _new;
    }
}
