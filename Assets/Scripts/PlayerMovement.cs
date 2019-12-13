using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;

    private CharacterController characterController;

    private float moveX;
    private float moveZ;

    private void Start()
    {
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
        characterController.Move(targetPos * MovementSpeed * Time.deltaTime);

        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * MovementSpeed * Time.deltaTime);
        }
    }

    private void Rota()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.point - transform.position), 0.1f);
        }
    }
}
