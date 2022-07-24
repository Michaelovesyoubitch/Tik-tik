using System.Collections;
using UnityEngine;
public class PoolsObject : MonoBehaviour
{
    //ОБЪЕКТЫ ПУЛА. ИХ УРОН И УСЛОВИЯ ПОПАДАНИЯ В ПУЛ.

    public int damage = 1;
    private bool _coroutinaIsStarted;


    //Возвращение в пул.
    public void Return()
    {
        gameObject.SetActive(false);
    }

    //Запуск корутины.

    private void FixedUpdate()
    {

        if (gameObject.activeInHierarchy == true && !_coroutinaIsStarted)
        {
            StartCoroutine(ReturnTime());
        }
        else StopCoroutine(ReturnTime());
    }

    //Эта корутина вернёт пулю в пул.
    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(5f);
        Return();
    }

    //При столкновении с врагом пуля нанесёт ему урон.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<ControllerEnemy>().TakeDamage(damage);
        }

        Return();
    }


}
