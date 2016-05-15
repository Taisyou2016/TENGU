﻿using UnityEngine;
using System.Collections;

public class KamaitachiBlock : MonoBehaviour
{
    public int damage = 20;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;
        else if (other.tag == "Enemy") //Enemyに当たったら親のかまいたちを消すフラグを立てる
            transform.parent.GetComponent<Kamaitachi>().Hit();
    }
}
