using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testItem : ItemView<Item>
{
    public Text nameText;
    public Text levelText;
    
    public override void RefreshView()
    {
        Item itemInfo = GetItem();
        nameText.text = itemInfo.Name;
        levelText.text = $"Lv. {itemInfo.Level}";
    }
}
