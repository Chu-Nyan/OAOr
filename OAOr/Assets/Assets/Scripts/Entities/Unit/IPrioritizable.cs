using System;

public interface IPrioritizable<T> where T : IComparable<T>
{
    public int Index { get; set; }
    public T Priority { get; }
}
