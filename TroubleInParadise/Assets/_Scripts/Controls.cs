using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] int playerNum;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode attack;
    public XboxCtrlrInput.XboxController controller;
    public bool hasController = false;

    private void Awake()
    {
       if(playerNum == 1)
       {
           left = KeyCode.A;
           right = KeyCode.D;
           up = KeyCode.W;
           down = KeyCode.S;

           attack = KeyCode.Space;

           if (XboxCtrlrInput.XCI.IsPluggedIn(1))
           {
               controller = XboxCtrlrInput.XboxController.First;
               hasController = true;
           }
       }

       if(playerNum == 2)
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;

            attack = KeyCode.RightControl;

            if (XboxCtrlrInput.XCI.IsPluggedIn(2))
            {
                controller = XboxCtrlrInput.XboxController.Second;
                hasController = true;
            }
        }
    }
}
