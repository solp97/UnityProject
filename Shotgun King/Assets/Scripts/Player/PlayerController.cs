using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public UnityEvent<GridIndex> TunrOver = new UnityEvent<GridIndex>();

    private PlayerInput _input;
    private PlayerShoter _shoter;
    private PlayerMovement _movement;

    public bool IsDead;

    public GameObject PlayerFigure;
    public GameObject GunFigure;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _shoter = GetComponent<PlayerShoter>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //RotateToMouseDir();
        //Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit _hit;
        //Physics.Raycast(_ray, out _hit, 100f);
        ////Debug.Log(_hit.point);
        //
        //Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장????????
        //Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장?????????
        ////카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z축 정보에 ????????
        ////게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 합니다.????????
        //mPosition.z = oPosition.z - Camera.main.transform.position.z;
        ////화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 합니다.????????
        ////그래야, 위치를 찾아갈 수 있겠습니다.????????
        //Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        //// Debug.Log(target);
        //Debug.DrawLine(transform.position , Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward), Color.red);
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward ));
        
        ////다음은 아크탄젠트(arctan, 역탄젠트)로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를????????
        ////이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를 회전시키기????????
        ////위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킵니다.?????????
        ////우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.????????
        //float dy = target.y - oPosition.y;
        //float dx = target.x - oPosition.x;
        ////오릴러 회전 함수를 0에서 180 또는 0에서 -180의 각도를 입력 받는데 반하여????????
        ////(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크탄젠트 Atan2()함수의 결과 값은 ????????
        ////라디안 값(180도가 파이(3.141592654...)로)으로 출력되므로????????
        ////라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 됩니다.????????
        //float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        ////구해진 각도를 오일러 회전 함수에 적용하여 z축을 기준으로 게임 오브젝트를 회전시킵니다.????????
        //transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
         
         

        if (_input.Mouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (Vector3.Distance(transform.position, hit.point) > 0.1f)
                {
                    if (_shoter.TryShot() && !_movement.isMove)
                    {
                        _shoter.shot(hit.point - transform.position);
                        TunrOver.Invoke(_movement._board.PlayerPos);
                    }
                }
                else
                {
                    if (!_movement.isMove)
                    {
                        _movement.isCanMove(transform, hit.point);
                        _shoter.tryReload();
                        TunrOver.Invoke(_movement._board.PlayerPos);
                    }
                }

            }
        }
    }


    //void RotateToMouseDir()
    //{
    //    Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition+Vector3.forward*10f));
    //}


}
