using System;

public class PriorityQueue<T, K> where T : IPrioritizable<K> where K : IComparable<K>
{
    private readonly T[] _datas;
    private int _count;

    public int Count { get { return _count; } }

    public PriorityQueue(int Capacity)
    {
        _count = 0;
        _datas = new T[Capacity];
    }

    public void Enqueue(T node)
    {
        _count++;
        _datas[_count] = node;
        node.Index = _count;
        CompareParent(node);
    }

    public T Peek()
    {
        return _datas[1];
    }

    public T Dequeue()
    {
        if (_count < 1)
            throw new IndexOutOfRangeException();

        var firstNode = _datas[1];
        Remove(_datas[1]);
        return firstNode;
    }

    public void Refresh(T node)
    {
        CompareParent(node);
        CompareChildren(node);
    }

    private void CompareParent(T node)
    {
        while (node.Index > 1)
        {
            int parentIndex = node.Index >> 1;
            var parentNode = _datas[parentIndex];
            if (node.Priority.CompareTo(parentNode.Priority) >= 0)
                break;

            SwapIndex(node, parentNode);
        }
    }

    private void CompareChildren(T node)
    {
        if (_count < 1)
            return;

        int leftIndex = node.Index << 1; ;
        int rightIndex;
        int childIndex;

        while (leftIndex <= _count)
        {
            if (leftIndex == _count)
                childIndex = leftIndex;
            else
            {
                rightIndex = leftIndex + 1;
                childIndex = _datas[leftIndex].Priority.CompareTo(_datas[rightIndex].Priority) < 0 ? leftIndex : rightIndex;
            }

            if (_datas[childIndex].Priority.CompareTo(node.Priority) >= 0)
                break;

            SwapIndex(node, _datas[childIndex]);
            leftIndex = node.Index << 1;
        }
    }

    private void SwapIndex(T A, T B)
    {
        int temp = A.Index;
        A.Index = B.Index;
        B.Index = temp;

        _datas[A.Index] = A;
        _datas[B.Index] = B;
    }

    public void Remove(T node)
    {
        Remove(node.Index);
    }

    public void Remove(int index)
    {
        _datas[index] = _datas[_count];
        _datas[index].Index = index;
        _datas[_count] = default;
        _count--;
        CompareChildren(_datas[index]);
    }
}
