using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _drill;
    private Quaternion _rotationModel, _rotationDril;
    [SerializeField]
    private float _speedRotationModel, _speedRotationDrill;
    private void Start()
    {
        StandStraight();
    }
    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation,_rotationModel,_speedRotationModel);
        _drill.localRotation = Quaternion.Slerp(_drill.localRotation,_rotationDril,_speedRotationDrill);
    }

    public void StandStraight()
    {
       _rotationModel= Quaternion.Euler(new Vector3(0, 180, 0));
        _rotationDril = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    public void TurnSideways(int side)
    {
        _rotationModel = Quaternion.Euler(new Vector3(0, 90*side, 0));
        _rotationDril = Quaternion.Euler(new Vector3(-90, 0, 0));
    }
}
