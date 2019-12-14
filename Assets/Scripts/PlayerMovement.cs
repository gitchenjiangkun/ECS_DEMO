using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterController characterController;
    private float moveX;
    private float moveZ;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        Rota();
    }

    private void Move()
    {
        Vector3 targetPos = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(targetPos * gameManager.MovementSpeed * Time.deltaTime);

        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * gameManager.MovementSpeed * Time.deltaTime);
        }
    }

    private void Rota()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.point - transform.position), gameManager.RotatingSpeed);
        }
    }
}
