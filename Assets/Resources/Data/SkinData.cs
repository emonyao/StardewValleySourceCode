using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkinData")]
public class SkinData : ScriptableObject
{
    public string skinName; // 皮肤名称
    public Sprite skinSprite; // 小屋对应的皮肤图像
    public Sprite uiIcon; // 用于装备栏显示的图标
}