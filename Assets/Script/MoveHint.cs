using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxMovementTrigger : MonoBehaviour
{
    public Text uiTip;  // UI文本组件
    private Vector3 startPosition;
    private bool isTipShown = false;
    public float significantMoveThreshold = 1.0f;  // 显著移动的阈值

    void Start()
    {
        startPosition = transform.position;  // 记录初始位置
        uiTip.canvasRenderer.SetAlpha(0.0f);  // 初始化UI文本透明度为0
    }

    void Update()
    {
        // 检测箱子是否显著移动
        if (!isTipShown && (transform.position - startPosition).magnitude > significantMoveThreshold)
        {
            ShowTip();
            isTipShown = true;  // 标记提示已显示
        }
    }

    void ShowTip()
    {
        uiTip.gameObject.SetActive(true);
        uiTip.canvasRenderer.SetAlpha(1.0f);  // 设置文本透明度为全不透明
        uiTip.CrossFadeAlpha(0.0f, 3.0f, false);  // 在3秒内文本逐渐透明
        StartCoroutine(DeactivateTip());
    }

    IEnumerator DeactivateTip()
    {
        yield return new WaitForSeconds(3.0f);
        uiTip.gameObject.SetActive(false);
    }
}