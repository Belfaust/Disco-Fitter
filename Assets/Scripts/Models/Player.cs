using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private int currentInput;

   public int input
   {
      get => currentInput;
      set { 
      currentInput = value;
      GameController.instance.InputCheck(value);
      }
}
   private void InputSystem()
   {
      if (Input.GetKeyDown(KeyCode.Q))
      {
         input = 1;
      }
      if (Input.GetKeyDown(KeyCode.W))
      {
         input = 2;
      }
      if (Input.GetKeyDown(KeyCode.E))
      {
         input = 3;
      }
      if (Input.GetKeyDown(KeyCode.R))
      {
         input = 4;
      }
   }

   private void Update()
   {
      InputSystem();
   }
}
