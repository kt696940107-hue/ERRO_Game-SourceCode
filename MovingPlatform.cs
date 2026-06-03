using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;        // 平台移動速度
    public float horizontalRange = 3f;  // 左右移動範圍
    public float verticalRange = 3f;    // 上下移動範圍

    private Vector2 initialPosition;
    private bool movingRight = true;
    private bool movingUp = true;

    void Start()
    {
        // 儲存初始位置
        initialPosition = transform.position;
    }

    void Update()
    {
        // 控制平台左右移動
        MoveHorizontal();

        // 控制平台上下移動
        MoveVertical();
    }

    void MoveHorizontal()
    {
        if (movingRight)
        {
            // 計算新的水平位置
            float newX = initialPosition.x + horizontalRange;
            float newPositionX = Mathf.Lerp(transform.position.x, newX, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

            // 檢查是否到達範圍的右側，以改變移動方向
            if (Mathf.Approximately(newPositionX, newX))
            {
                movingRight = false;
            }
        }
        else
        {
            // 計算新的水平位置
            float newX = initialPosition.x - horizontalRange;
            float newPositionX = Mathf.Lerp(transform.position.x, newX, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

            // 檢查是否到達範圍的左側，以改變移動方向
            if (Mathf.Approximately(newPositionX, newX))
            {
                movingRight = true;
            }
        }
    }

    void MoveVertical()
    {
        if (movingUp)
        {
            // 計算新的垂直位置
            float newY = initialPosition.y + verticalRange;
            float newPositionY = Mathf.Lerp(transform.position.y, newY, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);

            // 檢查是否到達範圍的頂部，以改變移動方向
            if (Mathf.Approximately(newPositionY, newY))
            {
                movingUp = false;
            }
        }
        else
        {
            // 計算新的垂直位置
            float newY = initialPosition.y - verticalRange;
            float newPositionY = Mathf.Lerp(transform.position.y, newY, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);

            // 檢查是否到達範圍的底部，以改變移動方向
            if (Mathf.Approximately(newPositionY, newY))
            {
                movingUp = true;
            }
        }
    }
}
