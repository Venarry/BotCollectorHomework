using System;

public class StorageModel
{
    public int Count { get; private set;}

    public event Action Added;

    public void Add()
    {
        Count++;
        Added?.Invoke();
    }

    public void Add(int count)
    {
        Count += count;
        Added?.Invoke();
    }
}
