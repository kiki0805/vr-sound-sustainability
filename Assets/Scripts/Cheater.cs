using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheater : MonoBehaviour
{
    public TMP_Text tooltip;
    private int referCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void SetTooltip(string tooltipStr) {
    //     tooltipStr = tooltipStr.Replace("\\n", "\r\n");
    //     tooltip.text = tooltipStr;
    //     referCount ++;
    // }

    public void SetTooltip(Ingredient ingredient) {
        tooltip.text = ingredient.GetTipString();
        referCount ++;
    }

    public void ClearTooltip() {
        referCount --;
        if (referCount == 0) {
            tooltip.text = "";
        }
    }
}
