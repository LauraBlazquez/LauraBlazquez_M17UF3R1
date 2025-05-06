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
            transform.eulerAngles = Vector3.Lerp(closedPos, openedPos, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.eulerAngles = Vector3.Lerp(openedPos, closedPos, 1f);
        }
    }

}
