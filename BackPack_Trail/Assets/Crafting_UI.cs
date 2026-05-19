using Opsive.OmniAnimation.Packs.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_UI : MonoBehaviour
{
    public GameObject Crafting_Button;
    public RectTransform content;
    public List<GameObject> CraftButton_List;

    public int Space_Between;
    public int original_offsetx;
    public int original_offsety;
    public void OnEnable()
    {
        Game_Event.instance.Spawn_Crafting_Button += Crafting_Spawner;
    }
    public void OnDisable()
    {
        Game_Event.instance.Spawn_Crafting_Button -= Crafting_Spawner;
    }
    public void Crafting_Spawner(Crafting_SO craft)
    {
        //Éú³É°´Å¥
    }
}
