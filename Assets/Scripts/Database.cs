using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    private string[] users;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData ()
    {
        UnityWebRequest items = UnityWebRequest.Get ("http://localhost/");
        yield return items.SendWebRequest();
        string data = items.downloadHandler.text;

        data = data.Remove(data.Length - 1);
        users = data.Split (';');

        Debug.Log(GetValue(users[0], "Pass:"));
    }

    string GetValue (string data, string index)
    {
        string val = data.Substring (data.IndexOf(index) + index.Length);
        if (val.Contains("|"))
        val = val.Remove(val.IndexOf ("|"));
        return val;
    }

}
