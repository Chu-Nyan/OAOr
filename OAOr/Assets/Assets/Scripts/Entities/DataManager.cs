using Library.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private readonly Dictionary<UnitType, UnitStatusDTO> _unitDataContainer;
    private readonly Dictionary<BuffType, BuffData> _buffDataContainer;

    public Dictionary<UnitType, UnitStatusDTO> UnitDataContrainer
    {
        get => _unitDataContainer;
    }

    public Dictionary<BuffType, BuffData> BuffDataContainer
    {
        get => _buffDataContainer;
    }

    public DataManager()
    {
        _unitDataContainer = LoadObjectData<UnitStatusDTO, UnitType>(Const.UnitDataContainer);
        _buffDataContainer = LoadObjectData<BuffData, BuffType>(Const.BuffDataContainer);
    }

    public UnitStatusDTO GetPlayerData()
    {
        // TODO : 저장된 데이터 읽을 수 있게
        return _unitDataContainer[UnitType.Player];
    }

    public T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    private Dictionary<K, T> LoadObjectData<T,K>(string path) where K : Enum
    {
        var datas = new Dictionary<K, T>();
        var so = LoadAsset<ScriptableObject>(path);
        var container = (IContainerProvier<T,K>) so;
        var list = container.GetDataList;
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(container.GetKeyType(i), list[i]);
        }

        return datas;
    }
}
