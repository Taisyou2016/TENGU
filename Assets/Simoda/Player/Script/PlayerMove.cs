﻿using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 4.0f; //歩くスピード（メートル/秒）
    public float gravity = 10.0f; //重力加速度

    private CharacterController controller;
    private float velocityY = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        //y成分を無視する
        forward.y = 0;
        //正規化（長さを1にする）
        forward.Normalize();

        Vector3 velocity =
            forward * Input.GetAxis("Vertical") * walkSpeed
            + Camera.main.transform.right * Input.GetAxis("Horizontal") * walkSpeed;

        //Vector3 velocity =
        //    Vector3.forward * Input.GetAxis("Vertical") * walkSpeed
        //    + Vector3.right * Input.GetAxis("Horizontal") * walkSpeed;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

        velocityY -= gravity * Time.deltaTime;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

        //キャラクターの向きを変える
        velocity.y = 0;
        if (velocity.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(velocity);

            //transform.LookAt(transform.position + velocity); //上と同じ
        }
    }
}