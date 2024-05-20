using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    Rigidbody rb;
    Vector3 movementInput;
    bool frenoMano;

    [Header("Partículas")]
    [SerializeField] ParticleSystem forwardP;
    [SerializeField] ParticleSystem rightP;
    [SerializeField] ParticleSystem leftP;

    bool isForwardPlaying;
    bool isRightPlaying;
    bool isLeftPlaying;

    [Header("Giro")]
    [SerializeField] Vector3 giroNormal;
    [SerializeField] Vector3 giroFreno;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CheckParticles();
    }

    void Update()
    {
        HandleInput();
        UpdateParticleEffects();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        movementInput = Vector3.zero;
        frenoMano = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.W))
        {
            movementInput.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementInput.z -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x -= 1;
        }
    }

    void UpdateParticleEffects()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isForwardPlaying)
            {
                forwardP.Play();
                isForwardPlaying = true;
            }
        }
        else
        {
            if (isForwardPlaying)
            {
                forwardP.Stop();
                isForwardPlaying = false;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!isRightPlaying)
            {
                rightP.Play();
                isRightPlaying = true;
            }
        }
        else
        {
            if (isRightPlaying)
            {
                rightP.Stop();
                isRightPlaying = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!isLeftPlaying)
            {
                leftP.Play();
                isLeftPlaying = true;
            }
        }
        else
        {
            if (isLeftPlaying)
            {
                leftP.Stop();
                isLeftPlaying = false;
            }
        }
    }

    void CheckParticles()
    {
        isForwardPlaying = forwardP.isPlaying;
        isRightPlaying = rightP.isPlaying;
        isLeftPlaying = leftP.isPlaying;
    }

    void HandleMovement()
    {
        if (movementInput != Vector3.zero)
        {
            Vector3 desiredVelocity = movementInput.z * stats.acceleration * transform.forward;
            rb.velocity += desiredVelocity;

            float angularVelocity = 0;
            if (movementInput.x != 0)
            {
                angularVelocity = stats.accelerationAng * movementInput.x;
                rb.angularVelocity = Vector3.up * Mathf.Clamp(rb.angularVelocity.y + angularVelocity, -stats.maxAngularSpeed, stats.maxAngularSpeed);
            }
        }
        else
        {
            rb.velocity = new Vector3(
                rb.velocity.x / (1 + stats.friction * Time.deltaTime),
                rb.velocity.y,
                rb.velocity.z / (1 + stats.friction * Time.deltaTime)
            );

            rb.angularVelocity = Vector3.up * Mathf.Clamp(rb.angularVelocity.y, -stats.maxAngularSpeed, stats.maxAngularSpeed);
        }

        if (rb.velocity.magnitude > stats.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * stats.maxSpeed;
        }

        if (frenoMano)
        {
            rb.velocity = new Vector3(
                rb.velocity.x / (1 + stats.brakeStrength * Time.deltaTime),
                rb.velocity.y,
                rb.velocity.z / (1 + stats.brakeStrength * Time.deltaTime)
            );
        }

        if (movementInput.x != 0)
        {
            float giro = frenoMano ? giroFreno.y : giroNormal.y;
            rb.angularVelocity = new Vector3(
                rb.angularVelocity.x,
                rb.angularVelocity.y + (movementInput.x > 0 ? giro : -giro)
            );
        }

        if (rb.velocity.magnitude < 0.1f)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
