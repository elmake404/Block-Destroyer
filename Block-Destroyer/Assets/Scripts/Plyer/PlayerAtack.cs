using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField]
    private MatchSystem _matchSystem;
    [SerializeField]
    private LayerMask _layerMask;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atack(Vector3.down);
        }
    }
    private void Atack(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit,1f,_layerMask))
        {
            Debug.Log(123);
           _matchSystem.TryConsumeMatch( hit.collider.GetComponent<Chip>().Cell);
        }
    }
}
