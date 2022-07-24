using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //��������, �� ������� ���. ����� ��� ������, ����������� ��������� ���-�� �� ����.
    
    public PoolsObject bullet;
    public int bulletPool;
    private List<PoolsObject> _bullets;

    //������������� ���� ��� ������.

    void Awake()
    {
        _bullets = new List<PoolsObject>();

        InstantiateBullet();
    }

    //�������� ���� (�������� ��� �� ������� �����)

    private void InstantiateBullet()
    {
        for (int i = 0; i < bulletPool; i++)
        {
            CreateBullet(false);
        }

    }

    //������ �������� ���� � ���������� �� ��� ������.

    private PoolsObject CreateBullet(bool isActiveByDefault = false)
    {
        var creation = Instantiate(bullet, gameObject.transform);
        _bullets.Add(creation);
        creation.gameObject.SetActive(isActiveByDefault);
        return creation;
    }

    //������� ���� �� ����.

    public bool TryGetBullet(out PoolsObject poolsObject)
    {
        foreach (var bullet in _bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                poolsObject = bullet;
                bullet.gameObject.SetActive(true);
                return true;
            }
        }

        poolsObject = null;
        return false;
    }

    //���� � ���� ���� �������, �� �������. ���� ���, �� ������� �����.

    public PoolsObject GetFreeElement()
    {
        if (TryGetBullet(out var poolsObject))
        {
            return poolsObject;
        }
        else return CreateBullet(true);
    }

    //���������� ����������� ������.

    public PoolsObject GetFreeElement(Vector3 position, Quaternion quaternion)
    {
        if (TryGetBullet(out var poolsObject))
        {
            poolsObject.transform.position = position;
            poolsObject.transform.rotation = quaternion;
            return poolsObject;
        }
        else return CreateBullet(true);
    }

}
