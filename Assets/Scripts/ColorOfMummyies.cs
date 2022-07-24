using UnityEngine;

public class ColorOfMummyies : MonoBehaviour
{
    //ПЕРЕКРАШИВАЕМ МУМИЮ
    //Чтобы перекрасить тело мумии, вставьте четыре части (не Bip) его тела в массив.

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
