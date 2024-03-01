using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public GameObject SwitchGuns(string newGun)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        GameObject newGunType = Instantiate(Resources.Load(newGun) as GameObject);
        newGunType.transform.SetParent(transform, false);
        return newGunType;
    }
}
