using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public string Name { get; private set; }
    public int Level { get; private set; }

    public Item(string name, int level)
    {
        Name = name;
        Level = level;
    }
}

public class customItemView : ItemView<Item>
{
    public Text nameText;
    public Text levelText;
    
    public override void RefreshView()
    {
        Item itemInfo = GetItem();
        nameText.text = itemInfo.Name;
        levelText.text = $"Lv. {itemInfo.Level}";
    }

    public void OnClick()
    {
        Item itemInfo = GetItem();
        Debug.Log(itemInfo.Name);
        Debug.Log($"Lv. {itemInfo.Level}");
    }
}
