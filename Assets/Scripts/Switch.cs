using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private void Update()
    {
        //�����, ����� ������ ������ ���� �����, �������� �� ������ �������� �� ����.
        
        if (GetComponentInParent<PointWalking>().pastPointLink.transform.GetComponent<PointWalking>().pastPoint)
        {
            GetComponent<ControllerEnemy>().enabled = true;
        }
        else
            GetComponent<ControllerEnemy>().enabled = false;
    }
}
