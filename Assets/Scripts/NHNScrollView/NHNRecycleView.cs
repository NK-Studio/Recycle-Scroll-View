using Gpm.Ui;
using Newtonsoft.Json;
using UnityEngine;

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

        foreach (NHNItem nhnItemData in data) 
            infinity.InsertData(nhnItemData);
    }
}