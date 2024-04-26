using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

//여기부터 확인
[ExecuteAlways]
public class JsonSaveableEntity : MonoBehaviour
{
    //강의 참조 필요
    [SerializeField] string uniqueIdentifier = "";

    static Dictionary<string, JsonSaveableEntity> globalLookup = new Dictionary<string, JsonSaveableEntity>();

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    //Blog 설명
    public JToken CaptureAsJToken()
    {
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;
        foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
        {
            JToken token = jsonSaveable.CaptureAsJToken();
            string component = jsonSaveable.GetType().ToString();
            Debug.Log($"{name} Capture {component} = {token.ToString()}");
            stateDict[jsonSaveable.GetType().ToString()] = token;
        }
        return state;
    }
}
