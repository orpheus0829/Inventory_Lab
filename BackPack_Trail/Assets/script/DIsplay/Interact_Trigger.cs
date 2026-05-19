using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Trigger : MonoBehaviour
{
    public Player pl;
    public SphereCollider sc;
    public GameObject Interact_Button;
    public void Awake()
    {
        sc = GetComponent<SphereCollider>();
        pl = GetComponentInParent<Player>();
        if (Interact_Button.activeSelf)
        {
            Interact_Button.gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!Interact_Button.activeSelf)
            {
                pl.Can_Interact = true;
                Interact_Button.gameObject.SetActive(true);
                Trader targetTrader = other.GetComponent<Trader>();
                Game_Event.instance.Current_Trader = targetTrader;

                //Debug.Log($"绑定前Init_Store监听数量：{Game_Event.instance.Get_InitStore_ListenerCount()}");
                Game_Event.instance.Init_Store -= targetTrader.OnShopOpen;
                Game_Event.instance.Init_Store += targetTrader.OnShopOpen;
                //Debug.Log($"绑定后Init_Store监听数量：{Game_Event.instance.Get_InitStore_ListenerCount()}");
                //targetTrader.Reset_Shop_Init();
                //Game_Event.instance.Refresh_Buy_List();
                //Game_Event.instance.Refresh_Sell_List();
                //Game_Event.instance.Init_Store_Panel(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (Interact_Button.activeSelf)
            {
                pl.Can_Interact = false;
                Interact_Button.gameObject.SetActive(false);
                Trader trader = other.GetComponent<Trader>();
                Game_Event.instance.Init_Store -= trader.OnShopOpen;
                Game_Event.instance.Current_Trader = null;
            }
        }
    }
}
