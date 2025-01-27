using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public int index = 0;
    private SlotData data;

    public Image iconImage;
    public TextMeshProUGUI countText;


    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);

        UpdateUI();
    }
    public SlotData GetData()
    {
        return data;
    }

    private void OnDataChange()
    {
        UpdateUI();
    }

    // private void UpdateUI()
    // {
    //     if (data.item == null)
    //     {
    //         iconImage.enabled = false;
    //         countText.enabled = false;
    //     }
    //     else
    //     {
    //         iconImage.enabled = true;
    //         countText.enabled = true;
    //         iconImage.sprite = data.item.sprite;
    //         countText.text = data.count.ToString();
    //     }
    // }


    private void UpdateUI()
    {
        if (data == null || data.item == null)
        {
            // 如果数据为空，隐藏图标和数量
            iconImage.enabled = false;
            countText.enabled = false;
        }
        else
        {
            // 显示物品图标
            iconImage.enabled = true;
            iconImage.sprite = data.item.sprite;

            // 如果是皮肤类型，隐藏数量文本
            if (data.item.type == ItemType.Skin)
            {
                countText.enabled = false; // 皮肤没有数量概念
            }
            else
            {
                countText.enabled = true;
                countText.text = data.count.ToString(); // 显示物品数量
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ItemMoveHandler.Instance.OnSlotClick(this);
        // 如果是皮肤类型，切换小屋皮肤
        if (data != null && data.item != null && data.item.type == ItemType.Skin)
        {
            HutManager.Instance.ChangeSkin(Resources.Load<SkinData>($"Data/{data.item.subType}"));
            Debug.Log($"Switched to skin: {data.item.subType}");
        }
        else
        {
            // 调用物品移动逻辑
            ItemMoveHandler.Instance.OnSlotClick(this);
        }
    }
}
