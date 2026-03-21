using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public Transform player;
    public float maxForce = 50f;
    public float chargeSpeed = 10f;

    float currentForce;
    bool isCharging;
    public Vector3 idleOffset = new Vector3(0, 1.112f, -0.189f);
    public Vector3 chargingOffset = new Vector3(0.875f, 1.275f, 0.394f);
    public Animator player_anim;

    private void Start()
    {
        isCharging = false;
    }
    void Update()
    {
        // 1. Obs³uga ³adowania (logika przycisków)
        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            currentForce = 0;
            player_anim.SetBool("Normal", false);

            player_anim.SetBool("Run", false);
            
            
            player_anim.SetBool("StopAttack", false);
            player_anim.SetBool("Walk", false);
            player_anim.SetBool("StartAttack", true);
            Invoke("Metod", 2);
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

        // 2. AKTUALIZACJA POZYCJI I ROTACJI (zawsze w Update, aby ³uk œledzi³ ruch)
        if (isCharging)
        {
            // Ustawia ³uk w pozycji "bojowej" wzglêdem kierunku patrzenia gracza
            Vector3 chargingOffset = new Vector3(0, 1.275f, 0.394f);
            transform.position = player.TransformPoint(chargingOffset);

            // Rotacja: patrzy tam gdzie gracz + poprawka -90 stopni na osi Y
            transform.rotation = Quaternion.Euler(player.eulerAngles.x, player.eulerAngles.y - 90f, player.eulerAngles.z);
        }
        else
        {
            // Ustawia ³uk w pozycji "spoczynkowej" wzglêdem kierunku patrzenia gracza
            Vector3 idleOffset = new Vector3(0, 1.112f, -0.189f);
            transform.position = player.TransformPoint(idleOffset);

            // Rotacja: identyczna jak u gracza
            transform.rotation = player.rotation;
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
