using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomousAgent : AIAgent
{
    public AIPerception evadePerception = null;
	public AIPerception seekPerception = null;
	public AIPerception flockPerception = null;
    [SerializeField] private float seperationDistance = 5f;
    private GameObject lastFleed = null;

    private void Update()
    {
        if (seekPerception != null)
        {
            var detected = seekPerception.getGameObjects();
            //foreach(var obj in detected)
            //{
            //    Debug.DrawLine(transform.position, obj.transform.position, Color.red);

            //}
            if (detected.Length > 0)
            {
                movement.ApplyForce(seek(detected[0]));
            }
		}
		if (flockPerception != null)
		{
			var detected = flockPerception.getGameObjects();
			if (detected.Length > 0)
			{
				movement.ApplyForce(cohesion(detected));
                movement.ApplyForce(alignment(detected));
                movement.ApplyForce(seperation(detected, seperationDistance));
			}
		}
		if (evadePerception != null)
        {
			var detected = evadePerception.getGameObjects();
			//foreach(var obj in detected)
			//{
			//    Debug.DrawLine(transform.position, obj.transform.position, Color.red);

			//}
			if (detected.Length > 0 && (lastFleed == null ||(detected[0].transform.position - transform.position).magnitude <= (lastFleed.transform.position - transform.position).magnitude))
			{
				movement.ApplyForce(flee(detected[0]));
                lastFleed = detected[0];
			}
            else if(lastFleed != null)
            {
                if((lastFleed.transform.position - transform.position).magnitude < evadePerception.getDistance)
                {
                    movement.ApplyForce(flee(lastFleed));
                }
                else
                {
                    lastFleed = null;
                }
            }
		}
        transform.position = Utilities.wrap(transform.position, -50, 50);
    }

    private Vector3 getSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        return Vector3.ClampMagnitude(steer, movement.maxForce);
    }

	private Vector3 seek(GameObject target)
	{
		return getSteeringForce(target.transform.position - transform.position);
	}
	private Vector3 flee(GameObject target)
	{
		return getSteeringForce(transform.position - target.transform.position);
	}

    private Vector3 cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach(var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }
        Vector3 center = positions / neighbors.Length;
        return getSteeringForce(center - transform.position);
    }
    private Vector3 seperation(GameObject[] neighbors, float radius)
    {
        Vector3 seperation = Vector3.zero;
        foreach(var neighbor in neighbors)
        {
            Vector3 direction = transform.position -  neighbor.transform.position;
            if(direction.magnitude < radius)
            {
                seperation += direction / (direction.sqrMagnitude + 0.001f);
            }
        }
        return getSteeringForce(seperation);
    }

    private Vector3 alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;
        foreach(var neighbor in neighbors)
        {
            velocities += neighbor.GetComponent<AIAgent>().movement.Velocity;
        }
        return getSteeringForce(velocities / neighbors.Length);
    }
}
