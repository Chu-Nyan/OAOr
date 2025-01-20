using Library.DesignPattern;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private readonly Dictionary<UnitType, UnitStatusDTO> _unitDataContainer;

    public Dictionary<UnitType, UnitStatusDTO> UnitDataContrainer
    {
        get => _unitDataContainer;
    }

    public DataManager()
    {
        _unitDataContainer = LoadUnitData();
    }

    public UnitStatusDTO GetPlayerData()
    {
        // TODO : 저장된 데이터 읽을 수 있게
        return _unitDataContainer[UnitType.Player];
    }

    public T LoadAsset<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    private Dictionary<UnitType, UnitStatusDTO> LoadUnitData()
    {
        var datas = new Dictionary<UnitType, UnitStatusDTO>();
        var list = LoadAsset<UnitDataContainer>(Const.UnitDataContainer).UnitDatas;
        foreach (var item in list)
            datas.Add(item.type, item);

        return datas;
    }
}
