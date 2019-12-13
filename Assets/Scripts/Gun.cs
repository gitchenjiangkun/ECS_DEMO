using UnityEngine;
using Unity.Entities;

public class Gun : MonoBehaviour
{
    public GameObject GunPrefab;
    public GameObject FireEffPrefab;
    public GameObject BulletPrefab;
    public Transform FirePos;

    public Entity FireEffEntity;
    public Entity BulletEntity;
    
    public void Init()
    {
        GameObjectConversionSettings gameObjectConversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        FireEffEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(FireEffPrefab, gameObjectConversionSettings);
        BulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, gameObjectConversionSettings);
    }
}
