using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextOnKeyPress : MonoBehaviour
{
    public TextMeshPro textMeshProCross;
    public TextMeshPro textMeshProCircle;

    public GameObject arrow1;
    public GameObject arrow2;

    private void Start()
    {
        textMeshProCross.gameObject.SetActive(true);
        textMeshProCircle.gameObject.SetActive(false);
        arrow1.SetActive(false);
        arrow2.SetActive(false);
    }

    void Update()
    {
        // Check if either Left Shift or Right Shift is being pressed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            textMeshProCross.gameObject.SetActive(false);
            textMeshProCircle.gameObject.SetActive(true);
            arrow1.SetActive(true);
            arrow2.SetActive(true);
        }
        else
        {
            textMeshProCross.gameObject.SetActive(true);
            textMeshProCircle.gameObject.SetActive(false);
            arrow1.SetActive(false);
            arrow2.SetActive(false);
        }
    }
}
