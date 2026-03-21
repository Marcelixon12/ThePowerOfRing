using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public Transform player; // Najlepiej przypisaæ tutaj kamerê (dla celowania góra/dó³)

    public float maxForce = 50f;
    public float chargeSpeed = 10f;

    [Header("Pozycje (X: prawo/lewo, Y: góra/dó³, Z: przód/ty³)")]
    // Pozycja w rêku (celowanie)
    public Vector3 chargingOffset = new Vector3(0.4f, 1.2f, 0.4f);
    // Pozycja na plecach (spoczynek)
    public Vector3 backOffset = new Vector3(0f, 1.3f, -0.25f);

    [Header("Rotacje (stopnie)")]
    public Vector3 chargingRotationExtra = new Vector3(0, -90f, 0);
    public Vector3 backRotationExtra = new Vector3(0, 0, 90f); // Obrót ³uku p³asko na plecach

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
        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            currentForce = 0;
            player_anim.SetBool("Normal", false);

            player_anim.SetBool("Run", false);
            
            
            player_anim.SetBool("StopAttack", false);
            player_anim.SetBool("Walk", false);
            player_anim.SetBool("StartAttack", true);
            Invoke("Metod", 1);
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


        if (isCharging)
        {
            // £UK W RÊKU (CELOWANIE)
            transform.position = player.TransformPoint(chargingOffset);

            // Dodajemy rotacjê dodatkow¹ do kierunku patrzenia gracza
            transform.rotation = Quaternion.Euler(
                player.eulerAngles.x + chargingRotationExtra.x,
                player.eulerAngles.y + chargingRotationExtra.y,
                player.eulerAngles.z + chargingRotationExtra.z
            );
        }
        else
        {
            // £UK NA PLECACH
            transform.position = player.TransformPoint(backOffset);

            // Obracamy ³uk tak, aby le¿a³ p³asko na plecach gracza
            transform.rotation = Quaternion.Euler(
                player.eulerAngles.x + backRotationExtra.x,
                player.eulerAngles.y + backRotationExtra.y,
                player.eulerAngles.z + backRotationExtra.z
            );
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
