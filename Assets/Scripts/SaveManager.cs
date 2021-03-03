using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static Save _save;
    public static Save Save => _save;

    public static Action<Save> saveLoaedEvt;
    public static Action<Save> savePreparingEvt;
    
    public enum SaveFormat
    {
        Binary,
        Json
    }
    
    [RuntimeInitializeOnLoadMethod] private static void Init()
    {
        Debug.Log("Save initiate!");
        _save = new Save();
    }
    
    public static void SaveSave()
    {
        savePreparingEvt.Invoke(_save);
      
        /*switch (format)
        {
            case SaveFormat.Binary:
                BinarySaveSerializer.Instance().Save(_save);
                break;
            case SaveFormat.Json:
                JSONSaveSerializer.Instance().save(_save);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }*/
        
        BinarySaveSerializer.Instance().Save(_save);
    }

    public static void LoadSave()
    {
        Save loadedSave = new Save();

       /* switch (format)
        {
            case SaveFormat.Binary:
                loadedSave = BinarySaveSerializer.Instance().Load();
                break;
           case SaveFormat.Json:
                loadedSave = JSONSaveSerializer.Instance().load();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }*/

    loadedSave = BinarySaveSerializer.Instance().Load();
    
        if (loadedSave == null)
            return;

        _save = loadedSave;
        saveLoaedEvt.Invoke(_save);
    }
}
