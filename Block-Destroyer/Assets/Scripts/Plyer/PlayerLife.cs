using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private void OnTriggerEnter(Collider other)
    {
        Chip chip = other.GetComponent<Chip>(); 
        if (chip!=null||other.gameObject.layer==10)
        {
            Death();
        }
    }
    private void Death()
    {
        GameStage.Instance.ChangeStage(Stage.LostGame);
        StartCoroutine(ActivationAnimation());
        enabled = false;

    }
    private IEnumerator ActivationAnimation()
    {
        _animator.SetBool("Death",true);
        yield return new WaitForSeconds(0.02f);
        _animator.SetBool("Death", false);
    }
}
