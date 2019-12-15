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
        Invoke("LiftDestroy", gameManager.BulletLifeTime);
    }

    private void Update()
    {
        rigidbody.MovePosition(transform.position + transform.forward * gameManager.BulletSpeed * Time.deltaTime);
    }

    private void LiftDestroy()
    {
        Destroy(gameObject);
    }
}
