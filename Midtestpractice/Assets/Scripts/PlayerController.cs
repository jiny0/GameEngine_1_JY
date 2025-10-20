using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    
    [Header("점프 설정")]
    public float jumpForce = 10.0f;
    
    private Animator animator;
    private Rigidbody2D rb;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다!");
        }
    }
    
    void Update()
    {
        // 좌우 이동 입력
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        
        // 물리 기반 이동
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        
        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("점프!");
        }
        
        // 애니메이션
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", currentSpeed);
    }
}