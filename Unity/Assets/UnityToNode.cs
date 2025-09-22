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

        public class res_data : common
        {
            public req_data[] result;
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
    public Button btnPostExample;
    public Button btnResDataExample;
    public string host;
    public int port;
    public string route;

    public string postUrl;
    public string resUrl;
    public int id;
    public string data;

    private IEnumerator PostData(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError
            || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않아 통신 불가능");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
        webRequest.Dispose();
    }

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

        btnPostExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, postUrl);
            Debug.Log(url);
            var req = new Protocols.Packets.req_data();
            req.cmd = 1000;
            req.id = id;
            req.data = data;
            var json = JsonConvert.SerializeObject(req);

            StartCoroutine(PostData(url, json, (raw) =>
            {
                Protocols.Packets.common res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0},{1}", res.cmd, res.message);
            }));
        });
        btnResDataExample.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, resUrl);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) =>
            {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.res_data>(raw);

                foreach (var user in res.result)
                {
                    Debug.LogFormat("{0} , {1}", user.id, user.data);
                }
            }));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
