using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{

    public AttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.rightEye.LookAt(enemy.player.position);
        enemy.leftEye.LookAt(enemy.player.position);

        Vector3 direction = enemy.player.position - enemy.transform.position;
        direction.y = 0;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, toRotation, enemy.rotSpeed * Time.deltaTime);

        //enemy.transform.position = enemy.transform.position + (enemy.transform.forward * Time.deltaTime * enemy.moveSpeed / 3);
    }

    public void Fire()
    {

    }
}
