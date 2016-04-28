using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour{

    public float TargetDistance = 10f;  // 透過視認範囲
    public float AttackDistance = 1f;   // 攻撃移行範囲
    public int maxlife = 40;            // 最大ＨＰ
    public float speed;                 // スピード
    public GameObject obj;

    private int life;
    private float rotateSmooth = 2.0f;  // 振り向きにかかる時間
    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rd;

    // Use this for initialization
    void Start () {
        // Playerの座標を取得
        player  = GameObject.FindGameObjectWithTag("Player").transform;
        agent   = GetComponent<NavMeshAgent>();
        rd      = GetComponent<Rigidbody>();
        life = maxlife;
	}

    // Update is called once per frame
    public void Update()
    {
        Wait();
    }

    void Wait()
    {// 待機状態

        // Playerとの距離
        float ToTargetDistance = Vector3.SqrMagnitude(this.transform.position - player.position);

        // 透過視認範囲外ならば
        if (ToTargetDistance > TargetDistance * 10.0f)
        {
            // 以降の処理をスルー
            return;
        }

        Pursuit();
        Attack();

        // Targetとの間の障害物を検挙
        //NavMeshHit hit;
        //if (!agent.Raycast(player.position, out hit))
        //{
        //}
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
        // Playerとの距離
        float ToAttackDistance = Vector3.SqrMagnitude(this.transform.position - player.position);
        // 攻撃範囲外ならば
        if (ToAttackDistance > AttackDistance * 10.0f)
        {
            // 以降の処理をスルー
            return;
        }

        //print("攻撃開始");

    }

    void Died()
    {// 死亡処理
        Destroy(this.gameObject);
    }

    void Hit(Vector3 vec, int damage)
    {// ダメージ処理
        life -= damage;
        if(life <= 0)
        {
            Died();
        }
        rd.AddForce(vec);

    }

    void OnCollisionEnter(Collision col)
    {// 風との衝突
        if(col.gameObject.tag == "Wind")
        {
            Switch(0);

            Vector3 vec = 
                col.gameObject.GetComponent<WindHit>().a();
            vec.Normalize();
            Hit(vec, 0);   
        }
    }

    void Switch(int a)
    {
        if (a == 0)
        {
            agent.enabled = false;
            rd.isKinematic = false;
        }
        else if(a == 1)
        {
            rd.isKinematic = true;
            agent.enabled = true;
        }

    }
}
