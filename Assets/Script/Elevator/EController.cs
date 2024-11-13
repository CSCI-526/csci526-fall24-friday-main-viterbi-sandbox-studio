using UnityEngine;

public class EController : MonoBehaviour
{
    public float speed = 2f;         // 平台移动速度
    public float lowerHeight = 0f;   // 平台的最低位置
    public float upperHeight = 10f;  // 平台的最高位置

    private bool isMoving = false;   // 控制平台是否移动
    private bool movingUp = true;    // 控制平台的移动方向

    // 开始平台的移动
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
