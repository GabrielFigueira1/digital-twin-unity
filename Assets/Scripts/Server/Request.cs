using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Request : MonoBehaviour
{
    public const string serverUrl = "http://localhost:3333/";
    
    public void TestConnection(System.Action<string> callback, string url = serverUrl)
    {
        StartCoroutine(GetRequest(serverUrl, callback));
    }

    public void AllData(System.Action<string> callback, string url = serverUrl)
    {
        StartCoroutine(GetRequest(serverUrl + "readAll", callback));
    }

    public void ReadLast(System.Action<string> callback, string url = serverUrl)
    {
        StartCoroutine(GetRequest(serverUrl + "readLast", callback));
    }

    public void DeleteLast(System.Action<string> callback, string url = serverUrl)
    {
        StartCoroutine(GetRequest(serverUrl + "deleteLast", callback));
    }

    public void ReadStatus(System.Action<string> callback, string url = serverUrl)
    {
        StartCoroutine(GetRequest(serverUrl + "ReadStatus", callback));
    }

    public void InsertData(string name, int value, System.Action<string> callback, string url = serverUrl)
    {
        JsonInsertData jsonObj = new JsonInsertData();
        jsonObj.name = name;
        jsonObj.value = value;
        string json = JsonUtility.ToJson(jsonObj);
        Debug.Log(json);
        StartCoroutine(PostRequest(serverUrl + "insertData", json, callback));
    }

    IEnumerator GetRequest(string uri, System.Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    callback("ERROR");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    callback("ERROR");
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    callback(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator PostRequest(string url, string postData, System.Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, postData))
        {
            webRequest.method = "POST";
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    callback("ERROR");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    callback("ERROR");
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nForm uploaded\n Response: " + webRequest.downloadHandler.text);
                    callback(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
