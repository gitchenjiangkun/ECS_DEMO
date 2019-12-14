using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Entities;

public class GameManager:MonoBehaviour
{
    [Header("ECS")]
    public bool UseECS;
    [HideInInspector] public EntityManager EntityManager;

    [Header("Gun")]
    public Gun Gun;
    public GameObject GunPos;
    public int BulletNum = 5;
    public float BulletSpeed = 2f;
    public float BulletLifeTime = 2f;

    [Header("Movement")]
    public float MovementSpeed = 5f;
    public float RotatingSpeed = 0.1f;

    [Header("Fire")]
    public float FireSpeed = 5;

    private void Start()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void SetGun(Gun gun)
    {
        if (Gun) Destroy(GunPos.transform.GetChild(0).gameObject);

        Gun = Instantiate(gun.GunPrefab, GunPos.transform).GetComponent<Gun>();
        Gun.Init();
    }
}
