using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rigidbody;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.right * gameManager.BulletSpeed);
        Invoke("LiftDestroy", gameManager.BulletLifeTime);
    }

    private void Update()
    {
    }

    private void LiftDestroy()
    {
        Destroy(gameObject);
    }
}
