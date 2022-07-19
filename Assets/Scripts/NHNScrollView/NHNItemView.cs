using Gpm.Ui;
using UnityEngine.UI;

public class NHNItem : InfiniteScrollData
{
    public string Name { get; private set; }
    public int Level { get; private set; }

    public NHNItem(string name, int level)
    {
        Name = name;
        Level = level;
    }
}

public class NHNItemView : InfiniteScrollItem
{
    public Text nameText;
    public Text levelText;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        NHNItem item = (NHNItem)scrollData;
        nameText.text = item.Name;
        levelText.text = $"Lv. {item.Level}";
    }

    public void OnClick()
    {
        OnSelect();
    }
}
