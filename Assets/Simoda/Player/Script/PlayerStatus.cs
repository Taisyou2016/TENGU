using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 10; //最大HP
    public int maxMp = 100; //最大妖力
    public int currentHp; //現在のHP
    public int currentMp; //現在の妖力
    public int mpAutoRecoveryCost = 1; //自動回復するときの妖力の量
    public float mpAutoRecoveryTime = 1.0f; //自動回復の間隔
    public bool invincible = false; //無敵化どうか
    public float knockBackSmallInvincibleTime = 1.0f; //ノックバック小の時の無敵時間
    public float knockBackLargeInvincibleTime = 3.0f; //ノックバック大の時の無敵時間

    private float lastMpAutoRecoveryTime = 0.0f; //前に回復した時の時刻
    private bool mpOver = false; //妖力がOvarしたかどうか
    private float currentInvincibleTime = 0.0f; //現在の無敵時間

    void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
    }

    void Update()
    {
        //現在の時刻が（前の回復した時刻 + 自動回復の間隔）を過ぎている + 現在の妖力が100より少なければ妖力を回復する
        if ((Time.time >= lastMpAutoRecoveryTime + mpAutoRecoveryTime) && currentMp < 100)
        {
            lastMpAutoRecoveryTime = Time.time;
            currentMp += mpAutoRecoveryCost;
        }

        if (currentMp == 0) //妖力が0になったらmpOverをtrueに
            mpOver = true;
        if (currentMp == 100 && mpOver == true) //妖力が100まで回復したらmpOverをfalseに
            mpOver = false;

    }

    public void HpRecovery(int cost) //HPをcost分回復　maxHPを超えたらmaxHPを代入
    {
        if (currentHp + cost > maxHp)
            currentHp = maxHp;
        else
            currentHp += cost;
    }

    public void HpDamage(int damage)
    {

    }

    public void MpConsumption(int cost, GameObject obj) //cost分現在のMPを消費する MPがcostより少なかったら発生させたobjを消す
    {
        if (mpOver == true) //一度妖力が0になったら100になるまで使えない
        {
            Destroy(obj);
            print("妖力回復中");
            return;
        }
        if (currentMp < cost)
        {
            Destroy(obj);
            print("妖力が足りない");
            return;
        }
        currentMp -= cost;
    }
}