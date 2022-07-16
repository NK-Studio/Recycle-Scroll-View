interface IitemView<T>
{
    void RenderItem();
    void SetItem(T item);
    T GetItem();
}