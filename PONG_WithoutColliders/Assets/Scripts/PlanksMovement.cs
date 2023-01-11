using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanksMovement : MonoBehaviour
{
    [SerializeField] Transform leftPlank, rightPlank;
    [SerializeField] float speed;

    float leftMove,rightMove;
    Vector2 screenBonds;
    private void Awake()
    {
        screenBonds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        screenBonds.y -= leftPlank.localScale.y / 2.0f;

        leftPlank.transform.position = new Vector2(-screenBonds.x+1f,0f);
        rightPlank.transform.position = new Vector2(screenBonds.x-1f,0f);
    }

    
    private void Update()
    {
        leftMove = Input.GetAxis("Horizontal_Left")*speed;
        rightMove = Input.GetAxis("Horizontal_Right")*speed;

                
        leftPlank.Translate(0f, leftMove, 0f); 
        rightPlank.Translate(0f, rightMove, 0f);

        leftPlank.position = new Vector2(leftPlank.position.x,
            Mathf.Clamp(leftPlank.position.y, -screenBonds.y, screenBonds.y));
        rightPlank.position = new Vector2(rightPlank.position.x,
            Mathf.Clamp(rightPlank.position.y, -screenBonds.y, screenBonds.y));

      
    }
}
