using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public String weaponName;
    public GameObject weaponPrefab;
    public Sprite weaponIcon;
}
