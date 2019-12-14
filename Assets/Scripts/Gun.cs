using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public GameObject GunPrefab;
    public GameObject FireEffPrefab;
    public GameObject BulletPrefab;
    public Transform FirePos;
    
    [HideInInspector] public Entity BulletEntity;
    [HideInInspector] public ParticleSystem FireEffParticleSystem;


    public void Init()
    {
        GameObject FireEff = Instantiate(FireEffPrefab, FirePos);
        FireEffParticleSystem = FireEff.GetComponent<ParticleSystem>();
        FireEffParticleSystem.Stop();

        GameObjectConversionSettings gameObjectConversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        BulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, gameObjectConversionSettings);
    }
}

