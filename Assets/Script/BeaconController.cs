using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    public GameObject beaconFire1; // The first beacon fire (mandatory)
    public GameObject beaconFire2; // The second beacon fire (optional)
    public Vector3 targetScale = new Vector3(1, 1, 1); // The target scale for the fire
    public float scaleDuration = 0.5f; // Time it takes to scale up

    // Start is called before the first frame update
    void Start()
    {
        // Initialize both fires to a scale of zero
        if (beaconFire1 != null)
        {
            beaconFire1.transform.localScale = Vector3.zero;
        }

        if (beaconFire2 != null)
        {
            beaconFire2.transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optional update logic if needed
    }

    // Function to light up both fires
    public void LightUp()
    {
        if (beaconFire1 != null)
        {
            StopCoroutine(nameof(ScaleFire)); // Ensure no duplicate coroutines run
            StartCoroutine(ScaleFire(beaconFire1));
        }

        if (beaconFire2 != null)
        {
            StopCoroutine(nameof(ScaleFire)); // Ensure no duplicate coroutines run
            StartCoroutine(ScaleFire(beaconFire2));
        }
    }

    // Coroutine to scale a fire from (0, 0, 0) to the target size
    private System.Collections.IEnumerator ScaleFire(GameObject fire)
    {
        Vector3 initialScale = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            fire.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
            yield return null;
        }

        fire.transform.localScale = targetScale; // Ensure final scale is the target scale
    }
}
