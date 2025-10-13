using UnityEngine;

public class controllerold : MonoBehaviour
{
    [Header("캐릭터 설정")]
    public string playerName = "플레이어";
    public float moveSpeed = 5.0f;
    
    // Animator 컴포넌트 참조 (private - Inspector에 안 보임)
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // 게임 시작 시 한 번만 - Animator 컴포넌트 찾아서 저장
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 추가

        // 캐릭터 소개
        Debug.Log("안녕하세요, " + playerName + "님!");
        Debug.Log("이동 속도: " + moveSpeed);
    }
    
    void Update()
    {
        // 이동 벡터 계산
        Vector3 movement = Vector3.zero;
        
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            spriteRenderer.flipX = true;
        }
    
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.down;
        }
        
        // 달리기 속도 계산
        float currentMoveSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed = moveSpeed * 2f;
            Debug.Log("달리기 모드 활성화!");
        }

        // 이동할 때 계산된 속도 사용
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime);

        // 점프 입력 (한 번만 실행되어야 하므로 GetKeyDown!)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetBool("isJumping", true);
                Debug.Log("점프!");
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetBool("isJumping", false);
                Debug.Log("점프 끝!");
            }
        }

        // 실제 이동 적용
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
        
        // 속도 계산: 이동 중이면 moveSpeed, 아니면 0
        float currentSpeed = movement != Vector3.zero ? currentMoveSpeed : 0f;
        
        // Animator에 속도 전달
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
            Debug.Log("Current Speed: " + currentSpeed);
        }
    }
}