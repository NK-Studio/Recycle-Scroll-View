using System;

[Serializable]
public class Item
{
    public string Name { get; private set; }
    public int Level { get; private set; }

    public Item(string name, int level)
    {
        this.Name = name;
        this.Level = level;
    }
}