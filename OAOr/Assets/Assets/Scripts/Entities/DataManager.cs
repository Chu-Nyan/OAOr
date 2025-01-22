using Library.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public readonly Dictionary<UnitType, UnitStatusDTO> UnitDataContainer;
    public readonly Dictionary<BuffType, BuffData> BuffDataContainer;
    public readonly Dictionary<SkillType, SkillData> SkillDataContainer;

    public DataManager()
    {
        UnitDataContainer = LoadObjectData<UnitStatusDTO, UnitType>(Const.UnitDataContainer);
        BuffDataContainer = LoadObjectData<BuffData, BuffType>(Const.BuffDataContainer);
        SkillDataContainer = LoadObjectData<SkillData, SkillType>(Const.SkillDataContainer);
    }

    public UnitStatusDTO GetPlayerData()
    {
        // TODO : 저장된 데이터 읽을 수 있게
        return UnitDataContainer[UnitType.Player];
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
