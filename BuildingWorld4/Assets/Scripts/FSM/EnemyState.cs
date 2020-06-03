using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected bool isInFov;
    protected EnemyState(Enemy enemy,EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {
        if (isInFov)
            Debug.Log("I can see youu");

        if(enemy.health <= 0)
        {
            //Die. This doesn't really need a state, because the enemy will stop being
            enemy.Die();
        }
    }

    public virtual void PhysicsUpdate()
    {
        isInFov = inFOV(enemy.transform, enemy.player, enemy.maxAngle, enemy.maxRadius);
    }

    public virtual void Exit()
    {

    }

    public virtual bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {

        Collider[] overlaps = Physics.OverlapSphere(enemy.transform.position, enemy.maxRadius);

        foreach (Collider overlap in overlaps)
        {
            if (overlap != null)
            {
                if (overlap.transform == target)
                { 
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        { 
                            if (hit.transform == target)
                                return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
