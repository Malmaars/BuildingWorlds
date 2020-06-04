using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : EnemyState
{
    public int leftOrRight;
    public WanderState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    { 
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

        enemy.rightEye.transform.localRotation = new Quaternion(0, 0, 0, 0);
        enemy.leftEye.transform.localRotation = new Quaternion(0, 0, 0, 0);

        Debug.DrawRay(enemy.transform.position, enemy.transform.forward * enemy.viewRange, Color.red);

        Vector3 fovLine1 = Quaternion.AngleAxis(45, enemy.transform.up) * enemy.transform.forward * enemy.viewRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-45, enemy.transform.up) * enemy.transform.forward * enemy.viewRange;
        Debug.DrawRay(enemy.transform.position, fovLine1, Color.red);
        Debug.DrawRay(enemy.transform.position, fovLine2, Color.red);
        if (!Physics.Raycast(enemy.transform.position, enemy.transform.forward, enemy.viewRange, ~enemy.enemyLayer) &&
            !Physics.Raycast(enemy.transform.position, fovLine1, ~enemy.enemyLayer) && 
            !Physics.Raycast(enemy.transform.position, fovLine2, ~enemy.enemyLayer))
        {
            //enemy.transform.Translate(-enemy.transform.right * enemy.moveSpeed * Time.deltaTime, enemy.transform);
            enemy.transform.position = enemy.transform.position + (enemy.transform.forward * Time.deltaTime * enemy.moveSpeed);
            leftOrRight = (int)Random.Range(0, 2);

        }

        if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, enemy.viewRange, ~enemy.enemyLayer))
        {
            if(leftOrRight == 0)
            enemy.transform.Rotate(new Vector3(0, 1, 0), Space.World);

            if (leftOrRight == 1)
                enemy.transform.Rotate(new Vector3(0, -1, 0), Space.World);
        }

        else if(Physics.Raycast(enemy.transform.position, fovLine1, enemy.viewRange, ~enemy.enemyLayer))
        {
            enemy.transform.Rotate(new Vector3(0, -1, 0), Space.World);
        }

        else if (Physics.Raycast(enemy.transform.position, fovLine2, enemy.viewRange, ~enemy.enemyLayer))
        {
            enemy.transform.Rotate(new Vector3(0, 1, 0), Space.World);
        }
    }
}
