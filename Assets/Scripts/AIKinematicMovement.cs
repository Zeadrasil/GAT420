using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKinematicMovement : AIMovement
{
    public override void ApplyForce(Vector3 force)
    {
        Acceleration += force;
    }

    public override void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        ApplyForce(direction * maxForce);
    }

    public override void Stop()
    {
        Velocity = Vector3.zero;
    }

    public override void Resume()
    {
        //
    }

    void LateUpdate()
    {
        Velocity += Acceleration * Time.deltaTime;
        //Velocity = Velocity.ClampMagnitude(minSpeed, maxSpeed);
        transform.position += Velocity * Time.deltaTime;

        if (Velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }

        Acceleration = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
