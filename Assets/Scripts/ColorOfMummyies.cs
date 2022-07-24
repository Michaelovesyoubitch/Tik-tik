using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]

public class ColorOfMummyies : MonoBehaviour
{
    //ПЕРЕКРАШИВАЕМ МУМИЮ
    //Чтобы перекрасить тело мумии, вставьте четыре части (не Bip) его тела в массив.

    public GameObject[] body;

    void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            foreach (GameObject go in body)
                go.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.26f, 0.69f, 0.94f);
        }
    }

}
