using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class PlayerFire : MonoBehaviour
{
    public float FireSpeed;
    public Gun Gun;
    private float fireTime;
    private EntityManager entityManager;

    public void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void Update()
    {
        fireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if(fireTime <= 0)
            {
                InstantiateFireEff();
                fireTime = 1 / FireSpeed;
            }
        }
    }

    private void InstantiateFireEff()
    {
        Entity fireEffEntity = entityManager.Instantiate(Gun.FireEffEntity);
        entityManager.SetComponentData<Translation>(fireEffEntity, new Translation
        {
            Value = Gun.FirePos.position
        });

        entityManager.SetComponentData<Translation>(fireEffEntity,);
    }
}

