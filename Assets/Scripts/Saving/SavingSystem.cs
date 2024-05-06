﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour
{
    private const string extension = ".json";

    //public IEnumerator LoadLastScene(string saveFile)
    //{
    //    JObject state = LoadJsonFromFile(saveFile);
    //    IDictionary<string, JToken> stateDict = state;
    //    int buildIndex = SceneManager.GetActiveScene().buildIndex;
    //    if (stateDict.ContainsKey("lastSceneBuildIndex"))
    //    {
    //        buildIndex = (int)stateDict["lastSceneBuildIndex"];
    //    }
    //    yield return SceneManager.LoadSceneAsync(buildIndex);
    //    RestoreFromToken(state);
    //}

    public void Save(string saveFile)
    {
        //..
        string path = GetPathFromSaveFile(saveFile);
        print("Saving to " + path);
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            byte[] bytes = Encoding.UTF8.GetBytes("À¡la Seoul!");
            stream.Write(bytes, 0, bytes.Length);
        }

        //JObject state = LoadJsonFromFile(saveFile);
        //CaptureAsToken(state);
        //SaveFileAsJSon(saveFile, state);
    }

    private void SaveFileAsJSon(string saveFile, JObject state)
    {
        throw new NotImplementedException();
    }

    private void CaptureAsToken(JObject state)
    {
        throw new NotImplementedException();
    }

    public void Load(string saveFile)
    {
        //..
        string path = GetPathFromSaveFile(saveFile);
        print("Loading from " + path);
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            print(Encoding.UTF8.GetString(buffer));
        }

        //RestoreFromToken(LoadJsonFromFile(saveFile));
    }

    private void RestoreFromToken(object v)
    {
        throw new NotImplementedException();
    }

    //private JObject LoadJsonFromFile(string saveFile)
    //{
    //    string path = GetPathFromSaveFile(saveFile);
    //    if(!File.Exists(path))
    //    {
    //        return new JObject();
    //    }

    //    using (var textReader = File.OpenText(path))
    //    {
    //        using (var reader = new JsonTextReader(textReader))
    //        {
    //            reader.FloatParseHandling = FloatParseHandling.Double;

    //            return JObject.Load(reader);
    //        }
    //    }
    //}

    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + extension);
    }
}
