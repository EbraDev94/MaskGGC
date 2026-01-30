using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    
    private Vector2 StartTouchPosition;
    private Vector2 EndTouchPosition;

   [SerializeField] private float SensitivityMove;
   [SerializeField] private float SensitivityAct;

   void Update()
   {
       if (GetComponent<PlayerController>().isDead)
           return;

       if (GetComponent<PlayerController>().move)
       {
           if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
           {
               StartTouchPosition = Input.GetTouch(0).position;
           }

           if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
           {
               EndTouchPosition = Input.GetTouch(0).position;
               print(StartTouchPosition.x - EndTouchPosition.x);
               print(StartTouchPosition.y - EndTouchPosition.y);
               if (EndTouchPosition.x < StartTouchPosition.x)
               {
                   if ((StartTouchPosition.x - EndTouchPosition.x) > SensitivityMove)
                       Left();
               }
               else if (EndTouchPosition.x > StartTouchPosition.x)
               {
                   if ((EndTouchPosition.x - StartTouchPosition.x) > SensitivityMove)
                       Right();
               }

               if (EndTouchPosition.y < StartTouchPosition.y)
               {
                   if ((StartTouchPosition.y - EndTouchPosition.y) > SensitivityAct)
                       Down();
               }
               else if (EndTouchPosition.y > StartTouchPosition.y)
               {
                   if ((EndTouchPosition.y - StartTouchPosition.y) > SensitivityAct)
                       UP();
               }
           }
       }
   }

   void Left()
    {
        print("Left");
        GetComponent<PlayerController>().MoveLeft();
    }

    void Right()
    {
        print("Right");
        GetComponent<PlayerController>().MoveRight();
    }
    
    void UP()
    {
        print("UP");
        GetComponent<PlayerController>().Jump();
    }
    
    void Down()
    {
        print("Down");
        GetComponent<PlayerController>().Slide();
    }
}
