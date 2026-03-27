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
    public GameObject woodBow;
    public GameObject stoneBow;
    public GameObject ironBow;
    public GameObject goldBow;
    public GameObject diamondBow;
    bool isBow = false;
    Animator anim;
    public BowController bow;
    public int currentHealth = 0;
    public int maxHealth = 100;
    public  int normalDamage = 10;
    public  int stoneDamage = 15;
    public int ironDamage = 30;
    public int goldDamage = 50;
    public int diamondDamage = 100;
    public static  int currentDamage;
    public GameObject store;
    public int gold = 0;
    public int stoneBowPrice = 10;
    public int ironBowPrice = 100;
    public int goldBowPrice = 1000;
    public int diamondBowPrice = 10000;
    public UIScript ui;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = Speed;
        currentHealth = 100;
        currentDamage = normalDamage;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gold);
        Debug.Log(currentDamage);
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        direction = new Vector3(Horizontal, 0f, Vertical);
        direction = transform.TransformDirection(direction);    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            
            anim.SetBool("Walk", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("StartAttack", false);
            anim.SetBool("StopAttack", false);
            anim.SetBool("Run", false);
            anim.SetBool("Normal", false);
            anim.SetBool("Jump", true);

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (direction.z != 0f && direction.x != 0f && !bow.isCharging)
            {
                anim.SetBool("Normal", false);
                anim.SetBool("Walk", false);
                anim.SetBool("Jump", false);
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
                anim.SetBool("Jump", false);
                anim.SetBool("Run", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("StartAttack", false);
                anim.SetBool("StopAttack", false);
                anim.SetBool("Walk", true);
            }
            
            
            
            else if (direction.z == 0f && direction.x == 0f && !bow.isCharging)
            {
                anim.SetBool("Jump", false);
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
            woodBow.SetActive(true);
            stoneBow.SetActive(false);  
            ironBow.SetActive(false);
            goldBow.SetActive(false);
            diamondBow.SetActive(false);
        }
        if (other.gameObject.CompareTag("Store"))
        {
            ui.SetActive();
            Cursor.lockState = CursorLockMode.None;
            // Upewnienie siê, ¿e kursor jest widoczny
            Cursor.visible = true;
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Store"))
        {
            ui.NoSetActive();
            Cursor.lockState = CursorLockMode.Locked;
            // Upewnienie siê, ¿e kursor jest widoczny
            Cursor.visible = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    
}

