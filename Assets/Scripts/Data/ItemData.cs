using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None=0,
    Tool=1,
    Seed=2,
    Crop=3,   // 作物（已成熟）
    Animal=4,
    Skin=5 // 小屋皮肤
}

public enum SubType
{
    None=0,
    Carrot=10,
    Tomato=20,
    Hoe=30,
    Chicken=40,
    Cow=50,
    Hut_Default = 60, // 默认皮肤
    Hut_Winter = 70,   // 冬季皮肤
    Hut_Summer = 80  //夏季皮肤
}

[CreateAssetMenu()]
public class ItemData :ScriptableObject
{
    public ItemType type=ItemType.None; // 大类（种子、工具、作物等）
    public SubType subType = SubType.None; // 具体种类（胡萝卜种子、锄头等）
    public Sprite sprite;
    public GameObject prefab;
    public int maxCount=1;

    // 作物生长的相关字段
    public Sprite[] growthSprites; // 作物生长阶段的图片（数组，每阶段一张图）
    public float totalGrowthTime; // 总生长时间（秒）

    // 小屋皮肤相关字段
    [Header("Skin Settings")]
    [Condition("type", ItemType.Skin)] // 仅在类型为 Skin 时显示
    public bool isUnlocked = false; // 是否解锁
}
