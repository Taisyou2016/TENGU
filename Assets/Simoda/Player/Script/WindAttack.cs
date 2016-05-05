using UnityEngine;
using System.Collections;

public class WindAttack : MonoBehaviour
{
    public GameObject windPrefab;
    public float power = 20.0f;

    private Vector3 vector = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {

    }

    public void WindAttack1() //パターン1 →
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.5f
            + transform.right * -5.0f;

        wind.GetComponent<Wind>().SetForce(transform.forward, 10.0f);
        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move(transform.right * power, 0.5f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    transform.right
        //    * power;

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public void WindAttack2() //パターン2 ←
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.5f
            + transform.right * 5.0f;

        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move(transform.right * power * -1.0f, 0.5f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    transform.right
        //    * power
        //    * -1.0f;

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public void WindAttack3() //パターン3 ↓
    {
        //this.vector = Vector3.Normalize(vector);
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.0f
            + transform.up * 2.0f;

        wind.GetComponent<Wind>().SetScale(7.0f, 2.5f);
        wind.GetComponent<Wind>().Move(transform.forward * power, 0.3f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    transform.forward
        //    * power;

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(WindVectorMove(0.5f, wind, -power));
        //StartCoroutine(StopWind(2.0f, wind));
    }

    public void WindAttack4() //パターン4 ↑
    {
        //this.vector = Vector3.Normalize(vector);
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.0f
            + transform.up * 2.0f;

        wind.GetComponent<Wind>().SetScale(7.0f, 2.5f);
        wind.GetComponent<Wind>().Move(transform.forward * power, 0.3f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    transform.forward
        //    * power;

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(WindVectorMove(0.5f, wind, power));
        //StartCoroutine(StopWind(2.0f, wind));
    }

    public void WindAttack5() //パターン5 ↙
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.5f
            + transform.right * 4.0f
            + transform.up * 3.0f;

        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move((transform.right * power * -1.0f) + (transform.up * power * -1.0f / 2), 0.35f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    (transform.right * power * -1.0f / 2)
        //    + (transform.up * power * -1.0f / 2);

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public void WindAttack6() //パターン6 ↗
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.5f
            + transform.right * -4.0f
            + transform.up * -1.5f;

        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move((transform.right * power * 1.0f) + (transform.up * power * 1.0f / 2), 0.35f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    (transform.right * power * 1.0f / 2)
        //    + (transform.up * power * 1.0f / 2);

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public void WindAttack7() //パターン7 ↘
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position
            + transform.forward * 2.5f
            + transform.right * -3.0f
            + transform.up * 3.0f;

        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move((transform.right * power * 1.0f) + (transform.up * power * -1.0f / 2), 0.35f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    (transform.right * power * 1.0f / 2)
        //    + (transform.up * power * -1.0f / 2);

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public void WindAttack8() //パターン8 ↖
    {
        GameObject wind = Instantiate(windPrefab);

        wind.transform.position =
            transform.position +
            transform.forward * 2.5f
            + transform.right * 4.0f
            + transform.up * -1.5f;

        wind.GetComponent<Wind>().SetScale(1.0f, 2.0f);
        wind.GetComponent<Wind>().Move((transform.right * power * -1.0f) + (transform.up * power * 1.0f / 2), 0.35f);

        //wind.GetComponent<Rigidbody>().velocity =
        //    (transform.right * power * -1.0f / 2)
        //    + (transform.up * power * 1.0f / 2);

        //Vector3 hitVector = wind.GetComponent<Rigidbody>().velocity;
        //wind.GetComponent<WindHit>().SetVector(hitVector);

        //StartCoroutine(StopWind(0.5f, wind));
    }

    public IEnumerator WindVectorMove(float waitTime, GameObject wind, float vectorPower)
    {
        yield return new WaitForSeconds(waitTime);

        wind.GetComponent<Rigidbody>().velocity +=
            transform.up
            * vectorPower;
    }
    public IEnumerator StopWind(float waitTime, GameObject wind)
    {
        yield return new WaitForSeconds(waitTime);

        wind.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
