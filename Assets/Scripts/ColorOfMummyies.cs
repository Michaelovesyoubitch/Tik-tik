using UnityEngine;

public class ColorOfMummyies : MonoBehaviour
{
    //������������� �����
    //����� ����������� ���� �����, �������� ������ ����� (�� Bip) ��� ���� � ������.

    public GameObject[] body;
    [SerializeField] private Color _color;

    void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            foreach (GameObject go in body)
                go.GetComponent<SkinnedMeshRenderer>().material.color = _color;
        }
    }

}
