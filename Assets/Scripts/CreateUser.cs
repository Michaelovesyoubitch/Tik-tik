using UnityEngine;
using UnityEngine.Networking;

public class CreateUser : MonoBehaviour
{
    public string login;
    public int pass;

    private void Start()
    {
        Create(login, pass);
    }
    void Create (string login, int pass)
    {
        WWWForm form = new WWWForm ();
        form.AddField("login", login);
        form.AddField("pass", pass);
        WWW www = new WWW ("http://localhost/reg.php", form);
    }
}
