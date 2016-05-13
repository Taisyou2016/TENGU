using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
    public float length = 200.0f;

    private Vector3 startPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
    private Vector3 endPos;
    private Vector3 vector;
    private float radian;
    private float angle;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;

            Invoke("WindAttackDecision", 0.3f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;

            Invoke("KamaitachiDecision", 0.3f);
        }
    }

    public void WindAttackDecision()
    {
        endPos = Input.mousePosition;
        vector = endPos - startPos;
        radian = vector.y / vector.x;
        angle = Mathf.Atan(radian) * Mathf.Rad2Deg;

        //print(startPos);
        //print(endPos);
        //print(endPos - startPos);
        //print("ラジアン" + radian);
        //print("角度" + Mathf.Atan(radian) * Mathf.Rad2Deg);
        //print("長さ" + vector.magnitude);

        if (vector.magnitude > length)
        {
            GameObject.FindObjectOfType<AttackPattern>().WindPatternDecision(angle, vector);
        }
    }

    public void KamaitachiDecision()
    {
        endPos = Input.mousePosition;
        vector = endPos - startPos;
        radian = vector.y / vector.x;
        angle = Mathf.Atan(radian) * Mathf.Rad2Deg;

        if (vector.magnitude > length)
        {
            GameObject.FindObjectOfType<AttackPattern>().KamaitachiPatternDecision(angle, vector);
        }
    }
}
