using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Transform playerPos;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void moveToPlayer()
    {
        Debug.Log(playerPos);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, step);
    }
}
