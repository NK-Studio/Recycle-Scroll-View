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
        //Select가 실행시 Callback함수를 실행합니다.
        infinity.AddSelectCallback(Callback);

        //데이터를 생성합니다.
        CreateData();
    }

    private void Callback(InfiniteScrollData obj)
    {
        NHNItem nhnItem = (NHNItem)obj;
        Debug.Log(nhnItem.Name);
        Debug.Log(nhnItem.Level);
    }

    private void CreateData()
    {
        NHNItem[] data = JsonConvert.DeserializeObject<NHNItem[]>(dataJson.text);

        Assert.IsNotNull(data, "data == null");

        foreach (NHNItem nhnItemData in data)
            infinity.InsertData(nhnItemData);
    }


    /// <summary>
    /// 해당 index값을 가진 아이템 뷰로 이동합니다.
    /// 아이템 뷰는 가운데에 표시됩니다.
    /// </summary>
    /// <param name="index"></param>
    public void MoveItemByIndex(int index)
    {
        infinity.MoveTo(index, InfiniteScroll.MoveToType.MOVE_TO_CENTER);
    }

    /// <summary>
    /// 모든 아이템을 클리어합니다.
    /// </summary>
    public void ClearItem()
    {
        infinity.Clear();
    }

    /// <summary>
    /// 첫번째 아이템 뷰를 제거합니다.
    /// </summary>
    public void RemoveItemByIndex(int index)
    {
        infinity.RemoveData(index);
    }

    /// <summary>
    /// 값이 변경되면 트리거 됩니다.
    /// </summary>
    /// <param name="fistDataIndex"></param>
    /// <param name="lastDataIndex"></param>
    /// <param name="isStartLine"></param>
    /// <param name="isEndLine"></param>
    public void OnValueChanged(int fistDataIndex, int lastDataIndex, bool isStartLine, bool isEndLine)
    {
        //some code...
    }

    /// <summary>
    /// 아이템 뷰의 액티브 상태가 변경되면 트리거 됩니다.
    /// </summary>
    /// <param name="dataIndex"></param>
    /// <param name="active"></param>
    public void OnChangeActiveItem(int dataIndex, bool active)
    {
        //some code...
    }

    /// <summary>
    /// 첫번째 위치로 이동하면 true를 반환합니다.
    /// </summary>
    /// <param name="isStartLine"></param>
    public void OnStartLine(bool isStartLine)
    {
        //some code...
    }

    /// <summary>
    /// 마지막 위치로 이동하면 true를 반환합니다.
    /// </summary>
    /// <param name="isEndLine"></param>
    public void OnEndLine(bool isEndLine)
    {
        //some code...
    }
}