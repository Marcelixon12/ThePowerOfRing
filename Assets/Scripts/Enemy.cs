using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] spawnPoints;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected int attack;
    protected GameObject player;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
    bool dead = false;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }

    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }
    public virtual void Move()
    {
    }
    public virtual void Attack()
    {
    }
    public void ChangeHealth(int count)
    {
        // odejmowanie zdrowia
        currentHealth -= count;
        // jeœli zdrowie spada do zera lub ni¿ej, to...
        if (currentHealth <= 0)
        {
            // zmiana wartoœci zmiennej dead, co oznacza, ¿e wywo³ania funkcji Attack i Move przestan¹ dzia³aæ
            dead = true;
            // wy³¹czanie collidera wroga
            GetComponent<Collider>().enabled = false;
            // w³¹czanie animacji œmierci
            
        }
    }
}
