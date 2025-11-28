using UnityEngine;
using System.Collections; 
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour
{
    // --- 1. 패널 GameObject 변수들 (Inspector에서 연결) ---
    public GameObject homePanel;
    public GameObject stageSelectPanel;
    public GameObject characterPanel;

    // --- 2. Animator 컴포넌트 변수 (Inspector에서 연결) ---
    public Animator stageSelectAnimator; 
    
    // 애니메이션 재생 시간 (SlideIn, SlideOut 클립의 길이와 일치해야 함)
    private const float ANIMATION_DURATION = 0.5f; 

    // --- 공통 기능: 모든 패널을 끄는 함수 (부분적으로 사용) ---
    void DisableAllPanels()
    {
        homePanel.SetActive(false);
        stageSelectPanel.SetActive(false);
        characterPanel.SetActive(false);
    }
    
    // --- 버튼에 연결할 공개 함수 (여는 동작) ---

    // 스테이지 선택 창 열기 (애니메이션 사용)
    public void OpenStageSelect()
    {
        // 홈 화면이 열려있을 때만 전환 시작
        if (homePanel.activeSelf)
        {
            StartCoroutine(OpenStageSelectPanelCoroutine());
        }
    }
    
    // 캐릭터 관리 창 열기 (애니메이션 없이 바로 전환)
    public void OpenCharacterManagement()
    {
        DisableAllPanels();
        characterPanel.SetActive(true);
    }

    // --- 버튼에 연결할 공개 함수 (닫는 동작) ---

    // 홈 화면으로 돌아가기 (StageSelectPanel을 닫는 동작)
    public void OpenHome()
    {
        // StageSelectPanel이 열려있을 때만 애니메이션 전환 시작
        if (stageSelectPanel.activeSelf)
        {
            StartCoroutine(CloseStageSelectPanelCoroutine());
        } 
        else if (characterPanel.activeSelf)
        {
            // 다른 패널에서 돌아올 경우 (애니메이션 없는 경우)
            DisableAllPanels();
            homePanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // ==========================================================
    // --- 코루틴: 애니메이션 재생 로직 (최종 수정 완료) ---
    // ==========================================================

    // 스테이지 선택 화면을 여는 코루틴
    IEnumerator OpenStageSelectPanelCoroutine()
    {
        // 1. StageSelectPanel을 즉시 활성화 (HomePanel 위에 겹쳐서 보이게 함)
        stageSelectPanel.SetActive(true); 

        // 2. Animator가 연결되어 있다면, 애니메이션을 실행
        if (stageSelectAnimator != null)
        {
            stageSelectAnimator.SetTrigger("Open");
            
            // 3. 애니메이션이 끝날 때까지 대기
            //    -> HomePanel은 애니메이션이 끝날 때까지 켜져 있어 빈 화면을 방지합니다.
            yield return new WaitForSeconds(ANIMATION_DURATION);
        }
        else
        {
            // 애니메이터가 없을 경우에도 짧은 시간 대기
            yield return new WaitForSeconds(0.01f);
        }

        // 4. 애니메이션이 끝난 후, HomePanel을 비활성화
        homePanel.SetActive(false);
        characterPanel.SetActive(false);
    }


    // 스테이지 선택 화면을 닫는 코루틴 (배경 유지 로직)
    IEnumerator CloseStageSelectPanelCoroutine()
    {
        // 1. StageSelectPanel 뒤에 HomePanel을 즉시 활성화 (배경 채우기)
        homePanel.SetActive(true);
        characterPanel.SetActive(false); 

        // 2. 닫기 애니메이션(SlideOut) Trigger 발동
        if (stageSelectAnimator != null)
        {
            stageSelectAnimator.SetTrigger("Close");
            
            // 3. 애니메이션이 끝날 때까지 대기
            yield return new WaitForSeconds(ANIMATION_DURATION);
        }
        
        // 4. 애니메이션이 끝난 후, StageSelectPanel을 비활성화
        stageSelectPanel.SetActive(false);
    }
}