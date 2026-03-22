using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 7f;
    float currentSpeed;
    public float shiftSpeed = 14f;
    public float jumpForce = 6f;
    float stamina = 5f;
    Rigidbody rb;
    Vector3 direction;
    bool isGrounded = false;
    public GameObject Bow;
    bool isBow = false;
    Animator anim;
    public BowController bow;
    public int currentHealth = 0;
    public int maxHealth = 100;
    public static int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = Speed;
        currentHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        direction = new Vector3(Horizontal, 0f, Vertical);
        direction = transform.TransformDirection(direction);    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (direction.z != 0f && direction.x != 0f && !bow.isCharging)
            {
                anim.SetBool("Normal", false);
                anim.SetBool("Walk", false);

                anim.SetBool("isAttacking", false);
                anim.SetBool("StartAttack", false);
                anim.SetBool("StopAttack", false);
                anim.SetBool("Run", true);
            }
            

            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
                currentSpeed = Speed;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = Speed;
            if (direction.z != 0f && direction.x != 0f && !bow.isCharging)
            {
                anim.SetBool("Normal", false);
                
                anim.SetBool("Run", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("StartAttack", false);
                anim.SetBool("StopAttack", false);
                anim.SetBool("Walk", true);
            }
            
            
            
            else if (direction.z == 0f && direction.x == 0f && !bow.isCharging)
            {

                anim.SetBool("Walk", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("StartAttack", false);
                anim.SetBool("StopAttack", false);
                anim.SetBool("Run", false);
                anim.SetBool("Normal", true);
            }
        }

        else
        {
            currentSpeed = Speed;
        }
        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
            isGrounded = true;          
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bow") && isBow == false)
        {
            Destroy(other.gameObject);
            isBow = true;
            Bow.SetActive(true);
        }
    }
    public void ChangeHealth(int count)
    {
        // odejmowanie zdrowia
        currentHealth -= count;
        // jeœli zdrowie spada do zera lub ni¿ej, to...
        if (currentHealth <= 0)
        {
            // zmiana wartoœci zmiennej dead, co oznacza, ¿e wywo³ania funkcji Attack i Move przestan¹ dzia³aæ
            Time.timeScale = 0;
            // wy³¹czanie collidera wroga
            
            // w³¹czanie animacji œmierci

        }
    }
}

