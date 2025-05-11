using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public int damage;
    public GenericPoolSO poolSO = null;
    public GameObject fireOrigin;

    public virtual void UseWeapon(GameObject user)
    {
        Debug.Log("Using a weapon");
    }
}
