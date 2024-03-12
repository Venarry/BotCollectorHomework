using System;

public class StorageModel
{
    public int Count { get; private set;}

    public StorageModel()
    {
    }

    public StorageModel(int baseValue)
    {
        Count = baseValue;
    }

    public event Action Added;
    public event Action Removed;

    public void Add(int count = 1)
    {
        if(count < 0)
            count = 0;

        Count += count;
        Added?.Invoke();
    }

    public bool TryRemove(int count = 1)
    {
        if (Count < count)
            return false;

        Count -= count;
        Removed?.Invoke();
        return true;
    }
}
