using UnityEngine;

public class ExpandShrinkAnimation : MonoBehaviour
{
    public float maxScale = 1.5f; // Maximum scale value
    public float minScale = 0.5f; // Minimum scale value
    public float speed = 1f;      // Speed of the animation

    private Vector3 initialScale;
    private float time;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the scale factor
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(time * speed, 1));
        // Apply scale on x and y axes, keep z axis unchanged
        transform.localScale = new Vector3(scale * initialScale.x, initialScale.y, scale * initialScale.z);
        
        // Update time for smooth scaling
        time += Time.deltaTime;
    }
}
