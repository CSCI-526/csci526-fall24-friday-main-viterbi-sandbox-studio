using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextOnKeyPress : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    // Define colors
    private Color shiftPressedColor = new Color(44f / 255f, 223f / 255f, 15f / 255f);
    private Color defaultColor = Color.red;

    void Update()
    {
        // Check if either Left Shift or Right Shift is being pressed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            textMeshPro.text = "O";
            textMeshPro.color = shiftPressedColor;
        }
        else
        {
            textMeshPro.text = "X";
            textMeshPro.color = defaultColor;
        }
    }
}
