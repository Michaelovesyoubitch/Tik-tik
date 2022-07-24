using System.Collections;
using UnityEngine;
public class PoolsObject : MonoBehaviour
{
    //������� ����. �� ���� � ������� ��������� � ���.

    public int damage = 1;
    private bool coroutinaIsStarted;


    //����������� � ���.
    public void Return()
    {
        gameObject.SetActive(false);
    }

    //������ ��������.

    private void FixedUpdate()
    {

        if (gameObject.activeInHierarchy == true && !coroutinaIsStarted)
        {
            StartCoroutine(ReturnTime());
        }
        else StopCoroutine(ReturnTime());
    }

    //��� �������� ����� ���� � ���.
    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(5f);
        Return();
    }

    //��� ������������ � ������ ���� ������ ��� ����.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<ControllerEnemy>().TakeDamage(damage);
        }

        Return();
    }


}
