using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] Hp; // �Ѱų� �� �ڽ� ������Ʈ
    public AudioClip deathClip; //����� ����� ����� Ŭ��
    public float jumpForce = 700f; //���� ��
    public int hp = 3;

    public float normalSpeed = 5f; // ���� �ӵ�
    public float boostedSpeed = 10f; // �������� �Ծ��� ���� ������ �ӵ�

    private bool isBoosted; // �ν��� �������� �Ծ����� ���� Ȯ��
    private float boostDuration = 10f; // �ӵ� ���� ���� �ð�
    private float boostTimer; // �ӵ� ���� Ÿ�̸�

    private bool isIY; // ���� �������� �Ծ����� ���� Ȯ��
    private float MZDuration = 5f; //���� ���� �ð�
    private float MZTimer =0;// ���� Ÿ�̸�

    private int jumpCount = 0; //���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false;//��� ����

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator animator; // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ����� ����� �ҽ� ������Ʈ

    private float t;
    private float duration =10;
    // Start is called before the first frame update
    private CapsuleCollider2D capsule;

    private float mtime = 0f;
    private bool iE = false;
    void Start()
    {
       
        capsule = GetComponent<CapsuleCollider2D>();
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        for (int i = 0; i < Hp.Length; i++)
        {
            Hp[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mtime += Time.deltaTime;
        Jump();
        Slide();
        Booster();
        Invincibility();
        transparency();
    }
    private void Booster()
    {
        if (isBoosted == true)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                isBoosted = false;
                ScrollingObject.speed /= 2;

                // ���⿡ �ӵ� ���� ������ ���ư��� ���� ó�� ���� �߰��� �� �ֽ��ϴ�.
            }
        }
    }
    private void Invincibility()
    {   //isIY�� Ʈ���̸� �ߵ�
        if (isIY == true)
        {   
            //MZTimer -Time.deltaTime�̰�
            MZTimer -= Time.deltaTime;
            //MZTimer�� 0���� ������
            if (MZTimer <= 0f)
            {   
                if (t < 10f)
                {   //t�� Time.deltaTime/duration ��ŭ Ŀ����.
                    t += Time.deltaTime*4 / duration;
                    //t�� 2.5f���� ������ true;
                    if (t < 2.5f)
                    {   // Ʈ������ ���� �������� ũ�⸦ Lerp�Լ��� ����ؼ� ���������� ũŰ�� 1�� ũ�⿡�� 2�� ũ����� ���� ����(t*0.4f)�� Ŀ����.
                        transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(2f, 2f, 1f), t );
                    }
                    else if (t >= 7.5f)
                    {
                        //t�� 4.5f���� Ŀ���� ���� �÷��̾��� ũ��� (1f,1f,1f)�� �ȴ�.
                        transform.localScale = new Vector3(1f, 1f, 1f);

                        //Invincibility �Լ��� �ߵ��ϱ� ���� �ٽ� �ʱ�ȭ
                        isIY = false;
                        MZTimer = 0;
                        t = 0f;
                        iE = true;
                    }
                
                }
            }
            else
            {   //Invincibility �Լ��� �ߵ��ϱ� ���� �ٽ� �ʱ�ȭ
                isIY = false;
                t = 0f;
                //���� ����?
                MZTimer = MZDuration;
            }
        }
    }
    private void Jump()
    {
        if (isDead)
        {
            // ��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //���� Ƚ�� ����
            jumpCount++;
            // ���� ������ �ӵ��� ���������� ����(0,0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            // ������ٵ� �������� �� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // ����� �ҽ� ���
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������(���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
    }
    private void Slide()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Slide", true);
            capsule.enabled = false;
                
        }
        else if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Slide", false);
            capsule.enabled = true;
        }
    }
    private void Recovery()
    {
        if (hp < Hp.Length)
        {

            Hp[hp].SetActive(true);
            hp += 1;
        }
    }
    private void Die()
    {
        //��� ó��
        animator.SetTrigger("Die");
        //���̿� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;
        //��� ȿ���� ���
        playerAudio.Play();
        //�ӵ��� ����(0,0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;
        //���� �Ŵ����� ���ӿ��� ó�� ����
        GameManager.instance.OnPlayerDead();
    }
    private void transparency()
    {
        if (iE == true)
        {
            mtime += Time.deltaTime;
            if (mtime >= 3f)
            {
                iE = false;
                mtime = 0f;
                // ���� ���� ���� �� �ʿ��� ó�� �߰��ϱ�
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹�� ����
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
        else if (other.tag == "Recovery" && !isDead)
        {
            Recovery();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Trap" && !isDead)
        {   
            if (hp > 0 && isIY == false && iE == false)
            {
                iE = true;
                mtime = 0f;
                hp -= 1;
                Hp[hp].SetActive(false);
                if (hp == 0)
                {
                    Die();
                }
                
            }
            else if (isIY == true)
            {
                other.gameObject.SetActive(false);
            }
            
        }
        else if (other.tag == "Score" && !isDead)
        {
            GameManager.instance.AddScore(100);
            other.gameObject.SetActive(false);
        }
        else if (other.tag =="Mushroom" && !isDead)
        {
            playerRigidbody.velocity = Vector2.zero; // �÷��̾��� ���� �ӵ��� ���η� ����
            playerRigidbody.AddForce(new Vector2(0, jumpForce*1.2f)); // ���� ���� �ֱ� ���� �÷��̾��� �ӵ��� �������� �ʰ� ���� �ִ� �κ�

        }
        else if (other.tag == "Booster" && !isDead)
        {
            if (!isBoosted) // �������� �̹� ���� ���¶�� �ߺ� ó�� ����
            {
                isBoosted = true;
                boostTimer = boostDuration;
                ScrollingObject.speed *= 2;
                // ���⿡ �ӵ� ������ ���� �ð��� ȿ�� ���� �߰��� �� �ֽ��ϴ�.
            }

            //other.gameObject.SetActive(false);
        }
        else if (other.tag == "IY" && !isDead)
        {
            if (isIY==false)
            {
                isIY = true;
                Invincibility();
                other.gameObject.SetActive(false);
               
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��
        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGround�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴڿ��� ������� �����ϴ� ó��
        isGrounded = false;
    }
}
