using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public GameObject ohuda;
    public GameObject bow;
    public GameObject punch1, punch2;
    private float cooltime_S ,cooltime_M, cooltime_L;
    private bool run = false;
    private float a;

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
        if(a == 1)
        {
            StartCoroutine(InFighting());
        }
        else if(a == 2)
        {
            StartCoroutine(OhudaAttack());
        }
        else if(a== 3)
        {
            StartCoroutine(yumiAttack());
        }
    }

    private IEnumerator InFighting()
    {
        if (run) { yield break; }
        run = true;
        // 処理

        // ストレート
        a = Random.Range(0, 1);
        if (a == 0) {
            Instantiate(punch1, transform.localPosition + transform.forward, transform.rotation);
        }else {
            Instantiate(punch2, transform.localPosition + transform.forward, transform.rotation);
        }

        yield return new WaitForSeconds(cooltime_S);
        run = false;
    }

    private IEnumerator OhudaAttack()
    {
        if (run) { yield break; }
        run = true;
        //処理

        Vector3 vec = transform.forward + transform.up / 2;
        Instantiate(ohuda, transform.localPosition + vec, transform.rotation);


        yield return new WaitForSeconds(cooltime_M);
        run = false;
    }

    private IEnumerator yumiAttack()
    {
        if (run) { yield break; }
        run = true;
        //処理

        Instantiate(bow, transform.localPosition, this.transform.rotation);


        yield return new WaitForSeconds(cooltime_L);
        run = false;
    }
}
