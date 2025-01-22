using System;
using System.Collections.Generic;

public interface IContainerProvier<T, K> where K : Enum
{
    public List<T> GetDataList { get; }
    public K GetKeyType(int index);
}
