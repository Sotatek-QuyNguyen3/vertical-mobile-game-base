
using UnityEngine;
public class AssetManager
{
    private static AssetManager _instance;
    public static AssetManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AssetManager();
            }
            return _instance;
        }

    }
    public GameObject LoadResourcesByPath(string path)
    {
        GameObject result = Resources.Load<GameObject>(path);
        return result;
    }
    public GameObject[] LoadResourcesAll(string path)
    {
        GameObject[] results = Resources.LoadAll<GameObject>(path);
        return results;
    }
    public GameObject LoadPrefabFxChangeInfo()
    {
        return Resources.Load<GameObject>(string.Format("{0}", "GamePaths.FX_CHANGE_INFO_PREFAB"));
    }
    public Sprite GetSprite(string assetName, string path = "")
    {
        if (path == "")
        {
            return Resources.Load<Sprite>(string.Format("{0}", assetName));
        }
        else
            return Resources.Load<Sprite>(string.Format("{0}/{1}", path, assetName));
    }
    public Sprite[] GetSprites(string path)
    {
        return Resources.LoadAll<Sprite>(path);
    }
    public string LoadResourceTextfile(string path)
    {

        TextAsset targetFile = Resources.Load<TextAsset>(path);

        return targetFile.text;
    }

}
