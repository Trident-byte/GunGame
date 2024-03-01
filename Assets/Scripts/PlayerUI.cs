using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject sprint;
    [SerializeField] GameObject health;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gunName;
    [SerializeField] GameObject ammoCount;
    public GameObject gun;
    private Player playerScript;
    private Gun gunStats;
    private TextMeshProUGUI weaponName;
    private TextMeshProUGUI ammoDisplay;
    private UnityEngine.UI.Image bar;
    private UnityEngine.UI.Image healthBar;
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        bar = sprint.GetComponent<UnityEngine.UI.Image>();
        healthBar = health.GetComponent<UnityEngine.UI.Image>();
        weaponName = gunName.GetComponent<TextMeshProUGUI>();
        ammoDisplay = ammoCount.GetComponent<TextMeshProUGUI>();
        gunStats = gun.GetComponent<Gun>();
    }
    void Update()
    {
        gunStats = gun.GetComponent<Gun>();
        bar.fillAmount = playerScript.sprintTime / playerScript.maxSprintTime;
        healthBar.fillAmount = playerScript.currentHealth / playerScript.maxHealth;
        weaponName.text = gunStats.gunName;
        ammoDisplay.text = gunStats.currentAmmo + "/" + gunStats.maxAmmo;
    }
}
