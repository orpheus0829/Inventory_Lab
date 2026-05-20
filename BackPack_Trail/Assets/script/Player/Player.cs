using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Player : MonoBehaviour
{
    public PlayerInput playerInput;

    [Header("启动资产")]
    public int Start_Money;

    [Header("背包")]
    public GameObject Bag_Panel;
    public Player_Bag bag;

    [Header("相机")]
    public Vector3 moveDir;

    [Header("移动")]
    public Vector3 InputMove;
    public bool Is_Running;
    public float MoveSpeed;
    public float RunSpeed;
    public float Speed;

    [Header("引用")]
    public Rigidbody rb;
    public Animator am;

    [Header("交互")]
    public bool Can_Interact;
    public GameObject Trader_Panel;
    public GameObject Buy_Panel;
    public GameObject Sell_Panel;

    [Header("合成")]
    public GameObject Craft_Panel;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        am = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        bag = GetComponent<Player_Bag>();

        if (PlayerPrefs.GetInt("Money",0) <= 0)
        {
            PlayerPrefs.SetInt("Money", Start_Money);
        }
        PlayerPrefs.Save();
    }
    public void OnEnable()
    {
        Game_Event.instance.Init_Store += Set_StorePanel;
    }
    public void OnDisable()
    {
        Game_Event.instance.Init_Store -= Set_StorePanel;
    }
    public void Update()
    {
        // 保留画线，方便以后自己测试用
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * 2f, Color.red);
    }

    public void FixedUpdate()
    {
        Move_Follow_Camera();

        Speed = Is_Running ? RunSpeed : MoveSpeed;
        am.SetBool("Is_Run", InputMove != Vector3.zero && Speed == RunSpeed);
        am.SetBool("Is_Move", InputMove != Vector3.zero);
        float VerticalVelocity = rb.velocity.y;
        Vector3 HorizontalVelocity = moveDir * Speed;

        rb.velocity = new Vector3(HorizontalVelocity.x, VerticalVelocity, HorizontalVelocity.z);
    }

    public void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector3>();
        InputMove = moveInput;
    }
    public void OnRun(InputValue value)
    {
        Is_Running = value.isPressed;
    }
    public void OnBackPack(InputValue value)
    {
        if (value.isPressed && !Trader_Panel.activeSelf && !Craft_Panel.activeSelf)
        {
            if (!Bag_Panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Bag_Panel.SetActive(true);
                Introduction_Mrg.instance.gameObject.SetActive(false);
                bag.Load_Data("Bag_Data");
                bag.ReClean_Bag_Display();
                bag.Refresh_Bag_Display();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                bag.Save_Bag("Bag_Data");
                bag.ReClean_Bag_Display();
                Bag_Panel.SetActive(false);
            }
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            // 跳跃逻辑空位，以后自己写

            //暂时用作背包清空按键，后续改回去
            bag.Throw_And_Delete_All();
        }
    }
    public void OnDrop_Item(InputValue value)
    {
        if (value.isPressed && bag.IsDragging)
        {
            bag.currentDraggingItem.Throw_Item();
            bag.ReClean_Bag_Display();
            bag.Refresh_Bag_Display();
        }
    }
    public void OnCraft(InputValue value)
    {
        if (value.isPressed && !Trader_Panel.activeSelf && !Bag_Panel.activeSelf)
        {
            if (!Craft_Panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Craft_Panel.SetActive(true);
                Game_Event.instance.Init_Crafting();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Craft_Panel.SetActive(false);
            }
        }
    }
    public void OnInteract(InputValue value)
    {
        if (value.isPressed && Can_Interact && !Bag_Panel.activeSelf)
        {
            if (!Trader_Panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Trader_Panel.gameObject.SetActive(true);
                Buy_Panel.SetActive(true);
                Sell_Panel.SetActive(false);

                Game_Event.instance.Refresh_Buy_List();
                Game_Event.instance.Refresh_Sell_List();
                Game_Event.instance.Init_Store_Panel(true);
                //Game_Event.instance.Current_Trader.Refresh_B();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Trader_Panel.gameObject.SetActive(false);
                Buy_Panel.SetActive(false);
                Sell_Panel.SetActive(false);
            }
        }
    }
    public void Move_Follow_Camera()
    {
        Transform cam = Camera.main.transform;
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        moveDir = camForward * InputMove.z + camRight * InputMove.x;

        if (moveDir.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 15f * Time.fixedDeltaTime);
        }
    }
    public void Set_StorePanel(bool Is_Ready)
    {
        Trader_Panel.SetActive(Is_Ready);
    }
}