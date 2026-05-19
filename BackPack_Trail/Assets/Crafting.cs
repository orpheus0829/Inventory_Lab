using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public AllCrafting_Maps maps;
    public int Map_Nums;
    public void Awake()
    {
        Map_Nums = maps.Crafting_Maps.Count;
        foreach(var i in maps.Crafting_Maps)
        {
            Create_Crafting_Single_Map(i);
        }
    }
    public void Create_Crafting_Single_Map(Crafting_SO craft)
    {
        Game_Event.instance.Spawn_Craft_Button(craft);
        Debug.Log("生成了" + craft.Map_Name + "合成表");
    }
}
