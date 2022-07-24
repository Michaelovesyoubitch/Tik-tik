using UnityEngine;

[RequireComponent(typeof(PointWalking))]
public class DetectEnemy : MonoBehaviour
{
    //������� ��������� �����, �� ������� ���� �����.

    //������ ������������ ����� �������� �� �����. ����� ����� �������� � ������ �� ����� ����� ����, ����� ������������.

    //����� ������� �� �����������, ������������ � ���. ����� ��������� �� � ������.

    public GameObject[] enemy;
    public float countOfHealth;
    private PointWalking _pointWalking;


    private void Start()
    {
        TakeDamage();
        _pointWalking = GetComponent<PointWalking>();
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
        if (_pointWalking.finish)
            return;
        //���� ���, ����� ������� �������� ��������� ����� �� ����. ��� ������ �������, ����� �������� ����� ����� ����.
        if (countOfHealth <= 0)
        {
            if (!_pointWalking.finish && !_pointWalking.pastPoint)
            {
                _pointWalking.isActive = true;
            }
        }
        else
            _pointWalking.isActive = false;
    }
}
