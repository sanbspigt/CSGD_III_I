using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 direction;
    float radius;
    [SerializeField] Transform leftPlank, rightPlank;

    float angle;
    Vector2 screenBonds;
    bool canPlay = true;
    private void Start()
    {
        screenBonds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SetDirection();
        radius = transform.localScale.x / 2.0f;
    }
    Coroutine routine;
    private void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime);

        DetectCollisionForWalls();
        DetectCollisionForPlankRight();
        DetectCollisionForPlankLeft();
    }

    void DetectCollisionForPlankRight()
    { 
        float widthofPlank = leftPlank.transform.localScale.x/2.0f;
        float heightofPlank = leftPlank.transform.localScale.y/2.0f;

        if (transform.position.x+radius < rightPlank.transform.position.x - widthofPlank ||
            transform.position.x-radius > rightPlank.transform.position.x + widthofPlank ||
            transform.position.y + radius < rightPlank.transform.position.y - heightofPlank ||
            transform.position.y - radius > rightPlank.transform.position.y + heightofPlank)
        {
            direction.x *= -1;
        }

        //if ()
        //{
        //    direction.y *= -1;
        //}
    }

    void DetectCollisionForPlankLeft()
    {
        float widthofPlank = leftPlank.transform.localScale.x / 2.0f;
        float heightofPlank = leftPlank.transform.localScale.y / 2.0f;

        if (transform.position.x+radius < leftPlank.transform.position.x - widthofPlank ||
            transform.position.x-radius > leftPlank.transform.position.x + widthofPlank || 
            transform.position.y + radius < leftPlank.transform.position.y - heightofPlank ||
            transform.position.y - radius > leftPlank.transform.position.y + heightofPlank)
        {
            direction.x *= -1;
        }

        //if ()
        //{
        //    direction.y *= -1;
        //}
    }

    void DetectCollisionForWalls()
    {
        if (transform.position.y >= screenBonds.y || transform.position.y <= -screenBonds.y)
            direction.y *= -1;

        if (transform.position.x >= screenBonds.x || transform.position.x <= -screenBonds.x)
        {
            Debug.Log("GAME_OVER");

            if (routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(ResetGame());
        }
    }

    void SetDirection()
    {
        angle = Random.Range(30,180) + Random.Range(180,360);
        direction = new Vector2(Mathf.Sin(angle),Mathf.Cos(angle));
    }

    IEnumerator ResetGame()
    {
        canPlay = false;
        direction = Vector2.zero;
        transform.position = Vector2.zero;
        yield return new WaitForSeconds(2.0f);    
        
        SetDirection();
        canPlay = true;
    }
}
