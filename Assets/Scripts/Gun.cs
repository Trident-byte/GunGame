using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float maxAmmo;
    public float currentAmmo;
    public float timeBetweenShots;
    public float fireRate;
    public float timeToReload;
    public float damage;
    public Boolean automatic;
    public Transform barrel;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShots += 1;
    }
}
