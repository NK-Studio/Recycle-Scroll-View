using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class NormalScrollView : MonoBehaviour
{
    [SerializeField] private TextAsset dataJson;
    [SerializeField] private ScrollRect ScrollView;
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        NormalItem[] data = JsonConvert.DeserializeObject<NormalItem[]>(dataJson.text);

        for (int i = 0; i < data.Length; i++)
        {
            NormalItemView view = Instantiate(prefab, ScrollView.content).GetComponent<NormalItemView>();
            view.SetItem(data[i]);
            view.RenderView();
        }
    }
}