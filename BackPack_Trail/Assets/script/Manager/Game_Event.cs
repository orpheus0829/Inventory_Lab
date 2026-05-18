using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Event : MonoBehaviour
{
    public static Game_Event instance { private set; get; }
    //刷新玩家金币数
    public event Action<int> Player_Coin;
    //商店买卖
    public event Action<Item_Data> Buy_Item;
    public event Action<bool> Real_Buy_Item;
    public event Action<Item_Data> Sell_Item;
    public event Action<bool> Real_Sell_Item;
    //生成买卖按钮
    public event Action<Item_Data, int> Spawn_Buy_Button;
    public event Action<Item_Data, int> Spawn_Sell_Button;
    //卖出扣除物品
    public event Action<Item_Data> Remove_Sold_Good;
    //通讯玩家剩余某物品数量
    public Func<Item_Data, int> Last_Item_By_ID;
    //初始化商店
    public event Action<bool> Init_Store;
    //刷新商店
    public event Action Refresh_Buy;
    public event Action Refresh_Sell;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #region 商店买卖
    public void Send_BuyItem(Item_Data item_Data)
    {
        Debug.Log("【事件发送】准备购买物品：" + item_Data.item_name);
        Debug.Log("【事件监听数量】Buy_Item 监听者：" + (Buy_Item?.GetInvocationList().Length ?? 0));
        Buy_Item?.Invoke(item_Data);
    }
    public void Send_Real_BuyItem(bool Really)
    {
        Real_Buy_Item?.Invoke(Really);
    }
    public void Send_SellItem(Item_Data item_Data)
    {
        Sell_Item?.Invoke(item_Data);
    }
    public void Send_Real_SellItem(bool Really)
    {
        Real_Sell_Item?.Invoke(Really);
    }
    #endregion
    #region 生成买卖按钮
    public void Send_Spawn_Buy_Button(Item_Data item,int Store_Num)
    {
        Spawn_Buy_Button?.Invoke(item, Store_Num);
    }
    public void Send_Spawn_Sell_Button(Item_Data item, int Store_Num)
    {
        Spawn_Sell_Button?.Invoke(item, Store_Num);
    }
    #endregion
    #region 初始化和刷新商店
    public void Init_Store_Panel(bool is_ready)
    {
        Init_Store?.Invoke(is_ready);
    }
    public void Refresh_Buy_List()
    {
        Refresh_Buy?.Invoke();
    }
    public void Refresh_Sell_List()
    {
        Refresh_Sell?.Invoke();
    }
    #endregion
    #region 刷新玩家资产
    public void Refresh_Player_Coin(int cost)
    {
        Player_Coin?.Invoke(cost);
    }
    public void Remove_Sold_Item(Item_Data item)
    {
        Remove_Sold_Good?.Invoke(item);
    }
    #endregion
    #region 了解玩家剩余可卖出物品数量
    public int Last_Item_For_Sell(Item_Data item)
    {
        return Last_Item_By_ID?.Invoke(item) ?? 0;
    }
    #endregion
}
