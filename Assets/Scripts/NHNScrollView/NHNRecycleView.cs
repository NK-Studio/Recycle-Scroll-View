using System;
using Gpm.Ui;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

public class NHNRecycleView : MonoBehaviour
{
    public TextAsset dataJson;
    public InfiniteScroll infinity;

    private void Start()
    {
        infinity.AddSelectCallback(data =>
        {
            NHNItem nhnItem = (NHNItem)data;
            Debug.Log(nhnItem.Name);
            Debug.Log(nhnItem.Level);
        });

        CreateData();
    }

    private void CreateData()
    {
        NHNItem[] data = JsonConvert.DeserializeObject<NHNItem[]>(dataJson.text);

        Assert.IsNotNull(data, "data == null");

        foreach (NHNItem nhnItemData in data)
            infinity.InsertData(nhnItemData);
    }

    public void MoveToIndex(int index)
    {
        infinity.MoveTo(index, InfiniteScroll.MoveToType.MOVE_TO_CENTER);
    }

    public void ClearItem()
    {
        infinity.Clear();
    }

    public void RemoveFirst()
    {
        infinity.RemoveData(0);
    }
}