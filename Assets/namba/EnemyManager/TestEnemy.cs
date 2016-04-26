using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour {

    public float TargetDistance = 10f;  // 透過視認範囲
    public int maxlife;                 // 最大ＨＰ
    public float speed;                 // スピード

    private int life;
    private float rotateSmooth = 0.8f;  // 振り向きにかかる時間
    private Transform player;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        // Playerの座標を取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
        life = maxlife;

        agent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
        // Playerとの距離
        float ToTargetDistance = Vector3.SqrMagnitude(this.transform.position - player.position);

        // 透過視認範囲外ならば
        if (ToTargetDistance > TargetDistance * 10.0f)
        {
            // 以降の処理をスルー
            return;
        }

        // Targetとの間の障害物を検挙
        NavMeshHit hit;
        if (!agent.Raycast(player.position, out hit))
        {
            Pursuit();
        }
    }

    void Pursuit()
    {// 追跡処理
        // Playerの方向を向く
        Quaternion targetRotate = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, Time.deltaTime * rotateSmooth);

        // 前に進む
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void Attack()
    {// 攻撃処理

    }

    void Died()
    {// 死亡処理

    }
}
