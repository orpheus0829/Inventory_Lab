using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Panel_Mgr : Base_mgr<Panel_Mgr>
{
    [Header("面板")]
    public BasePanel BagPanel;
    public BasePanel TraderPanel;
    public BasePanel CraftPanel;
    public BasePanel InteractPanel;

    public BasePanel BuyPanel;
    public BasePanel SellPanel;
    [Header("列表")]
    public List<BasePanel> PanelList;
    protected override void Awake()
    {
        base.Awake();
        if (instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        FindAllPanel();
    }
    public void FindAllPanel()
    {
        PanelList.Clear();
        FieldInfo[] fields = typeof(Panel_Mgr).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(BasePanel))
            {
                BasePanel basePanel = (BasePanel)field.GetValue(this);
                if (basePanel != null)
                {
                    PanelList.Add(basePanel);
                }
                else
                {
                    Debug.Log("为空值");
                }
            }
        }
    }
    public void HideAllPanel()
    {
        foreach(var panel in PanelList)
        {
            panel?.HidePanel();
        }
    }
    public void OpenPanel(BasePanel panel)
    {
        HideAllPanel();
        panel.ShowPanel();
    }
    public void OpenTraderBuyPanel()
    {
        HideAllPanel();
        TraderPanel.ShowPanel();
        BuyPanel.ShowPanel();
    }
    public void OpenTraderSellPanel()
    {
        HideAllPanel();
        TraderPanel.ShowPanel();
        SellPanel.ShowPanel();
    }
    public void Control_InteractPanel(bool open)
    {
        InteractPanel.gameObject.SetActive(open);
    }
    public bool IsPanelVisible(BasePanel panel)
    {
        if (panel == null)
        {
            return false;
        }
        return panel.IsVisible();
    }
}
