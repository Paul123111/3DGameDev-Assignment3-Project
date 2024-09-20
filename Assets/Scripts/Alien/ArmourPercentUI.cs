using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmourPercentUI : MonoBehaviour
{

    TextMeshProUGUI percent;

    // Start is called before the first frame update
    void Start()
    {
        percent = GetComponent<TextMeshProUGUI>();
    }

    public void SetPercent(float remaining) {
        percent.text = ((remaining).ToString() + "%");
    }

}
