using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class RecycleScrollView<T> : MonoBehaviour where T : MonoBehaviour
{
    //아이템 프리팹
    private GameObject _originItem;

    //스크롤 뷰
    private ScrollRect _scrollView;

    //아이템 높이
    private float _itemHeight;

    //아이템 개수
    private int _count;

    //아이템 뷰 컴포넌트 수록
    private List<T> _itemList;

    //어떻게 그릴건지 처리합니다.
    private UnityAction<T, int> _renderView;

    //간격
    private float _spacing;

    private void InitItemHeightSize()
    {
        //_originItem이 비어있으면 에러 표시
        Assert.IsNotNull(_originItem, "originItem == null");

        //아이템 뷰 오브젝트의 Rect트랜스폼 컴포넌트를 가져온다.
        RectTransform itemRectTransform = _originItem.GetComponent<RectTransform>();

        //RectTransform을 못가져오면 에러 표시
        Assert.IsNotNull(itemRectTransform, "itemRectTransform == null");

        //Rect 정보를 가져옵니다.
        Rect itemViewRect = itemRectTransform.rect;

        //아이템 높이를 설정합니다.
        _itemHeight = itemViewRect.height;
    }

    private void InitContentHeight()
    {
        //스크롤 뷰가 null이면 에러 표시
        Assert.IsNotNull(_scrollView, "_scrollView == null");

        //스크롤 뷰 컨텐츠의 사이즈 X를 가져옵니다.
        float x = _scrollView.content.sizeDelta.x;

        //스크롤 뷰 컨텐츠의 사이즈 Y를 계산합니다.
        float y = _count * (_itemHeight + _spacing) - _spacing;

        //스크롤 뷰 컨텐츠의 사이즈를 재 정의합니다.
        _scrollView.content.sizeDelta = new Vector2(x, y);
    }

    private void CreateItem(UnityAction<T, int> renderView)
    {
        //_originItem이 isNotNull이 아니면 에러를 발생합니다.
        Assert.IsNotNull(_originItem, "_originItem == null");

        //height가 만약 1000이고 itemHeight : 150 + spacing : 30에 위 아래 보조 3개  
        int itemCount = (int)(_scrollView.viewport.rect.height / (_itemHeight + _spacing)) + 3;

        //ItemList를 초기화합니다.
        _itemList = new List<T>();

        //필요한 개수에 맞춰서 생성
        for (int i = 0; i < itemCount; i++)
        {
            //오브젝트 생성
            GameObject item = Instantiate(_originItem, _scrollView.content);
            T view = item.GetComponent<T>();

            //간격
            float y = -i * (_itemHeight + _spacing);

            //간격 적용
            item.transform.localPosition = new Vector3(0, y, 0);

            //게임 오브젝트 적용
            _itemList.Add(view);
            renderView?.Invoke(view, i);
        }
    }

    private bool RefreshPositionItem(Transform item, float contentPosY, float contentPosUpY, float contentPosBottomY)
    {
        //아이템 앵커포인트 기반 Y포지션을 계산한다.
        float itemPosY = item.localPosition.y + contentPosY;

        //아이템이 컨텐츠 영역 상단으로 넘어갔을 때,
        bool isUpLine = itemPosY > contentPosUpY;

        //아이템이 컨텐츠 영역 하단으로 넘어갔을 때,
        bool isDownLine = itemPosY < contentPosBottomY;

        if (isUpLine)
        {
            Vector3 itemPos = item.localPosition;
            itemPos.y -= _itemList.Count * (_itemHeight + _spacing); //아이템 개수 * 아이템 높이 + 마진
            item.localPosition = itemPos;

            RefreshPositionItem(item.transform, contentPosY, contentPosUpY, contentPosBottomY);
            return true;
        }

        if (isDownLine)
        {
            Vector3 itemPos = item.localPosition;
            itemPos.y += _itemList.Count * (_itemHeight + _spacing); //아이템 개수 * 아이템 높이 + 마진
            item.localPosition = itemPos;

            RefreshPositionItem(item.transform, contentPosY, contentPosUpY, contentPosBottomY);
            return true;
        }

        return false;
    }

    private void Update()
    {
        Assert.IsNotNull(_scrollView, "Init이 처리되지 않았습니다.");

        //스크롤 했을 때 컨텐츠 오브젝트 Y를 반환합니다.
        float contentPosY = _scrollView.content.anchoredPosition.y;

        //이 값을 넘어가면 스크롤 뷰 rectMaxY를 넘어간 것이다.
        float itemHeightSpacing = _itemHeight + _spacing;

        //컨텐츠 Rect상단에 아이템 2개 정도 높이
        float contentPosUpY = itemHeightSpacing * 2;

        //컨텐츠 Rect하단에 아이템 1개 정도 밑 높이
        float contentPosBottomY = -(_scrollView.viewport.rect.height + itemHeightSpacing);

        foreach (T itemView in _itemList)
        {
            //아이템 뷰들의 위치를 갱신합니다. (보이는 영역 밖으로 나가면 true, 아닐 시 false)
            bool isChange = RefreshPositionItem(itemView.transform, contentPosY, contentPosUpY, contentPosBottomY);

            //보이는 영역 밖으로 나갔다면,
            if (isChange)
            {
                //영역 밖으로 나가면 아이템 뷰에 표시되야하는 데이터의 인덱스를 재계산합니다.
                int index = (int)(-itemView.transform.localPosition.y / itemHeightSpacing);

                //아이템 뷰가 인덱스 영역 밖이면 안보이게 한다.
                if (index < 0 || index >= _count)
                {
                    itemView.gameObject.SetActive(false);
                    return;
                }

                //아이템 뷰를 무조건 활성화 해준다.
                itemView.gameObject.SetActive(true);

                //재정의된 인덱스를 가지고 아이템 뷰를 다시 렌더링한다.
                _renderView.Invoke(itemView, index);
            }
        }
    }

    public void Init(int count, T itemPrefab, UnityAction<T, int> renderView)

    {
        _scrollView = GetComponent<ScrollRect>();

        _count = count;
        _originItem = itemPrefab.gameObject;
        _renderView = renderView;

        InitItemHeightSize();

        InitContentHeight();

        CreateItem(renderView);
    }
}