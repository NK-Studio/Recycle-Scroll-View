using System;
using Newtonsoft.Json;
using UnityEngine;

public class use : MonoBehaviour
{
    public RecycleScrollView view;
    public TextAsset dataJson;
    public GameObject prefab;
    [Range(0, 100), Tooltip("간격")] public float spacing;

    private void Awake()
    {
        Item[] data = JsonConvert.DeserializeObject<Item[]>(dataJson.text);

        view.Init(data.Length, prefab, spacing);
    }
}

//
//
// public class SingerAdapter : MonoBehaviour, IBaseAdapter
// {
//     public TextAsset dataJson;
//     
//     public int getCount()
//     {
//         //사용할 데이터 가져오기
//         Item[] data = JsonConvert.DeserializeObject<Item[]>(dataJson.text);
//
//         return data.Length;
//     }
//
//     public void addItem()
//     {
//         
//     }
//     
//     
// }