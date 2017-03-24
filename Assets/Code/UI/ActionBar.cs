using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ActionBar : MonoBehaviour
{
    [Serializable]
    public class ActionBarOrder
    {
        public ActionKey Key;
        public ActionType Type;
    }

    [Serializable]
    public class ActionInfo
    {
        public ActionType Type;
        public Sprite Icon;
        [Range(0, 10)]
        public int Cost;
    }

    public ActionInfo[] Actions;
    public ActionBarOrder[] Order;
    public ActionBarButton[] Buttons;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("ActionBarOrder"))
        {
            var StoredOrder = JsonUtility.FromJson<ActionBarOrder[]>(PlayerPrefs.GetString("ActionBarOrder"));
            SetupActionBar(StoredOrder);
        }
        else
            SetupActionBar(Order);
    }

    public void SetupActionBar(ActionBarOrder[] order)
    {
        foreach (var o in order)
        {
            var a = Actions.Single(i => i.Type == o.Type);
            var b = Buttons.Single(i => i.Key == o.Key);
            b.Setup(a.Cost, a.Icon);
        }
    }

    public ActionInfo ActionOnKey(ActionKey key)
    {
        var type = Order.First(i => i.Key == key).Type;
        return Actions.First(i => i.Type == type);
    }

    // Update is called once per frame
    void Update()
    {

    }



}
