using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField]
    private MatchSystem _matchSystem;
    [SerializeField]
    private LayerMask _layerMask;
    private PlayerMove _playerMove;

    [SerializeField]
    private float _timeBeforeAttack;
    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        //StartCoroutine(Shooting());
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atack(_playerMove.DirectionTravel);
        }
    }
    private void Atack(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit,1f,_layerMask))
        {
           _matchSystem.TryConsumeMatch( hit.collider.GetComponent<Chip>().Cell);
        }
    }
    private IEnumerator Shooting()
    {
        while (true)
        {
            Atack(_playerMove.DirectionTravel);
            yield return new WaitForSeconds(_timeBeforeAttack);
        }
    }
}
