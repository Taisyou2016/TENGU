using UnityEngine;
using System.Collections;

public enum EnemyState
{
    Wait,
    Pursuit,
    LostContact,
    Attack,
    Died
}

public class EnemyRoutine : EnemyBase<EnemyRoutine, EnemyState>
{
    public float SearchDistance = 10f;  // 透過視認範囲
    public int maxlife = 40;            // 最大ＨＰ
    public float speed;                 // スピード
    public int LengeType = 2;           // 攻撃タイプ(1=格闘 2=お札 3=弓)

    private int life;
    private bool Pflag = false;
    private float rotateSmooth = 3.0f;  // 振り向きにかかる時間
    private float AttackDistance;       // 攻撃移行範囲
    private Vector3 StartPos;
    private Vector3 lostPos;
    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rd;
    private EnemyAttack attack;
    private CharacterController ctrl;

    // Use this for initialization
    public void Start()
    {
        // Playerの座標を取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rd = GetComponent<Rigidbody>();
        attack = GetComponent<EnemyAttack>();
        ctrl = GetComponent<CharacterController>();

        if (LengeType == 1) { AttackDistance = 1; }
        else if (LengeType == 2) { AttackDistance = 8; }
        else if (LengeType == 3) { AttackDistance = 10; }
        life = maxlife;
        StartPos = this.transform.position;
        lostPos = StartPos;
        Switch(0);

        // Stateの初期設定
        statelist.Add(new StateWait(this));
        statelist.Add(new StatePursuit(this));
        statelist.Add(new StateLostContact(this));
        statelist.Add(new StateAttack(this));
        statelist.Add(new StateDied(this));

        stateManager = new StateManager<EnemyRoutine>();

        ChangeState(EnemyState.Wait);
    }

    private void PSeach()
    {
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
        int layerMask = ~LayerMask.GetMask(new string[] { "Enemy", "Bullet", "PlayerAttack"});
        if (Physics.Raycast(this.transform.position, temp, out hit, SearchDistance, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                Pflag = true;
                return;
            }
        }

        Pflag = false;
    }

    /// <summary>
    /// NavMeshとIsKinematicのON/OFF
    /// </summary>
    /// <param name="a">0でON 1でOFF</param>
    private void Switch(int a)
    {
        if (a == 0)
        {
            agent.enabled = false;
            rd.isKinematic = false;
        }
        else if (a == 1)
        {
            agent.enabled = true;
            rd.isKinematic = true;
        }
    }

    public void Damage(int dmg)
    {
        life -= dmg;
        if (life < 0)
        {
            ChangeState(EnemyState.Died);
        }
    }

    private IEnumerator Lost()
    {
        yield return new WaitForSeconds(2);
        agent.SetDestination(StartPos);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            //iTween.RotateTo(gameObject, iTween.Hash("x", 0, "z", 0));
        }
    }

    /*----------------------------------------------------/
                        ここからState処理
    /----------------------------------------------------*/



    /// <summary>
    /// 待機状態
    /// </summary>
    private class StateWait : IState<EnemyRoutine>
    {
        public StateWait(EnemyRoutine owner) : base(owner) { }

        public override void Initialize()
        {
        }

        public override void Execute()
        {
            owner.PSeach();

            if(owner.Pflag)
            {
                owner.Switch(0);
                owner.ChangeState(EnemyState.Pursuit);
            }

        }

        public override void End()
        {
        }

    }

    /// <summary>
    /// 追跡処理
    /// </summary>
    private class StatePursuit : IState<EnemyRoutine>
    {
        public StatePursuit(EnemyRoutine owner) : base(owner) { }

        public override void Initialize()
        {
            owner.Switch(0);
        }

        public override void Execute()
        {
            owner.PSeach();
            if(!owner.Pflag)
            {
                owner.ChangeState(EnemyState.LostContact);
            }

            // Playerとの距離
            float ToAttackDistance = Vector3.SqrMagnitude(this.owner.transform.position - owner.player.position);
            // 攻撃範囲内
            if (ToAttackDistance < owner.AttackDistance * 10.0f)
            {
                //攻撃処理
                owner.ChangeState(EnemyState.Attack);
            }


            // Playerの方向を向く
            Vector3 vec = owner.player.position - owner.transform.position;
            vec.y = 0;
            Quaternion targetRotate = Quaternion.LookRotation(vec);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotate, Time.deltaTime * owner.rotateSmooth);

            // 前に進む
            owner.transform.Translate(Vector3.forward * owner.speed * Time.deltaTime);

        }

        public override void End()
        {
        }
    }

    /// <summary>
    /// 見失った時の処理
    /// </summary>
    private class StateLostContact : IState<EnemyRoutine>
    {
        public StateLostContact(EnemyRoutine owner) : base(owner) { }

        public override void Initialize()
        {
            owner.lostPos = owner.player.position;
        }

        public override void Execute()
        {
            owner.PSeach();
            if(owner.Pflag)
            {
                owner.ChangeState(EnemyState.Pursuit);
            }

            owner.Switch(1);
            owner.agent.SetDestination(owner.lostPos);

            if (Vector3.SqrMagnitude(owner.transform.position - owner.lostPos) <= 2)
            {
                owner.StartCoroutine(owner.Lost());
                owner.ChangeState(EnemyState.Wait);
            }

        }

        public override void End()
        {
        }

    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    private class StateAttack : IState<EnemyRoutine>
    {
        public StateAttack(EnemyRoutine owner) : base(owner) { }

        public override void Initialize()
        {
            owner.Switch(0);
        }

        public override void Execute()
        {
            // Playerとの距離
            float ToAttackDistance = Vector3.SqrMagnitude(owner.transform.position - owner.player.position);
            // 攻撃範囲外
            if (ToAttackDistance > owner.AttackDistance * 10.0f)
            {
                //追跡ステートに移動
                owner.ChangeState(EnemyState.Pursuit);
            }
            owner.PSeach();
            if(!owner.Pflag)
            {
                owner.ChangeState(EnemyState.LostContact);
            }

            // Playerの方向を向く
            Quaternion targetRotate = Quaternion.LookRotation(owner.player.position - owner.transform.position);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotate, Time.deltaTime * owner.rotateSmooth);



            owner.attack.Attack(owner.LengeType);
        }

        public override void End()
        {
        }

    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    private class StateDied : IState<EnemyRoutine>
    {
        public StateDied(EnemyRoutine owner) : base(owner) { }

        public override void Initialize()
        {
            owner.Switch(0);
            Destroy(owner.gameObject, 1.0f);
        }

        public override void Execute()
        {
        }

        public override void End()
        {
        }

    }


}
