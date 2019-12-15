using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Rendering;

public class Gun : MonoBehaviour
{
    public GameObject GunPrefab;
    public GameObject BulletPrefab;
    public GameObject FireEff;
    public Transform FirePos;

    [HideInInspector] public Entity BulletEntity;
    [HideInInspector] public ParticleSystem FireEffParticleSystem;


    public void Init()
    {
        FireEff.SetActive(true);
        FireEffParticleSystem = FireEff.GetComponent<ParticleSystem>();
        FireEffParticleSystem.Stop();

        GameObjectConversionSettings gameObjectConversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        BulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, gameObjectConversionSettings);

        var renderMesh = World.DefaultGameObjectInjectionWorld.EntityManager.GetSharedComponentData<RenderMesh>(BulletEntity);
        
        renderMesh.mesh = BulletPrefab.GetComponent<MeshFilter>().sharedMesh;
        renderMesh.material = BulletPrefab.GetComponent<Renderer>().sharedMaterial;

        World.DefaultGameObjectInjectionWorld.EntityManager.SetSharedComponentData(BulletEntity, renderMesh);
    }
}

