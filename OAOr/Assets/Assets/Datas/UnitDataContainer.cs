using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDatas", menuName = "Scriptable Objects/UnitDatas")]
public class UnitDataContainer : ScriptableObject
{
    public List<UnitStatusDTO> UnitDatas;
}

[Serializable]
public struct UnitStatusDTO
{
    public UnitType type;
    public float HP;
    public float Speed;
    public float AttackSpeed;
    public float Damage;
    public float Defence;

    public void ConvertData(Dictionary<StatType, Stat> dic)
    {
        if (dic == null)
            throw new NullReferenceException();

        dic[StatType.HP].ChangeDefaultValue(HP);
        dic[StatType.Speed].ChangeDefaultValue(Speed);
        dic[StatType.AttackSpeed].ChangeDefaultValue(AttackSpeed);
        dic[StatType.Damage].ChangeDefaultValue(Damage);
        dic[StatType.Defence].ChangeDefaultValue(Defence);
    }
}
