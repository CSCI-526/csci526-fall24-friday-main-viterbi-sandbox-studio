using UnityEngine;

public class PActivator : MonoBehaviour
{
    public EController elevatorController; // ����ElevatorController
    private bool hasActivated = false; // ���ڼ�¼�Ƿ��Ѽ����ƽ̨

    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ�Ϊ��Ҵ���
        if (other.CompareTag("Player") && !hasActivated)
        {
            if (elevatorController != null)
            {
                elevatorController.StartMovement(); // ����ƽ̨�ƶ�
                hasActivated = true; // ����Ϊ�Ѽ����ֹ�ٴδ���
                Debug.Log("Platform activated by Player.");
            }
            else
            {
                Debug.LogWarning("ElevatorController reference is missing.");
            }
        }
    }
}
