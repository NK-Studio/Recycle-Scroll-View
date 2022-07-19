using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

public class CustomRecycleView : RecycleScrollView<customItemView>
{
    public customItemView prefab;
    public TextAsset dataJson;
    
    private RecycleScrollView<customItemView> _recycleScrollView;
    
    private void Awake()
    {
        Item[] data = JsonConvert.DeserializeObject<Item[]>(dataJson.text);

        Assert.IsNotNull(data,"data == null");
        
        _recycleScrollView = GetComponent<RecycleScrollView<customItemView>>();
        _recycleScrollView.Init(data.Length, prefab, (arg0, i) =>
        {
            //어떻게 렌더링 할 것인가?
            arg0.SetItem(data[i]);
            
            //아이템 뷰를 다시 그립니다.
            arg0.RefreshView();
        });
        
    }
}
