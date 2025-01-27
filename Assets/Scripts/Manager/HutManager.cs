using UnityEngine;

public class HutManager : MonoBehaviour
{
    public static HutManager Instance { get; private set; }
    private SpriteRenderer hutRenderer;

    private void Awake()
    {
        Instance = this;
        hutRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // 示例：加载并切换到 Hut_Winter 皮肤
        SkinData hutWinterSkin = Resources.Load<SkinData>("Data/Hut_Default");
        ChangeSkin(hutWinterSkin);
    }

    public void ChangeSkin(SkinData skinData)
    {
        if (hutRenderer != null && skinData != null)
        {
            hutRenderer.sprite = skinData.skinSprite;
            Debug.Log($"Changed hut skin to: {skinData.skinName}");
        }
    }
}