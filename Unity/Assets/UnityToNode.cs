using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;


public class Protocols 
{
    public class Packets
    {
        public class common
        {
            public int cmd;
            public string message;
        }
        public class req_data : common
        {
            public int id;
            public string data;
        }
    }
}
public class UnityToNode : MonoBehaviour
{
    private IEnumerator GetData(string url, System.Action<string> callback)
    {
        var webRequset = UnityWebRequest.Get(url);
        yield return webRequset.SendWebRequest();

        Debug.Log("Get :" + webRequset.downloadHandler.text);
        if (webRequset.result == UnityWebRequest.Result.ConnectionError
            || webRequset.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않아서 통신 불가 ");
        }
        else
        {
            callback(webRequset.downloadHandler.text);
        }
    }
    public Button btnGetExample;
    public string host;
    public int port;
    public string route;

    public void Start()
    {
        btnGetExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, route);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) =>
            {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
