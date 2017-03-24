using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BeardOilBar : MonoBehaviour
{
    public int StartValue;

    private float FillRate;
    private Slider OilBar;
    private Slider OverlayBar;
    private Text AmountText;

    public static float OilAmount { get; private set; }

    public void UseOil(int amount)
    {
        OilBar.value -= amount;
        OverlayBar.value -= amount;
    }

    // Use this for initialization
    void Start()
    {
        FillRate = GameController.Instance.OilBarFillRate;
        AmountText = GetComponentInChildren<Text>();

        foreach (var bar in GetComponentsInChildren<Slider>())
        {
            bar.value = StartValue;
            if (bar.wholeNumbers) OilBar = bar;
            else OverlayBar = bar;
        }
        AmountText.text = StartValue.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        OilAmount = OverlayBar.value += FillRate * Time.deltaTime;
        OilBar.value = Mathf.FloorToInt(OverlayBar.value);
        AmountText.text = OilBar.value.ToString();
    }
}
