using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 4.0f; //歩くスピード（メートル/秒）
    public float lockOnRotateSpeed = 45.0f; //ロックオンしているときの横移動
    public float gravity = 10.0f; //重力加速度
    public float jampPower = 10.0f;

    public float windPower = 0.0f;
    public Vector3 windDirection;

    private CharacterController controller;
    private float velocityY = 0;
    private bool jampState = false;
    private Vector3 velocity;

    private List<GameObject> lockEnemyList = new List<GameObject>();
    private GameObject lockEnemy;
    private bool lockOn = false;
    private GameObject cameraController;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraController = GameObject.FindGameObjectWithTag("CameraController");
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
        Vector3 forward = Camera.main.transform.forward;
        //y成分を無視する
        forward.y = 0;
        //正規化（長さを1にする）
        forward.Normalize();

        if (windPower <= 1)
        {
            if (lockOn == true) //ロックオン時の移動
            {
                transform.RotateAround(lockEnemy.transform.position,
                    transform.up,
                    lockOnRotateSpeed * Time.deltaTime * -Input.GetAxis("Horizontal"));

                velocity =
                    (lockEnemy.transform.position - transform.position).normalized * Input.GetAxis("Vertical") * walkSpeed;
            }
            else //通常時の移動
            {
                velocity =
                    forward * Input.GetAxis("Vertical") * walkSpeed
                    + Camera.main.transform.right * Input.GetAxis("Horizontal") * walkSpeed;
            }
            //Vector3 velocity =
            //    Vector3.forward * Input.GetAxis("Vertical") * walkSpeed
            //    + Vector3.right * Input.GetAxis("Horizontal") * walkSpeed;
        }
        else
        {//Windに触れた時の移動
            velocity = windDirection * windPower;
            windPower -= 0.5f;
        }


        if (controller.isGrounded) //ジャンプ処理
        {
            velocityY = 0;
            jampState = false;
        }
        else
        {
            jampState = true;
        }

        if (jampState == false && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = 10;
            jampState = true;
        }

        velocityY -= gravity * Time.deltaTime;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

        //キャラクターの向きを変える
        velocity.y = 0;
        if (velocity.magnitude > 0)
        {
            //transform.rotation = Quaternion.LookRotation(velocity);

            transform.LookAt(transform.position + velocity); //上と同じ
        }
    }

    public void SetWindPower(float power, Vector3 direction)
    {
        windPower = power;
        windDirection = direction;
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
            if (lockEnemy == other.gameObject) //範囲外出た敵がロックしている敵だったら　一番近い敵をロック
                lockEnemy = lockEnemyList[0];

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

    public bool GetLockOnInfo()
    {
        return lockOn;
    }

    public bool GetJampState()
    {
        return jampState;
    }
}
