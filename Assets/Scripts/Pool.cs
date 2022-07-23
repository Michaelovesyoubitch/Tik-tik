using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //ОЧЕВИДНО, НО СОЗДАЁТ ПУЛ. ТАКЖЕ ТУТ МЕТОДЫ, ПОЗВОЛЯЮЩИЕ ПРИЗЫВАТЬ ЧТО-ТО ИЗ ПУЛА.
    private List<PoolsObject> bullets;
    public PoolsObject bullet;
    public int bulletPool;

    //Инициализация пула при старте.

    void Awake()
    {
        bullets = new List<PoolsObject>();

        InstantiateBullet();
    }

    //Создание пула (набираем пул до нужного числа)

    private void InstantiateBullet()
    {
        for (int i = 0; i < bulletPool; i++)
        {
            CreateBullet(false);
        }

    }

    //Скрипт создания пуль и выключения их при старте.

    private PoolsObject CreateBullet(bool isActiveByDefault = false)
    {
        var creation = Instantiate(bullet, gameObject.transform);
        bullets.Add(creation);
        creation.gameObject.SetActive(isActiveByDefault);
        return creation;
    }

    //Перебор пула из пуль.

    public bool TryGetBullet(out PoolsObject poolsObject)
    {
        foreach (var bullet in bullets)
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

    //Если в пуле есть объекты, то достать. Если нет, то создать новые.

    public PoolsObject GetFreeElement()
    {
        if (TryGetBullet(out var poolsObject))
        {
            return poolsObject;
        }
        else return CreateBullet(true);
    }

    //Перегрузка предыдущего метода.

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
