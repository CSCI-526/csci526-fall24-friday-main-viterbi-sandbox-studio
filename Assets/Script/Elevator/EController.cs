using UnityEngine;

public class EController : MonoBehaviour
{
    public float speed = 2f;         // ƽ̨�ƶ��ٶ�
    public float lowerHeight = 0f;   // ƽ̨�����λ��
    public float upperHeight = 10f;  // ƽ̨�����λ��

    private bool isMoving = false;   // ����ƽ̨�Ƿ��ƶ�
    private bool movingUp = true;    // ����ƽ̨���ƶ�����

    // ��ʼƽ̨���ƶ�
    public void StartMovement()
    {
        isMoving = true;
        Debug.Log("Elevator movement started.");
    }

    private void Update()
    {
        if (isMoving)
        {
            if (movingUp)
            {
                if (transform.position.y < upperHeight)
                {
                    transform.position += Vector3.up * speed * Time.deltaTime;
                }
                else
                {
                    movingUp = false;
                }
            }
            else
            {
                if (transform.position.y > lowerHeight)
                {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                }
                else
                {
                    movingUp = true;
                }
            }
        }
    }
}
