using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_StoreMode : MonoBehaviour
{
    public Button Change_Button;
    public GameObject Buy;
    public GameObject Sell;
    public void Awake()
    {
        Change_Button = GetComponent<Button>();

        Change_Button.onClick.RemoveAllListeners();
        Change_Button.onClick.AddListener(() => Click_Chnage());
    }
    public void Click_Chnage()
    {
        Buy.SetActive(Sell.activeSelf);
        Sell.SetActive(!Sell.activeSelf);
    }
}
