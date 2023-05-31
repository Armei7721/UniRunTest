using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] Hp; // 켜거나 끌 자식 오브젝트
    public AudioClip deathClip; //사망시 재생할 오디오 클립
    public float jumpForce = 700f; //점프 힘
    public int hp = 3;

    public float normalSpeed = 5f; // 원래 속도
    public float boostedSpeed = 10f; // 아이템을 먹었을 때의 증가된 속도

    private bool isBoosted; // 부스터 아이템을 먹었는지 여부 확인
    private float boostDuration = 10f; // 속도 증가 지속 시간
    private float boostTimer; // 속도 증가 타이머

    private bool isIY; // 무적 아이템을 먹었는지 여부 확인
    private float MZDuration = 5f; //무적 지속 시간
    private float MZTimer =0;// 무적 타이머

    private int jumpCount = 0; //누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false;//사망 상태

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    public float t;
    public float duration =10;
    // Start is called before the first frame update
    public BoxCollider2D box;
    public CapsuleCollider2D capsule;

    public float mtime = 0f;
    public bool iE = false;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
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

                // 여기에 속도 원래 값으로 돌아갔을 때의 처리 등을 추가할 수 있습니다.
            }
        }
    }
    private void Invincibility()
    {   //isIY가 트루이면 발동
        if (isIY == true)
        {   
            //MZTimer -Time.deltaTime이고
            MZTimer -= Time.deltaTime;
            //MZTimer가 0보다 작으면
            if (MZTimer <= 0f)
            {   
                if (t < 10f)
                {   //t가 Time.deltaTime/duration 만큼 커진다.
                    t += Time.deltaTime / duration;
                    //t가 2.5f보다 작으면 true;
                    if (t < 1.5f)
                    {   // 트랜스폼 로컬 스케일의 크기를 Lerp함수를 사용해서 점진적으로 크키가 1의 크기에서 3의 크기까지 보간 비율(t*0.4f)로 커진다.
                        transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(3f, 3f, 1f), t * 0.5f);
                    }
                    else if (t >= 9.5f)
                    {
                        //t가 4.5f보다 커지면 최종 플레이어의 크기는 (1f,1f,1f)가 된다.
                        transform.localScale = new Vector3(1f, 1f, 1f);

                        //Invincibility 함수를 발동하기 위해 다시 초기화
                        isIY = false;
                        MZTimer = 0;
                        t = 0f;
                    }
                
                }
            }
            else
            {   //Invincibility 함수를 발동하기 위해 다시 초기화
                isIY = false;
                t = 0f;
                //아직 몰루?
                MZTimer = MZDuration;
            }
        }
    }
    private void Jump()
    {
        if (isDead)
        {
            // 사망 시 처리를 더 이상 진행하지 않고 종료
            return;
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //점프 횟수 증가
            jumpCount++;
            // 점프 직전에 속도를 순간적으로 제로(0,0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // 오디오 소스 재생
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
    }
    private void Slide()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Slide", true);
            box.enabled = true;
            capsule.enabled = false;
                
        }
        else if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Slide", false);
            box.enabled = false;
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
        //사망 처리
        animator.SetTrigger("Die");
        //오이오 소스에 할당된 오디오 클립을 deathClip으로 변경
        playerAudio.clip = deathClip;
        //사망 효과음 재생
        playerAudio.Play();
        //속도를 제로(0,0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;
        //게임 매니저의 게임오버 처리 실행
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
                Debug.Log("확인");
                // 무적 상태 해제 후 필요한 처리 추가하기
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //트리거 콜라이더를 가진 장애물과의 충돌을 감지
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
        else if (other.tag == "Booster" && !isDead)
        {
            if (!isBoosted) // 아이템을 이미 먹은 상태라면 중복 처리 방지
            {
                isBoosted = true;
                boostTimer = boostDuration;
                ScrollingObject.speed *= 2;
                // 여기에 속도 증가에 따른 시각적 효과 등을 추가할 수 있습니다.
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
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGround를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}
