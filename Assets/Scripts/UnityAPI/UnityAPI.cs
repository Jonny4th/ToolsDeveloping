using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Networking;

public class UnityAPI : MonoBehaviour
{
    private string m_ProjectId = "ae4bb919-e69a-4009-875a-1299b8e47114";
    private string m_EnvirommentName = "development";
    private string m_EnvironmentId = "4d935df6-2e86-4495-b4a1-9e9981fef242";

    private string m_Key = "Basic MDU5YWNkOGQtY2M5ZS00NTQ1LWE1MjktYTk2YWMxMzU0OWFmOm9SUU9ZYzkwSm04d0lKZWh4LXJZYnNrTWVsMHRiOTBC";

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    void Start()
    {
        string[] scopes = new string[] { "player_auth.server.custom_id_auth" };

        StartCoroutine(ExchangeToken(scopes, (token) =>
        {
            Debug.Log(token);
        }));

        Dictionary<string, object> parameters = new Dictionary<string, object>();

        var test = "{\r\n  \"expiresIn\": 3600,\r\n  \"idToken\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c\",\r\n  \"sessionToken\": \"5eb26a338a232\",\r\n  \"lastNotificationDate\": \"123000000\",\r\n  \"user\": {\r\n    \"disabled\": false,\r\n    \"externalIds\": [\r\n      {\r\n        \"externalId\": \"5eb26a338a232\",\r\n        \"providerId\": \"provider-id\"\r\n      }\r\n    ],\r\n    \"id\": \"eyJhbGciOiJIUzI1\",\r\n    \"username\": \"New_User_57\"\r\n  },\r\n  \"userId\": \"5eb26a338a232\"\r\n}";

        var obj = JObject.Parse(test);
        var externalIds = (JArray)obj["user"]["externalIds"];
        var id = externalIds.First(x => (string)x["providerId"] == "provider-id");

        Debug.Log((string)id["externalId"]);

        var response = JsonConvert.DeserializeObject<CustonIdSignInResponse>(test);
        Debug.Log(response.user.externalIds[0].externalId);
    }

    private IEnumerator ExchangeToken(string[] scopes, Action<string> callback)
    {
        var bytes = ConvertScopeArrayToBytes(scopes);
        var urlFormat = "https://services.api.unity.com/auth/v1/token-exchange?projectId={0}&environmentId={1}";
        var url = string.Format(urlFormat, m_ProjectId, m_EnvironmentId);

        var www = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(bytes),
            downloadHandler = new DownloadHandlerBuffer()
        };

        www.SetRequestHeader("Authorization", m_Key);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            callback?.Invoke("");
        }
        else
        {
            var token = ExtractTokenFromResponse(www.downloadHandler.text);
            callback?.Invoke(token);
        }

        //local methods
        byte[] ConvertScopeArrayToBytes(string[] scopes) => 
            Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(
                    new Dictionary<string, string[]>()
                    {
                        { "scopes", scopes }
                    }
                )
            );

        string ExtractTokenFromResponse(string response) => (string)JObject.Parse(response)["accessToken"];
    }

    [Serializable]
    public class UnityExternalId
    {
        public string externalId;
        public string providerId;
    }

    [Serializable]
    public class UnityUser
    {
        public bool disabled;
        public string id;
        public string username;
        public UnityExternalId[] externalIds;
    }

    [Serializable]
    public class CustonIdSignInResponse
    {
        public UnityUser user;
        public string userId;
        public int expiresIn;
        public string idToken;
        public string sessionToken;
        public string lastNotificationDate;
    }
}
