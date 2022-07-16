using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IitemView<Item>
{
    private Item _item;
    public Text nameText;
    public Text levelText;
    
    public void RenderItem()
    {
        levelText.text = $"Lv. {_item.Level}";
        nameText.text = _item.Name;
    }

    public void SetItem(Item item) => _item = item;
    
    public Item GetItem() => _item;
}