using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // ОПИСАНИЕ ДВИЖЕНИЯ ПУЛЬ. РЕЙКАСТ.

    public GameObject container, empty;
    public static RaycastHit hit;
    public static Ray ray;
    public int speed = 1;
    private bool _isCalled;
    private Pool _pool;
    private LayerMask _canvasLayer = 1 << 5, _notCanvasLayer;

    //Формируем два слоя. При этом обозначаем, что слой "UI" игнорируется.

    private void Start()
    {
        _notCanvasLayer = ~_canvasLayer;
        _canvasLayer = Physics.IgnoreRaycastLayer;
        _pool = container.GetComponent<Pool>();
    }

    //Просто призыв пули из пула.

    private PoolsObject Call()
    {
        PoolsObject whizbang = _pool.GetFreeElement(empty.transform.position, empty.transform.rotation);
        return whizbang;
    }

    //Стрельба. Пуля целится в точку коллизии луча.

    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && !_isCalled)
        {
            if (transform.parent.GetChild(1).gameObject.activeInHierarchy == true)
            {
                Touch touch = Input.GetTouch(0);

                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity, _notCanvasLayer))

                    {
                        Debug.DrawRay(empty.transform.position, ray.direction);

                        _isCalled = true;
                        var createdObject = Call();
                        createdObject.GetComponent<Rigidbody>().AddForce(speed * Time.fixedDeltaTime * ray.direction);
                        StartCoroutine(Switch());
                    }

                }

            }
        }
    }

    //Этот переключатель не даёт пулям спавниться слишком быстро.

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(0.5f);
        _isCalled = false;
    }

}
