using UnityEngine;

[RequireComponent(typeof(PointWalking))]
public class DetectEnemy : MonoBehaviour
{
    //������� ��������� �����, �� ������� ���� �����.

    //������ ������������ ����� �������� �� �����. ����� ����� �������� � ������ �� ����� ����� ����, ����� ������������.

    //����� ������� �� �����������, ������������ � ���. ����� ��������� �� � ������.

    public GameObject[] enemy;
    public float countOfHealth;
    private PointWalking pointWalking;


    private void Start()
    {
        TakeDamage();
        pointWalking = GetComponent<PointWalking>();
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
        if (pointWalking.finish)
            return;
        //���� ���, ����� ������� �������� ��������� ����� �� ����. ��� ������ �������, ����� �������� ����� ����� ����.
        if (countOfHealth <= 0)
        {
            if (!pointWalking.finish && !pointWalking.pastPoint)
            {
                pointWalking.isActive = true;
            }
        }
        else
            pointWalking.isActive = false;
    }
}
