using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    //������� ��������� �����, �� ������� ���� �����.

    //������ ������������ ����� �������� �� �����. ����� ����� �������� � ������ �� ����� ����� ����, ����� ������������.

    //����� ������� �� �����������, ������������ � ���. ����� ��������� �� � ������.

    public GameObject[] enemy;
    public float countOfHealth;


    private void Start()
    {
        TakeDamage();
    }

    //������� �������� "�����".

    public void TakeDamage()
    {

        if (enemy != null)
        {
            float summ = 0;
            foreach (var obj in enemy)
            {
                summ += obj.GetComponent<ControllerEnemy>().health;
            }
            countOfHealth = summ;
            summ = 0;
        }
    }

    private void Update()
    {

        //������� ���������� ���������.
        TakeDamage();
        //���� ����� - �����, ������������� ��������� � ���, ����� ����� �� ����������.
        if (GetComponent<PointWalking>().finish)
            return;
        //���� ���, ����� ������� �������� ��������� ����� �� ����. ��� ������ �������, ����� �������� ����� ����� ����.
        if (countOfHealth <= 0 && !GetComponent<PointWalking>().finish && !GetComponent<PointWalking>().pastPoint)
        {
            GetComponent<PointWalking>().isActive = true;
        }
        else
            GetComponent<PointWalking>().isActive = false;
    }
}
