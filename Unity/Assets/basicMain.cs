using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class basicMain : MonoBehaviour
{
    public Button Hello;
    public string host;
    public int port;
    public string route;

    private void Start()
    {
        this.Hello.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, route);
            Debug.Log(url);

            StartCoroutine(this.GetBasic(url, (raw) =>
            {
                Debug.LogFormat("{0}", raw);
            }));
        });
    }

    private IEnumerator GetBasic(string url , System.Action<string> callback) 
    {
        var webRequset = UnityWebRequest.Get(url);
        yield return webRequset.SendWebRequest();

        if (webRequset.result == UnityWebRequest.Result.ConnectionError
            || webRequset.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʾƼ� ��� �Ұ� ");
        }
        else
        {
            callback(webRequset.downloadHandler.text);
        }
    }
}
