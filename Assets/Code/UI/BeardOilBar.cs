using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BeardOilBar : MonoBehaviour
{
    public Text AmountText;
    public int StartValue;

    private float FillRate;
    [SerializeField]
    private Slider OilBar;
    [SerializeField]
    private Slider OverlayBar;

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

        OilBar.value = StartValue;
        OverlayBar.value = StartValue;

        AmountText.text = StartValue.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        OilAmount = OverlayBar.value += FillRate * Time.deltaTime;
        var target = Mathf.FloorToInt(OverlayBar.value);
        OilBar.value = iTween.FloatUpdate(OilBar.value, target, 20);
        AmountText.text = target.ToString();
    }
}
