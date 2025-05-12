using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponData> allWeapons;
}
