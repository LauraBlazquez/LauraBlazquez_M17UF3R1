using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Vector3 closedPos, openedPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).localRotation = Quaternion.Euler(openedPos);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).localRotation = Quaternion.Euler(closedPos);
        }
    }

}
