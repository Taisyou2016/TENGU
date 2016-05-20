﻿using UnityEngine;
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
    public Material modelMaterial; //モデルのメッシュ

    private float lastMpAutoRecoveryTime = 0.0f; //前に回復した時の時刻
    private bool mpOver = false; //妖力がOvarしたかどうか
    private float currentInvincibleTime = 0.0f; //現在の無敵時間
    private Color originColor; //モデルの元の色
    private bool alphaZero; //アルファが0以下になったらtreu　1以上になったらfalse
    private float flashingSecond = 0.0f;

    void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;

        originColor = modelMaterial.color;
        //Color alpha = new Color(0, 0, 0, 0.5f);
        //modelMesh.material.color -= alpha;
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

        currentInvincibleTime -= Time.deltaTime;
        if (currentInvincibleTime <= 0) //currentInvincibleTimeが0より小さくなったら無敵を解除
            invincible = false;

        if (Input.GetKeyDown(KeyCode.K))
            HpDamage(1);
        if (Input.GetKeyDown(KeyCode.L))
            HpDamage(3);

        flashingSecond -= Time.deltaTime;
        if (flashingSecond >= 0.0f)
        {
            Flashing();
        }
        else
        {
            Color alphaReset = new Color(0, 0, 0, 1.0f - modelMaterial.color.a);
            modelMaterial.color = originColor;
        }

    }

    public void HpRecovery(int cost) //HPをcost分回復　maxHPを超えていたらmaxHPを代入
    {
        if (currentHp + cost > maxHp)
            currentHp = maxHp;
        else
            currentHp += cost;
    }

    public void HpDamage(int damage) //HPをダメージ分減らす
    {
        if (invincible == true) return; //無敵だったら処理を行わない

        if (damage == 1) //damageが1だったらknockBackSmallInvincibleTimeを代入
        {
            currentInvincibleTime = knockBackSmallInvincibleTime;
            gameObject.GetComponent<PlayerMove>().ChangeKnockBackSmall();
            SetFlashingSecond(1.0f);
        }
        else if (damage >= 2) //damageが2以上だったらknockBackLargeInvincibleTimeを代入
        {
            currentInvincibleTime = knockBackLargeInvincibleTime;
            gameObject.GetComponent<PlayerMove>().ChangeKnockBackLarge(10.0f);
            SetFlashingSecond(3.0f);
        }

        if (currentHp - damage <= 0) //現在のHPが0より小さかったら0に
            currentHp = 0;
        else
            currentHp -= damage;

        invincible = true; //無敵に
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

    public void SetFlashingSecond(float second)
    {
        flashingSecond = second;
    }

    public void Flashing()
    {
        //アルファが0以下になったらtreu　1以上になったらfalse
        if (modelMaterial.color.a <= 0.0f)
            alphaZero = true;
        else if (modelMaterial.color.a >= 1.0f)
            alphaZero = false;

        Color alpha = new Color(0, 0, 0, Time.deltaTime * 2.0f); //アルファを0.5秒で1変化させる

        if (alphaZero == false)
            modelMaterial.color -= alpha;
        else
            modelMaterial.color += alpha;
    }
}