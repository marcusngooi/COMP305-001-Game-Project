using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static int jumpsLeft = 5;

    public TextMeshProUGUI jumpsText;

    void Start()
    {
        jumpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        jumpsText.text = "Jumps Left: " + jumpsLeft;
    }

}
