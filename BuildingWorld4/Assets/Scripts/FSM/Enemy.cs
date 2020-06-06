using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyStateMachine EnemySM;
    public WanderState wander;
    public AttackState attack;

    public Transform player;
    public LayerMask enemyLayer;

    public Transform[] Eyes;

    public int health;
    public GameObject deathEnemy;

    public float maxAngle;
    public float maxRadius;
    public float viewRange;
    public float moveSpeed;
    public float rotSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        EnemySM = new EnemyStateMachine();
        wander = new WanderState(this, EnemySM);
        attack = new AttackState(this, EnemySM);
        enemyLayer = LayerMask.GetMask("Enemy");

        EnemySM.Initialize(wander);
    }

    // Update is called once per frame
    private void Update()
    {
        EnemySM.CurrentState.LogicUpdate();
        //Debug.Log(EnemySM.CurrentState);
    }
    private void FixedUpdate()
    {
        EnemySM.CurrentState.PhysicsUpdate();
    }

    public void Die()
    {
        //Instantiate(deathEnemy, transform.position, transform.rotation);
        Destroy(this.transform.parent.gameObject);
    }
}
