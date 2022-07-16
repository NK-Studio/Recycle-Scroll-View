using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class RecycleScrollView : MonoBehaviour
{
    //아이템 프리팹
    [Tooltip("아이템 프리팹")] public GameObject originItem;
    [Range(0, 100), Tooltip("간격")] public float spacing = 0;

    //아이템 높이
    private float _itemHeight;
    private ScrollRect _scrollView;

    
    
    private void Awake()
    {
        _scrollView = GetComponent<ScrollRect>();

        //아이템 높이를 초기화 합니다.
        InitItemHeightSize();
    }

    private void InitItemHeightSize()
    {
        //아이템 사이즈를 가져온다.
        Assert.IsNotNull(originItem, "originItem != null");

        //아이템 렉트 컴포넌트를 가져온다.
        RectTransform itemRectTransform = originItem.GetComponent<RectTransform>();

        //에러 메세지 표시
        Assert.IsNotNull(itemRectTransform, "itemRectTransform != null");

        //렉트 정보를 가져옵니다.
        Rect itemRect = itemRectTransform.rect;

        //아이템 높이를 설정합니다.
        _itemHeight = itemRect.height;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Assert.IsTrue(originItem, "originItem이 비어 있습니다.");

        //height가 만약 1000이고 itemHeight : 150 + spacing : 30에 위 아래 보조 3개  
        int itemCount = (int)(_scrollView.viewport.rect.height / (_itemHeight + spacing)) + 3;

        //필요한 개수에 맞춰서 생성
        for (int i = 0; i < itemCount; i++)
        {
            //오브젝트 생성
            GameObject item = Instantiate(originItem, _scrollView.content);
            RectTransform itemRectTransform = item.GetComponent<RectTransform>();

            //간격
            float y = -i * (_itemHeight + spacing);

            //간격 적용
            itemRectTransform.anchoredPosition = new Vector3(0, y, 0);
        }
        
        SetContentHeight();
    }

    public void SetContentHeight()
    {
        // float x = _scrollView.content.sizeDelta.x;
        // float y = dataList.Count * itemHeight;
        // _scrollView.content.sizeDelta = new Vector2(x, y);
    }
}