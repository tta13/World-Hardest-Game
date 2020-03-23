using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBallScript : CharacterScript
{
    [SerializeField]
    private float rotationRate;

    public override void Move()
    {
        transform.Rotate(Vector3.up * rotationRate * Time.fixedDeltaTime);
    }
}
