using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    //private Rigidbody rd;
    private Transform player;
    public float angle = 45;
    public float gravity = 9.8f;
    private float t = 0;
    private float Vx, Vy;
    private Vector3 bedforpos;

    void Awake()
    {
        //rd = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 距離計算
        float targetDistance = Vector3.Distance(this.transform.position, player.position);
        // 初速計算
        float f_velocity = targetDistance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);
        Vx = Mathf.Sqrt(f_velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(f_velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0, (Vy - (gravity * t)) * Time.deltaTime, Vx * Time.deltaTime);
        t += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
