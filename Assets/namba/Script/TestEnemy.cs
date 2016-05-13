using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour{

    public float SearchDistance = 10f;  // 透過視認範囲
    public int maxlife = 40;            // 最大ＨＰ
    public float speed;                 // スピード
    public int LengeType = 2;           // 攻撃タイプ(1=格闘 2=お札 3=弓)

    private int life;
    private float rotateSmooth = 2.0f;  // 振り向きにかかる時間
    private float AttackDistance = 1f;   // 攻撃移行範囲
    private Vector3 StartPos;
    private Vector3 lostPos;
    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rd;
    private EnemyAttack attack;

    // Use this for initialization
    void Start () {
        // Playerの座標を取得
        player  = GameObject.FindGameObjectWithTag("Player").transform;
        agent   = GetComponent<NavMeshAgent>();
        rd      = GetComponent<Rigidbody>();
        attack  = GetComponent<EnemyAttack>();

        if (LengeType == 1) { AttackDistance = 1; }
        else if(LengeType == 2) { AttackDistance = 8; }
        else if(LengeType == 3) { AttackDistance = 10; }

        life = maxlife;
        StartPos = this.transform.position;
        Switch(0);
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
        if (ToTargetDistance > SearchDistance * 10.0f)
        {
            // 以降の処理をスルー
            return;
        }

        //Targetとの間の障害物がなければ行動
        RaycastHit hit;
        Vector3 temp = player.transform.position - this.transform.position;
        temp = temp.normalized;
        if (Physics.Raycast(this.transform.position, temp, out hit, SearchDistance))
        {
            if (hit.collider.tag == "Player")
            {
                Pursuit();
                Attack();
            }
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

    void LostContact()
    {// 見失った時の処理

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

        //攻撃処理
        attack.Attack(LengeType);

    }

    void Died()
    {// 死亡処理
        Destroy(this.gameObject, 1.0f);
    }



    void OnCollisionEnter(Collision col)
    {// 風との衝突
        if(col.gameObject.tag == "Wind")
        {
            Vector3 vec = 
                col.gameObject.GetComponent<WindHit>().a();
            vec.Normalize();
            StartCoroutine(Hit(vec, 0));
        }
    }

    /// <summary>
    /// 風に当たった時の処理
    /// </summary>
    /// <param name="vec">吹っ飛ぶ方向</param>
    /// <param name="dmg">ダメージ</param>
    /// <returns></returns>
    private IEnumerator Hit(Vector3 vec, int dmg)
    {

        yield return new WaitForSeconds(0.1f);
        life -= dmg;
        if (life < 0)
        {
            Died();
        }
        iTween.MoveTo(gameObject, transform.position + vec, 0.5f);

        yield return new WaitForSeconds(1.0f);

        iTween.RotateTo(gameObject, iTween.Hash("x", 0, "z", 0));
    }


    /// <summary>
    /// NavMeshとIsKinematicのON/OFF
    /// </summary>
    /// <param name="a">0でON 1でOFF</param>
    private void Switch(int a)
    {
        if(a == 0)
        {
            agent.enabled = false;
            rd.isKinematic = false;
        }
        else if(a == 1)
        {
            agent.enabled = true;
            rd.isKinematic = true;
        }
    }

}
