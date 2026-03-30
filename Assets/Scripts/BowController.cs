using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public Transform player; // Najlepiej przypisaæ tutaj kamerê (dla celowania góra/dó³)

    public float maxForce = 50f;
    public float chargeSpeed = 10f;
    public CharacterMovement cm;

   

    float currentForce;
    public bool isCharging;

    public Animator player_anim;

    private void Start()
    {
        isCharging = false;
    }
    void Update()
    {
        // 1. Obs³uga ³adowania (logika przycisków)
        if (Input.GetButtonDown("Fire1") && cm.isCanShoot)
        {
            isCharging = true;
            currentForce = 0;
            player_anim.SetBool("Normal", false);

            player_anim.SetBool("Run", false);
            
            
            player_anim.SetBool("StopAttack", false);
            player_anim.SetBool("Walk", false);
            player_anim.SetBool("StartAttack", true);
            player_anim.SetBool("isAttacking", true);
        }

        if (isCharging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Shoot();
            isCharging = false;
            

            player_anim.SetBool("Run", false);


            player_anim.SetBool("StopAttack", false);
            player_anim.SetBool("Walk", false);
            player_anim.SetBool("StartAttack", false);
            player_anim.SetBool("Normal", true);
        }


        

    }


    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, arrowPrefab.transform.rotation);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * currentForce, ForceMode.Impulse);
        
    }

    void Metod()
    {
        player_anim.SetBool("isAttacking", true);
    }

}
