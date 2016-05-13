using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public GameObject ohuda;
    public GameObject bow;
    private float cooltime_S, cooltime_M, cooltime_L;
    private bool run = false;

    void Start()
    {
        cooltime_S = 0.5f;
        cooltime_M = 3.0f;
        cooltime_L = 3.0f;
    }

    /// <summary>
    /// Enemyの攻撃生成
    /// </summary>
    /// <param name="a">1=格闘 2=お札 3=弓</param>
    public void Attack(int a)
    {
        if(a == 2)
        {
            StartCoroutine(OhudaAttack());
        }
        else if(a== 3)
        {
            StartCoroutine(yumiAttack());
        }
    }

    private IEnumerator OhudaAttack()
    {
        if (run) { yield break; }
        run = true;

        //処理

        Instantiate(ohuda, transform.localPosition + transform.forward, transform.rotation);


        yield return new WaitForSeconds(cooltime_M);
        run = false;
    }

    private IEnumerator yumiAttack()
    {
        if (run) { yield break; }
        run = true;

        //処理
        Instantiate(bow, transform.localPosition + transform.forward, transform.rotation);


        yield return new WaitForSeconds(cooltime_L);
        run = false;
    }
}
