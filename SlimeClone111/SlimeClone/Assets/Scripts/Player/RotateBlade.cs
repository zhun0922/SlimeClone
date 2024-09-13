using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlade : MonoBehaviour
{
    public int Id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        switch (Id)
        {
            case 0:
                speed = -150;
                //Batch();
                break;
            default:
                break;
        }

    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

      // if (Id == 0)
        //Batch();
    }
    private void Update()
    {
        switch (Id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                //Z축만 회전하면 됨 Vector3.forward(0,0,1)사용
                //근데 speed가 -가되어야 시계방향으로 도는데 
                //그러면 그냥 speed는 + 로하고, Vector.back(0,0,-1)을 사용하는게 좋으니 back사용
                break;
            default:
                break;
        }
    /*    if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }*/
    }
    //GetComponent<>잘 이해안가요
    //컴포넌트만 넣을수있는가?
    //서지웅의 제네릭 ㅣ 일반화 
    //일반화? 대중적인
    //같은 구현이고 형식만다를때 일반화사용
    //GetComponet= Instantiate한 게임오브젝트에서 컴포넌트를 가져오는것
    //어떤걸? Monster형식을.
    //형변환은아니야. 결과적으로 컴포넌트를가져오니까 Monster의 형식을 가지게 되는것.

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {

            Transform bullet = PoolManager.Instance.Get(prefabId).transform;
            //Get으로 총알을 만들면서 동시에 총알의 transform을 bullet변수에 집어넣음
            //그럼 Get()도 호출되고 transform값도 저장되는거여? 동시에?

            bullet.parent = this.transform;
            //GameManager.Instance.pool.Get(prefabId).transform.parent( transform.parent) tranform에는 parent라는 속성이 있다
            //parent속성을 이용하여 부모를 직접 변경. 워낙에 Get을 하면 PoolManager오브젝트의 자식으로 프리팹이 생성되는데, 
            //그걸 (this.)transform 즉, 이 스크립트가 있는 Weapon오브젝트의 자식으로 생성되도록 변경한다.

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; //회전은 Vector가 아니고 Quaternion.
            //레벨업을해서 count가 늘어나면 두가지 문제가 생김
            //1. 2 -> 5로 변경이아닌 추가가 됨
            //2. rotation이 초기화가 안돼서 위치가 이상하게 잡힘
            //==> position과 rotation초기화하자.

            Vector3 rotVec = Vector3.forward * 360 * index / count; //오...이거 괜찮네
            //(0,0,360)에 index/count를 곱할텐데 count = 4, index = 1일경우엔 (0,0,90) , index = 2 (0,0,180) ! 
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            //이게 중요한데, 월드좌표계가 아니라, 삽의 기준으로 위로 1.5만큼이동시킨다. 
            //bullet.up => 삽의 기준으로 위쪽방
            //그럼 자신이 기준이다를 가리키는 Space.self냐? 아니다. bullet.up으로 자신기준이라고 이미 다 해놨기 때문에
            //월드좌표를 사용한 Space.World 사용


            bullet.GetComponent<Blade>().Init(damage, -1); //-1은 무한
        }

    }
}
