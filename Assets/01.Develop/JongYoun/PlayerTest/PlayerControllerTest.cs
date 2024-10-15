using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTest : MonoBehaviour
{
    private Vector2 moveInput;    // 입력 값을 저장할 변수
    private Animator animator;    // 애니메이터 컴포넌트
    public float moveSpeed = 5f;  // 캐릭터 이동 속도

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    // Input System에서 호출되는 메서드 (WASD 입력 받음)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // 2D 벡터 값으로 이동 방향 설정
        UpdateAnimation(); // 애니메이션 상태 업데이트
    }

    private void Update()
    {
        // 실제로 캐릭터를 움직이는 로직 (프레임마다 이동 처리)
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y); // X, Y 축으로 이동 (2D)
            transform.position += moveDirection * moveSpeed * Time.deltaTime; // 실제 이동 처리
        }
    }

    private void UpdateAnimation()
    {
        if (moveInput != Vector2.zero) // 캐릭터가 움직이는 중일 때
        {
            animator.SetFloat("DirectionX", moveInput.x); // X 방향 애니메이션 파라미터
            animator.SetFloat("DirectionY", moveInput.y); // Y 방향 애니메이션 파라미터
            animator.SetBool("IsMoving", true);           // 이동 중 상태
        }
        else // 캐릭터가 멈춰 있을 때
        {
            animator.SetBool("IsMoving", false); // 멈춤 상태로 전환
        }
    }
}
