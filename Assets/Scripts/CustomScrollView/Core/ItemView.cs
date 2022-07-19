using UnityEngine;

public abstract class ItemView<T> : MonoBehaviour 
{
    private T _item;
    
    public void SetItem(T item)
    {
        _item = item;
    }

    public T GetItem()
    {
        return _item;
    }

    public abstract void RefreshView();
}