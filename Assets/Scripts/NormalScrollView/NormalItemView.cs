using UnityEngine;
using UnityEngine.UI;

    public class NormalItem
    {
        public string Name { get; private set; }
        public int Level { get; private set; }

        public NormalItem(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }

    public class NormalItemView : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        private NormalItem _item;
        
        public void SetItem(NormalItem item) => 
            _item = item;

        public  void RenderView()
        {
            nameText.text = _item.Name;
            levelText.text = $"Lv. {_item.Level}";
        }
        
        public void OnClick()
        {
            Debug.Log(_item.Name);
            Debug.Log($"Lv. {_item.Level}");
        }
    }