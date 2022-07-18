using System;
using Newtonsoft.Json;
using UnityEngine;

public class use : MonoBehaviour
{
    public RecycleScrollView<testItem> recycleScrollView;
    [Range(0, 100), Tooltip("간격")] public float spacing;
    public GameObject prefab;

    public TextAsset dataJson;
    
    private void Awake()
    {
        Item[] data = JsonConvert.DeserializeObject<Item[]>(dataJson.text);

        recycleScrollView.Init(data.Length, prefab, (arg0, i) =>
        {
            //어떻게 렌더링 할 것인가?
            arg0.SetItem(data[i]);

            //아이템 뷰를 다시 그립니다.
            arg0.RefreshView();
        }, spacing);
    }
}