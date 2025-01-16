using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoBehaviour
{
    public static ItemMoveHandler Instance { get; private set; }

    private Image icon;
    //private SlotUI selectedSlotUI;
    private SlotData selectedSlotData;

    private Player player;

    private bool isCtrlDown = false;

    private void Awake()
    {
        Instance = this;
        icon = GetComponentInChildren<Image>();
        HideIcon();
        player = GameObject.FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        if (icon.enabled)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(), Input.mousePosition,
                null,
                out position);
            icon.GetComponent<RectTransform>().anchoredPosition = position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                ThrowItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCtrlDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCtrlDown = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            ClearHandForced();
        }
    }




    public void OnSlotClick(SlotUI slotui)
    {
        //�ж������Ƿ�Ϊ��
        if (selectedSlotData != null)
        {
            //1-��Ϊ��
            //1-1��ǰ�����һ���ո���
            if (slotui.GetData().IsEmpty())
            {
                MoveToEmptySlot(selectedSlotData, slotui.GetData());
            }
            else
            {
                //1-2��ǰ�����һ���ǿո���
                //1-2-3���������
                //1-2-3����˱�ĸ�������
                if (selectedSlotData == slotui.GetData()) return;
                else
                { //1-2-3����˱�ĸ�������

                    //����һ�� �� ���Ͳ�һ��
                    if(selectedSlotData.item== slotui.GetData().item)
                    {
                        MoveToNotEmptySlot(selectedSlotData, slotui.GetData());
                    }
                    else
                    {
                        SwitchData(selectedSlotData, slotui.GetData());
                    }

                }

            }
        }
        else
        {
            //2-����Ϊ��
            if (slotui.GetData().IsEmpty()) return;
            selectedSlotData = slotui.GetData();
            ShowIcon(selectedSlotData.item.sprite);
        }
        
    }


    void HideIcon()
    {
        icon.enabled = false;
    }
    void ShowIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    void ClearHand()
    {
        if (selectedSlotData.IsEmpty())
        {
            HideIcon(); 
            selectedSlotData = null;
        }
    }
    void ClearHandForced()
    {
        HideIcon();
        selectedSlotData = null;
    }


    private void ThrowItem()
    {
        if (selectedSlotData != null)
        {
            GameObject prefab = selectedSlotData.item.prefab;
            int count = selectedSlotData.count;
            if (isCtrlDown)
            {
                player.ThrowItem(prefab, 1);
                selectedSlotData.Reduce();
            }
            else
            {
                player.ThrowItem(prefab, count);
                selectedSlotData.Clear();
            }
            ClearHand();
        }
        
    }
    private void MoveToEmptySlot(SlotData fromData,SlotData toData)
    {
        if (isCtrlDown)
        {
            toData.AddItem(fromData.item);
            fromData.Reduce();
        }
        else
        {
            toData.MoveSlot(fromData);
            fromData.Clear();
        }
        ClearHand();
    }
    private void MoveToNotEmptySlot(SlotData fromData, SlotData toData)
    {
        if (isCtrlDown)
        {
            if (toData.CanAddItem())
            {
                toData.Add();
                fromData.Reduce();
            }
            
        }
        else
        {
            int freespace = toData.GetFreeSpace();
            if (fromData.count > freespace)
            {
                toData.Add(freespace);
                fromData.Reduce(freespace);
            }
            else
            {
                toData.Add(fromData.count);
                fromData.Clear();
            }
            
        }
        ClearHand();
    }
    private void SwitchData(SlotData data1, SlotData data2)
    {
        ItemData item = data1.item;
        int count = data1.count;
        data1.MoveSlot(data2);

        data2.AddItem(item,count);

        ClearHandForced();
    }
}
