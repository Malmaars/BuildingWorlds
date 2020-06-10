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

        foreach(Transform eye in enemy.Eyes)
        {
            eye.transform.LookAt(enemy.player.position);
        }

        Vector3 direction = enemy.player.position - enemy.transform.position;
        direction.y = 0;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, toRotation, enemy.rotSpeed * Time.deltaTime);

        enemy.transform.position = enemy.transform.position + (enemy.transform.forward * Time.deltaTime * enemy.moveSpeed);
    }

    public void Fire()
    {

    }
}
