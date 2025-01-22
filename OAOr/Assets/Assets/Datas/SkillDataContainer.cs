using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataContainer", menuName = "Scriptable Objects/SkillDataContainer")]
public class SkillDataContainer : ScriptableObject, IContainerProvier<SkillData, SkillType>
{
    [SerializeField]
    private List<SkillData> _datas;

    public List<SkillData> GetDataList
    {
        get => _datas;
    }

    public SkillType GetKeyType(int index)
    {
        return _datas[index].SkillType;
    }
}
