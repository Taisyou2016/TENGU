using UnityEngine;
using System.Collections;

public class Kamaitachi : MonoBehaviour
{
    public GameObject kamaitatiMotion;
    public GameObject linePrefab;
    public int cost = 10;

    private Vector3 startPoint;
    public Vector3 point;
    public Vector3 previousPoint;

    public float lineLength = 1.0f;
    public float scaleY = 0.5f;
    public float scaleZ = 0.5f;
    public float depth = 0.0f; //DrawLineを続ける時間　これで奥行きを決める
    public float angleX = 0.0f; //linePrefabを回転させる角度
    public Vector3 moveVector;

    private bool hit = false;

    void Start()
    {
        point = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().MpConsumption(cost, gameObject);
    }

    void Update()
    {
        if (depth > 0.0f)
            DrawLine();

        transform.GetComponent<Rigidbody>().velocity = moveVector;
        depth -= Time.deltaTime;

        if (hit == true) //linePrefabがPlayer以外に当たったら自分を消す
            Destroy(gameObject);
    }

    public void Move(Vector3 direction, float speed, float depth)
    {
        moveVector = direction * speed;
        kamaitatiMotion.GetComponent<Rigidbody>().velocity = direction * (speed + 10.0f); //かまいたちの速度が変わっても、当たり判定の長さが変わらないように
        transform.forward = direction; //進行方向を前に

        this.depth = depth;
    }

    public void DrawLine()
    {
        if ((kamaitatiMotion.transform.position - point).magnitude > lineLength)
        {
            GameObject obj = Instantiate(linePrefab, startPoint, transform.rotation) as GameObject;
            obj.transform.parent = this.transform;

            obj.transform.position = kamaitatiMotion.transform.position;
            obj.transform.localPosition = obj.transform.localPosition * -1.0f;
            obj.transform.right = (kamaitatiMotion.transform.position - point).normalized;
            obj.transform.Rotate(new Vector3(angleX, 0, 0));
            obj.transform.localScale = new Vector3((obj.transform.position - point).magnitude, scaleY, scaleZ);
            if ((obj.transform.position - point).magnitude > lineLength)
                point = obj.transform.position;
            else
            {
                Destroy(obj);
                //print((obj.transform.position - point).magnitude + "長さが足りずに消された");
            }
        }
    }

    public void SetScale(float Y, float Z)
    {
        scaleY = Y;
        scaleZ = Z;
    }

    public void SetRotateX(float angleX) //linePrefabのX軸の回転角度を設定
    {
        this.angleX = angleX;
    }

    public void Hit() //linePrefabがPlayer以外に当たったら使用する
    {
        hit = true;
    }
}
