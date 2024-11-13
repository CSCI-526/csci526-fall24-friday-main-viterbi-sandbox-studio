using UnityEngine;

public class PActivator : MonoBehaviour
{
    public EController elevatorController; // 引用ElevatorController
    private bool hasActivated = false; // 用于记录是否已激活过平台

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否为玩家触发
        if (other.CompareTag("Player") && !hasActivated)
        {
            if (elevatorController != null)
            {
                elevatorController.StartMovement(); // 启动平台移动
                hasActivated = true; // 设置为已激活，防止再次触发
                Debug.Log("Platform activated by Player.");
            }
            else
            {
                Debug.LogWarning("ElevatorController reference is missing.");
            }
        }
    }
}
