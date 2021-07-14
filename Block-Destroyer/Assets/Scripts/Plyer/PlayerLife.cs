using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Chip chip = other.GetComponent<Chip>(); 
        if (chip!=null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
