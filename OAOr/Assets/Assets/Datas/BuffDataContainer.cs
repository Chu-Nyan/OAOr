using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffContainer", menuName = "Scriptable Objects/BuffContainer")]
public class BuffDataContainer : ScriptableObject, IContainerProvier<BuffData,BuffType>
{
    [SerializeField]
    private List<BuffData> _buffDatas;

    public List<BuffData> GetDataList
    {
        get => _buffDatas;
    }

    public BuffType GetKeyType(int index)
    {
        return _buffDatas[index].BuffType;
    }
}
