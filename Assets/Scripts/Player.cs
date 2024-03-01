using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public Transform temp_barrel;
    public float sprintTime;
    public float maxHealth;
    public float currentHealth;
    public float maxSprintTime;
    public string[] guns;
    public int gunsIndex;
    [SerializeField] GameObject currentWeapon;
    [SerializeField] float dashForce;
    [SerializeField] float speed;
    [SerializeField] GameObject hand;
    private float ylook;
    private Gun gunStats;
    private Boolean canSprint;
    private Hands handScript;
    private float waitTime;

    void Start()
    {
        guns = new string[2];
        gunsIndex = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        maxSprintTime = sprintTime;
        // currentHealth = maxHealth;
        gunStats = currentWeapon.GetComponent<Gun>();
        handScript = hand.GetComponent<Hands>();
        guns[0] = "Pistol";
        guns[1] = "Rifle";
        temp_barrel = gunStats.barrel;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        Movement();
        Shooting();


        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
        }

        //Switches Weapons
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunsIndex = (gunsIndex + 1) % 2;
            switchGuns();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switchGuns();
        }
    }

    //Allows player to move camera by moving their cursor
    void MoveCamera()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
            ylook = Mathf.Clamp(ylook - Input.GetAxis("Mouse Y"), -90, 90);
            cam.localRotation = Quaternion.Euler(ylook, 0, 0);
        }
    }

    //Enables shooting both semi and auto + reloading
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0) && gunStats.automatic && gunStats.timeBetweenShots > waitTime)
        {
            if (gunStats.currentAmmo > 0)
            {
                Projectile.CreateProjectile(temp_barrel.position, transform.forward * 20f, gunStats.damage);
                gunStats.currentAmmo -= 1;
                waitTime = gunStats.fireRate;
            }
            else
            {
                gunStats.currentAmmo = gunStats.maxAmmo;
                waitTime = gunStats.timeToReload;
            }
            gunStats.timeBetweenShots = 0;
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            gunStats.currentAmmo = gunStats.maxAmmo;
            waitTime = gunStats.timeToReload;
        }
    }

    //Moves player around the scene
    void Movement()
    {
        if (sprintTime <= 0)
        {
            Debug.Log("can't sprint");
            canSprint = false;
        }
        else if (sprintTime == maxSprintTime)
        {
            canSprint = true;
        }

        Vector2 kbInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (kbInput.magnitude > 1)
        {
            kbInput = kbInput.normalized;
        }

        float yvel = rb.velocity.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            yvel = 10;
        }


        if (Input.GetKey(KeyCode.LeftShift) && sprintTime > 0 && canSprint)
        {
            speed = 25;
            sprintTime -= 1;
        }
        else
        {
            speed = 10;
            if (sprintTime < maxSprintTime)
            {
                sprintTime += 1;
            }
        }

        rb.velocity = kbInput.x * transform.right * speed + kbInput.y * transform.forward * speed;
        rb.velocity += yvel * Vector3.up;
    }

    private void switchGuns()
    {
        currentWeapon = handScript.SwitchGuns(guns[gunsIndex]);
        gunStats = currentWeapon.GetComponent<Gun>();
        temp_barrel = gunStats.barrel;
        Debug.Log(currentWeapon);
        GameObject playerUI = GameObject.Find("PlayerUI");
        PlayerUI updated = playerUI.GetComponent<PlayerUI>();
        updated.gun = currentWeapon;
    }
}
