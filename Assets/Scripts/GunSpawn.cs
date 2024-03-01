using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunSpawn : MonoBehaviour
{
    [SerializeField] float respawnTimer;
    [SerializeField] GameObject player;
    private Boolean hasWeapon;
    private float timeSincePickup;
    private Boolean playerOn;
    private Player playerScript;
    private Gun gunScript;
    private GameObject spawnedGun;
    public GameObject newGun;
    // Start is called before the first frame update
    void Start()
    {
        hasWeapon = true;
        spawnedGun = Instantiate(newGun as GameObject);
        Collider gunCollider = spawnedGun.GetComponent<Collider>();
        gunCollider.isTrigger = true;
        spawnedGun.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        playerScript = player.GetComponent<Player>();
        gunScript = spawnedGun.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWeapon)
        {
            if (timeSincePickup > respawnTimer)
            {
                hasWeapon = true;
            }
            timeSincePickup += 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && hasWeapon && playerOn)
        {
            weaponPickUp();
        }
    }

    public void weaponPickUp()
    {
        hasWeapon = false;
        playerScript.guns[playerScript.gunsIndex] = gunScript.gunName;
        Destroy(spawnedGun);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = false;
        }
    }

}
