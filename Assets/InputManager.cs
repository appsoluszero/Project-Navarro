using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool rawInput;
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private KeyCode walkUp;
    [SerializeField] private KeyCode walkDown;
    [SerializeField] private KeyCode walkLeft;
    [SerializeField] private KeyCode walkRight;
    float timeCounterY = 0f;
    float timeCounterX = 0f;
    float axisX = 0f;
    float axisY = 0f;

    void FixedUpdate()
    {
        if(!rawInput)
        {
            CalculateAxisX();
            CalculateAxisY();
        }
        else
        {
            timeCounterX = 0f;
            timeCounterY = 0f;
            CalculateRawAxis();
        }
        GetComponent<IMovementMethod>().SetMovement(new Vector2(axisX, axisY).normalized);
    }
    void CalculateRawAxis()
    {
        axisX = 0f;
        axisY = 0f;
        if(Input.GetKey(walkUp)) axisY = 1f;
        else if(Input.GetKey(walkDown)) axisY = -1f;
        if(Input.GetKey(walkRight)) axisX = 1f;
        else if(Input.GetKey(walkLeft)) axisX = -1f;
    }
    void CalculateAxisY()
    {
        if(Input.GetKey(walkUp))
        {
            timeCounterY += Time.fixedDeltaTime;
            timeCounterY = Mathf.Clamp(timeCounterY, 0f, speed);
            axisY = CalculateInput(timeCounterY, 0f, 1f, speed);
        }
        else if(Input.GetKey(walkDown))
        {
            timeCounterY += Time.fixedDeltaTime;
            timeCounterY = Mathf.Clamp(timeCounterY, 0f, speed);
            axisY = CalculateInput(timeCounterY, 0f, -1f, speed);
        }
        else if(axisY > 0f)
        {
            timeCounterY -= Time.fixedDeltaTime;
            timeCounterY = Mathf.Clamp(timeCounterY, 0f, gravity);
            axisY = CalculateInput(timeCounterY, 0f, 1f, gravity);
        }
        else if(axisY < 0f)
        {
            timeCounterY -= Time.fixedDeltaTime;
            timeCounterY = Mathf.Clamp(timeCounterY, 0f, gravity);
            axisY = CalculateInput(timeCounterY, 0f, -1f, gravity);
        }
    }
    void CalculateAxisX()
    {
        if(Input.GetKey(walkRight))
        {
            timeCounterX += Time.fixedDeltaTime;
            timeCounterX = Mathf.Clamp(timeCounterX, 0f, speed);
            axisX = CalculateInput(timeCounterX, 0f, 1f, speed);
        }
        else if(Input.GetKey(walkLeft))
        {
            timeCounterX += Time.fixedDeltaTime;
            timeCounterX = Mathf.Clamp(timeCounterX, 0f, speed);
            axisX = CalculateInput(timeCounterX, 0f, -1f, speed);
        }
        else if(axisX > 0f)
        {
            timeCounterX -= Time.fixedDeltaTime;
            timeCounterX = Mathf.Clamp(timeCounterX, 0f, gravity);
            axisX = CalculateInput(timeCounterX, 0f, 1f, gravity);
        }
        else if(axisX < 0f)
        {
            timeCounterX -= Time.fixedDeltaTime;
            timeCounterX = Mathf.Clamp(timeCounterX, 0f, gravity);
            axisX = CalculateInput(timeCounterX, 0f, -1f, gravity);
        }
    }
    float CalculateInput(float currentTime, float start, float stop, float t)
    {
        float percent = currentTime / t;
        return Mathf.Lerp(start, stop, percent);
    }
    
}
