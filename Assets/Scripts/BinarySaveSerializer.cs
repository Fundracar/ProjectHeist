using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSerializer : MonoBehaviour
{
    private static BinarySaveSerializer _instance;

    public static BinarySaveSerializer Instance()
    {
        if (_instance == null)
            _instance = new BinarySaveSerializer();
        else
            Debug.Log("Already have a reference");

        return _instance;
    }
    
    private static readonly string Path = $"{Application.persistentDataPath}/save.data";

    public void Save(Save save)
    {
        Debug.Log($"Saving to: {Path}");

        var bf = new BinaryFormatter();
        
        var ss = new SurrogateSelector();
        bf.SurrogateSelector = ss;

        FileStream file = File.Create(Path);
        bf.Serialize(file, save);
        file.Close();
        
        
        Debug.Log($"{this.SaveStamp()}Saved!", this);
    }

    public Save Load()
    {
        Debug.Log($"{this.SaveStamp()}Loading from: {Path}", this);

        if (File.Exists(Path))
        {
            var bf = new BinaryFormatter();
            
            var ss = new SurrogateSelector();
            bf.SurrogateSelector = ss;
            
            FileStream file = File.Open(Path, FileMode.Open);
            var save =  (Save) bf.Deserialize(file);
            file.Close();
            Debug.Log($"{this.SaveStamp()}Loaded!", this);
            return save;
        }

        Debug.LogWarning("No save could be retrieved!");
        return null;
    }
}
