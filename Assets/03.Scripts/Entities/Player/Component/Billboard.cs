using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cameraToLookAt;

    void Update()
    {
        // 카메라와 오브젝트의 방향을 계산
        Vector3 direction = cameraToLookAt.transform.position - transform.position;

        // Z축 회전을 막고, Y축만 회전하도록 설정
        direction.x = 0; // Y축 회전만 허용 (즉, 기울이지 않음)

        // 스프라이트가 카메라를 바라보도록 회전값을 설정
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}
