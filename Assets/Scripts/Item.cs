using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject GunPrefab;
    public Gun Gun;

    private void Awake()
    {
        Gun = new AK47();
    }

    private void Start()
    {
        GunPrefab = Instantiate(GunPrefab, transform);
        Gun = GunPrefab.GetComponent<Gun>();
        Gun.Init();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            Gun playerGun = player.GetComponent<PlayerFire>().Gun;
            if (playerGun)
            {
                player.GetComponent<PlayerFire>().Gun = Gun;
                Gun = playerGun;

                GameObject playerGunObj = player.transform.Find("Gun").GetChild(0).gameObject;
                GunPrefab.transform.SetParent(player.transform.Find("Gun"));
                playerGunObj.transform.SetParent(transform);
                GunPrefab.transform.localPosition = Vector3.zero;
                GunPrefab.transform.rotation = new Quaternion();
            }
            else
            {
                player.GetComponent<PlayerFire>().Gun = Gun;
                GunPrefab.transform.SetParent(player.transform.Find("Gun"));
                GunPrefab.transform.localPosition = Vector3.zero;
                GunPrefab.transform.rotation = new Quaternion();
                Destroy(gameObject);
            }
        }
    }
}
