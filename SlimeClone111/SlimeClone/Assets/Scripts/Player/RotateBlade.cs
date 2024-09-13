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
                //Z�ุ ȸ���ϸ� �� Vector3.forward(0,0,1)���
                //�ٵ� speed�� -���Ǿ�� �ð�������� ���µ� 
                //�׷��� �׳� speed�� + ���ϰ�, Vector.back(0,0,-1)�� ����ϴ°� ������ back���
                break;
            default:
                break;
        }
    /*    if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }*/
    }
    //GetComponent<>�� ���ؾȰ���
    //������Ʈ�� �������ִ°�?
    //�������� ���׸� �� �Ϲ�ȭ 
    //�Ϲ�ȭ? ��������
    //���� �����̰� ���ĸ��ٸ��� �Ϲ�ȭ���
    //GetComponet= Instantiate�� ���ӿ�����Ʈ���� ������Ʈ�� �������°�
    //���? Monster������.
    //����ȯ���ƴϾ�. ��������� ������Ʈ���������ϱ� Monster�� ������ ������ �Ǵ°�.

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {

            Transform bullet = PoolManager.Instance.Get(prefabId).transform;
            //Get���� �Ѿ��� ����鼭 ���ÿ� �Ѿ��� transform�� bullet������ �������
            //�׷� Get()�� ȣ��ǰ� transform���� ����Ǵ°ſ�? ���ÿ�?

            bullet.parent = this.transform;
            //GameManager.Instance.pool.Get(prefabId).transform.parent( transform.parent) tranform���� parent��� �Ӽ��� �ִ�
            //parent�Ӽ��� �̿��Ͽ� �θ� ���� ����. ������ Get�� �ϸ� PoolManager������Ʈ�� �ڽ����� �������� �����Ǵµ�, 
            //�װ� (this.)transform ��, �� ��ũ��Ʈ�� �ִ� Weapon������Ʈ�� �ڽ����� �����ǵ��� �����Ѵ�.

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; //ȸ���� Vector�� �ƴϰ� Quaternion.
            //���������ؼ� count�� �þ�� �ΰ��� ������ ����
            //1. 2 -> 5�� �����̾ƴ� �߰��� ��
            //2. rotation�� �ʱ�ȭ�� �ȵż� ��ġ�� �̻��ϰ� ����
            //==> position�� rotation�ʱ�ȭ����.

            Vector3 rotVec = Vector3.forward * 360 * index / count; //��...�̰� ������
            //(0,0,360)�� index/count�� �����ٵ� count = 4, index = 1�ϰ�쿣 (0,0,90) , index = 2 (0,0,180) ! 
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            //�̰� �߿��ѵ�, ������ǥ�谡 �ƴ϶�, ���� �������� ���� 1.5��ŭ�̵���Ų��. 
            //bullet.up => ���� �������� ���ʹ�
            //�׷� �ڽ��� �����̴ٸ� ����Ű�� Space.self��? �ƴϴ�. bullet.up���� �ڽű����̶�� �̹� �� �س��� ������
            //������ǥ�� ����� Space.World ���


            bullet.GetComponent<Blade>().Init(damage, -1); //-1�� ����
        }

    }
}
