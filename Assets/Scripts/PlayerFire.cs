using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

public class PlayerFire : MonoBehaviour
{
    private GameManager gameManager;
    private float fireTime;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        fireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && gameManager.Gun != null)
        {
            if(fireTime <= 0)
            {
                FireEff();

                if (gameManager.UseECS)
                    if (gameManager.BulletNum > 1) SpawnBulletSpreadECS();
                    else BulletECS();
                else
                    if (gameManager.BulletNum > 1) SpawnBulletSpread();
                    else Bullet();

                fireTime = 1 / gameManager.FireSpeed;
            }
        }
    }

    private void FireEff()
    {
        gameManager.Gun.FireEffParticleSystem.Play();
    }

    private void Bullet()
    {
        GameObject bullet = Instantiate(gameManager.Gun.BulletPrefab);
        bullet.transform.position = gameManager.Gun.FirePos.position;
        bullet.transform.rotation = gameManager.Gun.FirePos.rotation;
    }

    private void SpawnBulletSpread()
    {
        int max = gameManager.BulletNum / 2;
        int min = -max;

        Vector3 tempRot = gameManager.Gun.FirePos.rotation.eulerAngles;
        for (int x = min; x < max; x++)
        {
            tempRot.x = (gameManager.Gun.FirePos.rotation.eulerAngles.x + 3 * x) % 360;

            for (int y = min; y < max; y++)
            {
                tempRot.y = (gameManager.Gun.FirePos.rotation.eulerAngles.y + 3 * y) % 360;

                GameObject bullet = Instantiate(gameManager.Gun.BulletPrefab) as GameObject;

                bullet.transform.position = gameManager.Gun.FirePos.position;
                bullet.transform.rotation = Quaternion.Euler(tempRot);
            }
        }
    }

    private void BulletECS()
    {
        Entity bullet = gameManager.EntityManager.Instantiate(gameManager.Gun.BulletEntity);

        gameManager.EntityManager.SetComponentData(bullet, new Translation { Value = gameManager.Gun.FirePos.position });
        gameManager.EntityManager.SetComponentData(bullet, new Rotation { Value = gameManager.Gun.FirePos.rotation });
        gameManager.EntityManager.AddComponentData(bullet, new BulletMoveComponent { Speed = gameManager.BulletSpeed });
    }

    private void SpawnBulletSpreadECS()
    {
        int max = gameManager.BulletNum / 2;
        int min = -max;
        int totalAmount = gameManager.BulletNum * gameManager.BulletNum;

        Vector3 tempRot = gameManager.Gun.FirePos.rotation.eulerAngles;
        int index = 0;

        NativeArray<Entity> bullets = new NativeArray<Entity>(totalAmount, Allocator.TempJob);
        gameManager.EntityManager.Instantiate(gameManager.Gun.BulletEntity, bullets);

        for (int x = min; x < max; x++)
        {
            tempRot.x = (gameManager.Gun.FirePos.rotation.eulerAngles.x + 3 * x) % 360;

            for (int y = min; y < max; y++)
            {
                tempRot.y = (gameManager.Gun.FirePos.rotation.eulerAngles.y + 3 * y) % 360;

                gameManager.EntityManager.SetComponentData(bullets[index], new Translation { Value = gameManager.Gun.FirePos.position });
                gameManager.EntityManager.SetComponentData(bullets[index], new Rotation { Value = Quaternion.Euler(tempRot) });
                gameManager.EntityManager.AddComponentData(bullets[index], new BulletMoveComponent { Speed = gameManager.BulletSpeed });

                index++;
            }
        }
        bullets.Dispose();
    }
}

