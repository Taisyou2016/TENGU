using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 10; //最大HP
    public int maxMp = 100; //最大妖力
    public int currentHp; //現在のHP
    public int currentMp; //現在の妖力
    public int autoRecoveryMp = 1; //自動回復するときの妖力の量
    public float autoRecoveryMpTime = 1.0f; //自動回復の間隔

    private float lastAutoRecoveryTime = 0; //前に回復した時の時刻

    void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
    }

    void Update()
    {
        //現在の時刻が（前の回復した時刻 + 自動回復の間隔）を過ぎている + 現在の妖力が100より少なければ妖力を回復する
        if ((Time.time >= lastAutoRecoveryTime + autoRecoveryMpTime) && currentMp < 100)
        {
            lastAutoRecoveryTime = Time.time;
            currentMp += autoRecoveryMp;
        }
    }

    public void HpRecovery(int cost) //HPをcost分回復　maxHPを超えたらmaxHPを代入
    {
        if (currentHp + cost > maxHp)
            currentHp = maxHp;
        else
            currentHp += cost;
    }

    public void MpConsumption(int cost, GameObject obj) //cost分現在のMPを消費する MPがcostより少なかったら発生させたobjを消す
    {
        if (currentMp < cost)
        {
            Destroy(obj);
            print("妖力が足りない");
            return;
        }
        currentMp -= cost;
    }
}