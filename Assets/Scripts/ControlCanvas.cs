using UnityEngine;

public class ControlCanvas : MonoBehaviour
{
    //�������������� �������.

    RaycastHit hit;
    private LayerMask mask = 1 << 5;
    [SerializeField] private GameObject gun;

    //�� ��������, ��� �� ���� � ������� �� Attack. ��. ��� ����� ��� ������������� ����.

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity, mask))

                {
                    transform.GetChild(0).transform.position = hit.point;
                }
            }

        }
    }
}
