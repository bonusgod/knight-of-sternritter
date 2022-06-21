using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    private void Start()
    { }

    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jumpForce = 15f;
    public bool isGrounded = false;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHearts;
    [SerializeField] private Sprite deadHearts;
    [SerializeField] GameObject losePanel;
    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; private set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        isRecharged = true;
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded && health > 0) State = States.jump;
    }
    public enum States
    {
        idle,
        run,
        jump,
        attack,
        death
    }
    private void Update()
    {
        if (isGrounded && !isAttacking && health > 0) State = States.idle;
        if (Input.GetButton("Horizontal") && health > 0 && !isAttacking)
            Run();
        if (!isAttacking && isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();

        if (health > lives)
            health = lives;

        for (int j = 0; j < hearts.Length; j++)
            {
                if (j < health)
                    hearts[j].sprite = aliveHearts;
                else 
                    hearts[j].sprite = deadHearts;

                if (j < lives)
                    hearts[j].enabled = true;
                else 
                    hearts[j].enabled = false;
            }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    

    public override void GetDamage()
    {
        health -= 1;
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHearts;
            Die();
        }   
    }

    public override void Die()
    {
        State = States.death;
        Invoke("SetLosePanel", 1.1f);
        Destroy(this.gameObject);
    }

    private void SetLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    public void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(2f);
        isRecharged = true;
    }
}