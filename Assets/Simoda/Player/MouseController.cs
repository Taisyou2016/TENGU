using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 vector;
    private float radian;
    private float angle;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;

            Invoke("AttackDecision", 0.3f);
        }
    }

    public void AttackDecision()
    {
        endPos = Input.mousePosition;
        vector = endPos - startPos;
        radian = vector.y / vector.x;
        angle = Mathf.Atan(radian) * Mathf.Rad2Deg;

        //print(startPos);
        //print(endPos);
        //print(endPos - startPos);
        //print("ラジアン" + radian);
        print("角度" + Mathf.Atan(radian) * Mathf.Rad2Deg);
        print("長さ" + vector.magnitude);

        if (vector.magnitude > 100.0f)
        {
            GameObject.FindObjectOfType<AttackPattern>().AttackPatternDecision(angle, vector);
        }
    }
}
