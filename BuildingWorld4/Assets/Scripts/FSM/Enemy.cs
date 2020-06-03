using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyStateMachine EnemySM;
    public WanderState wander;

    public Transform player;
    public LayerMask enemyLayer;

    public int health;
    public GameObject deathEnemy;

    public float maxAngle;
    public float maxRadius;
    public float viewRange;
    public float moveSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        EnemySM = new EnemyStateMachine();
        wander = new WanderState(this, EnemySM);
        enemyLayer = LayerMask.GetMask("Enemy");

        EnemySM.Initialize(wander);
    }

    // Update is called once per frame
    private void Update()
    {
        EnemySM.CurrentState.LogicUpdate();
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
