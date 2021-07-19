using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Chip chip = other.GetComponent<Chip>(); 
        if (chip!=null||other.gameObject.layer==10)
        {
            Destroy(transform.parent.gameObject);
        }
        if (other.tag =="Finish")
        {
            GameStage.Instance.ChangeStage(Stage.WinGame);
        }
    }
}
