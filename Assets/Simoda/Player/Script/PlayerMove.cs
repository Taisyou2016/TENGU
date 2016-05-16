﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerMoveState;
using System;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 4.0f; //歩くスピード（メートル/秒）
    public float lockOnRotateSpeed = 45.0f; //ロックオンしているときの横移動
    public float gravity = 10.0f; //重力加速度
    public float jampPower = 10.0f; //ジャンプするパワー
    public float knockBackPower = 0.0f; //KnockBackLargeの時吹き飛ぶパワー
    public float windPower = 0.0f; //風のパワー
    public Vector3 windDirection; //風の方向
    public bool previosGroundHit = false; //ひとつ前の地面に当たっているかどうかの判定
    public bool currentGroundHit = false; //現在の地面に当たっているかどうかの判定

    private CharacterController controller;
    private GameObject cameraController;
    private Vector3 cameraForward;
    private Vector3 velocity;
    private float velocityY = 0;
    private bool jampState = false;

    private List<GameObject> lockEnemyList = new List<GameObject>();
    private GameObject lockEnemy;
    private bool lockOn = false;



    public StateProcessor stateProcessor = new StateProcessor();
    public PlayerMoveStateDefault stateDefault = new PlayerMoveStateDefault();
    public PlayerMoveStateLockOn stateLockOn = new PlayerMoveStateLockOn();
    public PlayerMoveStateWind stateWind = new PlayerMoveStateWind();
    public PlayerMoveStateKnockBackSmall stateKnockBackSmall = new PlayerMoveStateKnockBackSmall();
    public PlayerMoveStateKnockBackLarge stateKnockBackLarge = new PlayerMoveStateKnockBackLarge();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraController = GameObject.FindGameObjectWithTag("CameraController");

        stateProcessor.State = stateDefault;
        stateDefault.exeDelegate = Default;
        stateLockOn.exeDelegate = LockOn;
        stateWind.exeDelegate = Wind;
        stateKnockBackSmall.exeDelegate = KnockBackSmall;
        stateKnockBackLarge.exeDelegate = KnockBackLarge;
    }

    void Update()
    {
        lockEnemyList.Sort(LengthSort); //lockEnemyListをプレイヤーからの距離が短い順にソート

        if (Input.GetKeyDown(KeyCode.Q) && lockEnemyList.Count > 0)//ロックオンする、しない
        {
            if (lockOn == false)
            {
                lockOn = true;
                lockEnemy = lockEnemyList[0];
                transform.LookAt(lockEnemy.transform.position); //ロックした敵の方を向く
                cameraController.GetComponent<CameraTest>().LockStart(); //プレイヤーの後ろに回る
                print("ロックオン開始");
            }
            else
            {
                lockOn = false;
                print("ロックオン終了");
            }
        }
        if (lockEnemyList.Count == 0 && lockOn == true)
        {
            lockOn = false;
            print("範囲内に敵なしロックオン終了");
        }



        if (Input.GetKeyDown(KeyCode.E) && lockOn == true) //ロックを1つ近い敵に変更
        {
            int index = lockEnemyList.IndexOf(lockEnemy);
            if (index == 0)
                lockEnemy = lockEnemyList[lockEnemyList.Count - 1];
            else
                lockEnemy = lockEnemyList[index - 1];

            transform.LookAt(lockEnemy.transform.position); //ロックした敵の方を向く
            cameraController.GetComponent<CameraTest>().LockStart(); //プレイヤーの後ろに回る
        }
        if (Input.GetKeyDown(KeyCode.R) && lockOn == true) //ロックを1つ遠い敵に変更
        {
            int index = lockEnemyList.IndexOf(lockEnemy);
            if (index == lockEnemyList.Count - 1)
                lockEnemy = lockEnemyList[0];
            else
                lockEnemy = lockEnemyList[index + 1];

            transform.LookAt(lockEnemy.transform.position); //ロックした敵の方を向く
            cameraController.GetComponent<CameraTest>().LockStart(); //プレイヤーの後ろに回る
        }




        //カメラの正面向きのベクトルを取得
        cameraForward = Camera.main.transform.forward;
        //y成分を無視する
        cameraForward.y = 0;
        //正規化（長さを1にする）
        cameraForward.Normalize();

        stateProcessor.Execute(); //設定されている移動状態を実行

        //地面との判定　ジャンプ処理
        previosGroundHit = currentGroundHit; //ひとつ前の状態
        currentGroundHit = CheckGrounded(); //現在の状態
        if (previosGroundHit == false && currentGroundHit == true)
        {
            velocityY = 0;
            print("地面に接触");
        }
        //if (controller.isGrounded && torndoHit == false)
        //    velocityY = 0;

        if (currentGroundHit)
            jampState = false;
        else
            jampState = true;

        if (jampState == false && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = 10;
            jampState = true;
        }

        if (currentGroundHit == false) velocityY -= gravity * Time.deltaTime;
        velocity.y = velocityY;

        //if (CheckGrounded())
        //{
        //    currentGroundHit = true;
        //    previosGroundHit = true;
        //}

        controller.Move(velocity * Time.deltaTime);
    }



    public void OnTriggerEnter(Collider other) //ロックオン範囲に入った敵をListに追加
    {
        if (other.gameObject.tag == "Enemy")
            lockEnemyList.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other) //ロックオン範囲から出た敵をListから削除
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (lockEnemy == other.gameObject) //範囲外出た敵がロックしている敵だったら　ロックを解除
            {
                lockOn = false;
            }
            lockEnemyList.Remove(other.gameObject);
            print("敵が範囲外に出た");
        }
    }

    public int LengthSort(GameObject a, GameObject b) //Listを敵との距離が近い順にソート
    {
        Vector3 VecA = transform.position - a.transform.position;
        Vector3 VecB = transform.position - b.transform.position;

        if (VecA.magnitude > VecB.magnitude) return 1;
        else if (VecA.magnitude < VecB.magnitude) return -1;
        else return 0;
    }

    public bool CheckGrounded() //地面に接地しているかどうかを調べる
    {
        //controller.isGroundedがtrueならRaycastを使わずに判定終了
        //if (controller.isGrounded) return true;
        //放つ光線の初期位置と姿勢
        //若干体にめり込ませた位置から発射しないと正しく判定できないときがある
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        //探索距離
        var tolerance = 1.5f;
        Debug.DrawRay(ray.origin, ray.direction * 1.5f);
        //Raycastがhitするかどうかで判定
        //地面にのみ衝突するようにレイヤを指定する
        return Physics.SphereCast(ray, 0.5f, tolerance, 1 << 8);
    }

    public bool GetLockOnInfo()
    {
        return lockOn;
    }

    public bool GetJampState()
    {
        return jampState;
    }

    public void SetWindPower(float power, Vector3 direction)
    {
        windPower = power;
        windDirection = direction;
    }

    public void SetVelocityY(int velocity)
    {
        velocityY = velocity;
    }

    /******************** プレイヤーの移動状態関係 ********************/
    public void Default() //通常移動
    {
        velocity =
            cameraForward * Input.GetAxis("Vertical") * walkSpeed
            + Camera.main.transform.right * Input.GetAxis("Horizontal") * walkSpeed;

        //キャラクターの向きを変える
        velocity.y = 0;
        if (velocity.magnitude > 0)
            transform.LookAt(transform.position + velocity);

        if (lockOn == true) stateProcessor.State = stateLockOn;
        if (windPower >= 1) stateProcessor.State = stateWind;
    }

    public void LockOn() //ロックオン時移動
    {
        transform.RotateAround(lockEnemy.transform.position,
            transform.up,
            lockOnRotateSpeed * Time.deltaTime * -Input.GetAxis("Horizontal"));

        velocity =
            (lockEnemy.transform.position - transform.position).normalized * Input.GetAxis("Vertical") * walkSpeed;

        //キャラクターの向きを変える
        velocity.y = 0;
        if (lockOn == true)
            transform.LookAt(lockEnemy.transform.position);

        if (lockOn == false) stateProcessor.State = stateDefault;
        if (windPower >= 1) stateProcessor.State = stateWind;
    }

    public void Wind() //気流に乗った時の移動
    {
        velocity = windDirection * windPower;
        windPower -= 0.5f;

        if (windPower <= 0 && lockOn == false) stateProcessor.State = stateDefault;
        if (windPower <= 0 && lockOn == true) stateProcessor.State = stateLockOn;
    }

    public void KnockBackSmall() //ノックバック小が起きた時の移動
    {
        velocity = Vector3.zero;

        Invoke("DefaultOrLockOnChange", 0.5f);
    }

    public void KnockBackLarge() //ノックバック大が起きた時の移動
    {
        velocity =
            transform.forward * -1.0f * knockBackPower;

        if (knockBackPower > 0) knockBackPower -= 0.5f;

        Invoke("DefaultOrLockOnChange", 2.0f);
    }

    public void ChangeKnockBackSmall()
    {
        stateProcessor.State = stateKnockBackSmall;
    }

    public void ChangeKnockBackLarge(float power)
    {
        knockBackPower = power;
        stateProcessor.State = stateKnockBackLarge;
    }

    public void DefaultOrLockOnChange()
    {
        if (lockOn == false) stateProcessor.State = stateDefault;
        if (lockOn == true) stateProcessor.State = stateLockOn;
    }
}
