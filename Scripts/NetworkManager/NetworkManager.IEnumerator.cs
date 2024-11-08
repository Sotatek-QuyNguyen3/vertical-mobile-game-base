using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Serialization;
using System;
public partial class NetworkManager
{
    
    const int CODE_TOKEN_INVALID = 403;
    const string MESSAGE_TOKEN_INVALID = "Access token is not valid";

    private bool isWaitingRefreshToken = false;
    public bool IsWaitingRefreshToken
    {
        get { return isWaitingRefreshToken; }
        set { isWaitingRefreshToken = value; }
    }


    public IEnumerator CreateWebDeleteRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail)
    {

        UnityWebRequest request = UnityWebRequest.Delete(requestAPi.url);
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UploadHandler data = new UploadHandlerRaw(payload);
        request.uploadHandler = data;
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {



            Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
            if (onFail != null)
            {
                onFail.Invoke(request.error);
            }
        }
        else
        {
            //Debug.LogError(request.downloadHandler.text);
            string result = request.downloadHandler == null ? null : request.downloadHandler.text;
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }

    public IEnumerator CreateWebPostRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail)
    {

        UnityWebRequest request = UnityWebRequest.PostWwwForm(requestAPi.url, UnityWebRequest.kHttpVerbPOST);
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UploadHandler data = new UploadHandlerRaw(payload);
        request.uploadHandler = data;
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        Debug.LogError("POST:" + requestAPi.url);
        Debug.LogError(requestAPi.body);
        PopupLog._LogText += "= Post API : " + requestAPi.url + "\n";
        PopupLog._LogText += "\t body : " + requestAPi.body + "\n";
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError("error " + request.downloadHandler.text);
            PopupLog._LogText += "\t Error : " + request.downloadHandler.text + "\n";
            PopupLog._LogText += "\t Msg : " + new JSONObject(request.downloadHandler.text)["message"].list[0].str + "\n";

           

                Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                if (onFail != null)
                {
                    onFail.Invoke(request.downloadHandler.text);
                }

        }
        else
        {
            Debug.LogError(string.Format("Request : {0}  \n Response: {1}", requestAPi.url, request.downloadHandler.text));

            string result = request.downloadHandler.text;
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }

    public IEnumerator CreateWebGetRequest(string requestAPi, System.Action<string> onComplete, System.Action<object> onFail)
    {


        UnityWebRequest request = UnityWebRequest.Get(requestAPi);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        Debug.LogError("GET:" + requestAPi);
        PopupLog._LogText += "= Get API : " + requestAPi + "\n";
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError("error " + request.downloadHandler.text);
            PopupLog._LogText += "\t Error : " + request.downloadHandler.text + "\n";
            PopupLog._LogText += "\t Msg : " + new JSONObject(request.downloadHandler.text)["message"].list[0].str + "\n";


           
                try
                {
                    Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                }
                catch (Exception e)
                {

                }
                if (onFail != null)
                {
                    onFail.Invoke(request.error);
                }


        }
        else
        {
            string result = request.downloadHandler.text;
            Debug.LogError(request.downloadHandler.text);
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }


    public IEnumerator CreateWebPutRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail, bool isPatch = false)
    {

        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UnityWebRequest request = UnityWebRequest.Put(requestAPi.url, payload);
        if (isPatch)
            request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        Debug.LogError(requestAPi.url);
        Debug.LogError(requestAPi.body);
        PopupLog._LogText += "= Put API : " + requestAPi.url + "\n";
        PopupLog._LogText += "\t body : " + requestAPi.body + "\n";
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError("error " + request.downloadHandler.text);
            PopupLog._LogText += "\t Error : " + request.downloadHandler.text + "\n";
            PopupLog._LogText += "\t Msg : " + new JSONObject(request.downloadHandler.text)["message"].list[0].str + "\n";


           
                try
                {
                    Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                }
                catch (Exception e)
                {

                }
                if (onFail != null)
                {
                    onFail.Invoke(request.error);
                }
        }
        else
        {
            Debug.LogError(request.downloadHandler.text);

            string result = request.downloadHandler.text;
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }
}
