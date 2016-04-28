using UnityEngine;
using System.Collections;

public class AttackPattern : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void AttackPatternDecision(float angle, Vector3 vector)
    {
        //横方向 → ←
        if (angle >= -20.0f && angle <= 20)
        {
            if (vector.x > 0)
            {
                print("パターン1 →");
                GameObject.FindObjectOfType<WindAttack>().WindAttack1();
            }
            if (vector.x < 0)
            {
                print("パターン2 ←");
                GameObject.FindObjectOfType<WindAttack>().WindAttack2();
            }
        }

        //縦方向 ↓ ↑
        if ((angle <= -70 && angle >= -90) || (angle >= 70 && angle <= 90))
        {
            if (vector.y < 0)
            {
                print("パターン3 ↓");
                GameObject.FindObjectOfType<WindAttack>().WindAttack3();
            }
            if (vector.y > 0)
            {
                print("パターン4 ↑");
                GameObject.FindObjectOfType<WindAttack>().WindAttack4();
            }
        }

        //右斜め方向 ↙ ↗
        if (angle > 20 && angle < 70)
        {
            if (vector.x < 0)
            {
                print("パターン5 ↙");
                GameObject.FindObjectOfType<WindAttack>().WindAttack5();
            }
            if (vector.x > 0)
            {
                print("パターン6 ↗");
                GameObject.FindObjectOfType<WindAttack>().WindAttack6();
            }
        }

        //左斜め方向 ↘ ↖
        if (angle < -20 && angle > -70)
        {
            if (vector.x > 0)
            {
                print("パターン7 ↘");
                GameObject.FindObjectOfType<WindAttack>().WindAttack7();
            }
            if (vector.x < 0)
            {
                print("パターン8 ↖");
                GameObject.FindObjectOfType<WindAttack>().WindAttack8();
            }
        }
    }
}
