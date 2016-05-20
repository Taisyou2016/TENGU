using UnityEngine;
using System.Collections;

public class KamaitachiGeneration : MonoBehaviour
{
    public GameObject kamaitachiPrefab;
    public float speed = 10.0f; //かまいたちの速度

    void Start()
    {

    }

    void Update()
    {

    }

    public void KamaitachiGeneration1() //パターン1 → ←
    {
        GameObject kamaitachi = Instantiate(kamaitachiPrefab);

        kamaitachi.transform.position =
            transform.position
            + transform.forward * 3.0f;

        kamaitachi.GetComponent<Kamaitachi>().SetScale(1.0f, 5.0f);
        kamaitachi.GetComponent<Kamaitachi>().Move(transform.forward, speed, 0.08f);
    }

    public void KamaitachiGeneration2() //パターン2 ↓ ↑
    {
        GameObject kamaitachi = Instantiate(kamaitachiPrefab);

        kamaitachi.transform.position =
            transform.position
            + transform.forward * 3.0f
            + transform.up * 2.0f;

        kamaitachi.GetComponent<Kamaitachi>().SetScale(5.0f, 1.0f);
        kamaitachi.GetComponent<Kamaitachi>().Move(transform.forward, speed, 0.6f);
    }

    public void KamaitachiGeneration3() //パターン3 ↙ ↗
    {
        GameObject kamaitachi = Instantiate(kamaitachiPrefab);

        kamaitachi.transform.position =
            transform.position
            + transform.forward * 3.0f
            + transform.up * 2.0f;

        kamaitachi.GetComponent<Kamaitachi>().SetRotateX(45.0f);
        kamaitachi.GetComponent<Kamaitachi>().SetScale(1.0f, 5.0f);
        kamaitachi.GetComponent<Kamaitachi>().Move(transform.forward, speed, 0.2f);
    }

    public void KamaitachiGeneration4() //パターン4 ↘ ↖
    {
        GameObject kamaitachi = Instantiate(kamaitachiPrefab);

        kamaitachi.transform.position =
            transform.position
            + transform.forward * 3.0f
            + transform.up * 2.0f;

        kamaitachi.GetComponent<Kamaitachi>().SetRotateX(-45.0f);
        kamaitachi.GetComponent<Kamaitachi>().SetScale(1.0f, 5.0f);
        kamaitachi.GetComponent<Kamaitachi>().Move(transform.forward, speed, 0.2f);
    }
}
