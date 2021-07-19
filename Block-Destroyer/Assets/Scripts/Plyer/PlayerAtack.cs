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
    private float _radiusAtack, _timeBeforeAttack,_attackDamage,_attackDistence=0.8f;
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        GameStageEvent.StartLevel += StartAtack;
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Atack(_playerMove.DirectionTravel);
    //    }
    //}
    private void StartAtack()
    {
        GameStageEvent.StartLevel -= StartAtack;

        StartCoroutine(Shooting());
    }
    private void Atack(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,_radiusAtack, direction, out hit, _attackDistence - _radiusAtack,_layerMask))
        {
           _matchSystem.StartTryConsumeMatch( hit.collider.GetComponent<Chip>().Cell,_attackDamage);
        }
    }
    private IEnumerator Shooting()
    {
        while (GameStage.IsGameFlowe)
        {
            yield return new WaitForSeconds(_timeBeforeAttack);
            Atack(_playerMove.DirectionTravel);
        }
    }
}
