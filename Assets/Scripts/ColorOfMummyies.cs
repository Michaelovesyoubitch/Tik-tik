using UnityEngine;

public class ColorOfMummyies : MonoBehaviour
{
    //������������� �����
    //����� ����������� ���� �����, �������� ������ ����� (�� Bip) ��� ���� � ������.

    public GameObject[] body;
    public Color color;

    void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            foreach (GameObject go in body)
                go.GetComponent<SkinnedMeshRenderer>().material.color = color;
        }
    }

}
