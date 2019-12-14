using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gun,
    Bullet
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
    
    private void Start()
    {
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch (ItemType)
            {
                case ItemType.Gun:
                    Gun gun = transform.GetChild(0).GetComponent<Gun>();
                    other.gameObject.GetComponent<GameManager>().SetGun(gun);
                    Destroy(gameObject);
                    break;
                case ItemType.Bullet:
                    break;
            }
        }
    }
}
