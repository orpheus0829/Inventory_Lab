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
            }
            Debug.Log("½øÈë½»»¥·¶Î§");
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
            }
        }
    }
}
