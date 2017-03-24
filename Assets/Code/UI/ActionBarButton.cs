using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionBarButton : MonoBehaviour
{
    public Image IconLoading;
    public Image IconBackground;
    public int Cost { get; private set; }

    public ActionKey Key;
    private Text CostText;
    private BeardOilBar bar;

    // Use this for initialization
    void Awake()
    {
        CostText = GetComponentInChildren<Text>();
        bar = FindObjectOfType<BeardOilBar>();
    }

    public void Setup(int cost, Sprite icon)
    {
        Cost = cost;
        CostText.text = cost.ToString();
        IconLoading.sprite = icon;
        IconBackground.sprite = icon;
    }

    // Update is called once per frame
    void Update()
    {
        IconLoading.fillAmount = Mathf.Clamp(BeardOilBar.OilAmount / Cost, 0, 1);
    }
}
