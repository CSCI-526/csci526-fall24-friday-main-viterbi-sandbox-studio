using UnityEngine;
using UnityEngine.UI;

public class TextEnhancer : MonoBehaviour
{
    public Text uiText;

    void Start()
    {
        if (uiText != null)
        {
            // 调整字体大小
            uiText.fontSize = 24;

            // 改变字体颜色为白色
            uiText.color = Color.white;

            // 设置字体样式为粗体
            uiText.fontStyle = FontStyle.Bold;

            // 添加阴影组件
            Shadow shadow = uiText.gameObject.GetComponent<Shadow>() ?? uiText.gameObject.AddComponent<Shadow>();
            shadow.effectColor = Color.black;
            shadow.effectDistance = new Vector2(2f, -2f);

            // 添加轮廓组件
            Outline outline = uiText.gameObject.GetComponent<Outline>() ?? uiText.gameObject.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(1f, -1f);
        }
        else
        {
            Debug.LogError("UI Text component is not attached to the script.");
        }
    }
}