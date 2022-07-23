using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

public class CustomRecycleView : RecycleScrollView<CustomItemView>
{
    public CustomItemView prefab;
    public TextAsset dataJson;
    
    private RecycleScrollView<CustomItemView> _recycleScrollView;
    
    private void Awake()
    {
        Item[] data = JsonConvert.DeserializeObject<Item[]>(dataJson.text);

        Assert.IsNotNull(data,"data == null");
        
        _recycleScrollView = GetComponent<RecycleScrollView<CustomItemView>>();
        _recycleScrollView.Init(data.Length, prefab, (arg0, i) =>
        {
            //어떻게 렌더링 할 것인가?
            arg0.SetItem(data[i]);
            
            //아이템 뷰를 다시 그립니다.
            arg0.RefreshView();
        });
        
    }
}
