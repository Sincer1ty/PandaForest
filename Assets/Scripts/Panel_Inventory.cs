using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class CharInfo
{
    string charName;
}

public class Panel_Inventory : MonoBehaviour
{
    GameObject CharSlot = null;
    Dictionary<string, GameObject> MyChilderen = new Dictionary<string, GameObject>();
    List<CharInfo> CharInfoList = new List<CharInfo>();
    List<GameObject> MyCharList = new List<GameObject>();
    List<GameObject> MyCharListRe = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        transform.gameObject.SetActive(false);

        SetChildrenMap(gameObject);
    }

    void SetChildrenMap(GameObject targetObj)
    {
        for(int i = 0; i < targetObj.transform.childCount; ++i)
        {
            Transform childTransform = targetObj.transform.GetChild(i);
            MyChilderen.Add(childTransform.name, childTransform.gameObject);

            if(childTransform.transform.childCount > 0)
                SetChildrenMap(childTransform.gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i< 8; ++i)
        {
            CharInfo info = new CharInfo();
            CharInfoList.Add(info);
        }

        CreateCharList(CharInfoList);
    }

    void CreateCharList(List<CharInfo> _CharInfoList)
    {
        if (CharSlot == null)
        {
            CharSlot = MyChilderen["CharSlot"];
            CharSlot.SetActive(false);
        }

        for(int i = 0; i < MyCharList.Count; ++i)
        {
            MyCharList[i].SetActive(false);
            MyCharListRe.Add(MyCharList[i]);
        }
        MyCharList.Clear();

        for(int i = 0; i < _CharInfoList.Count; ++i)
        {
            GameObject NewChar;

            if(MyCharListRe.Count > 0)
            {
                NewChar = MyCharListRe[0];
                MyCharListRe.RemoveAt(0);

                //...
            }
            else
            {
                NewChar = Instantiate<GameObject>(CharSlot);

                //...
            }

            StringBuilder NameBuilder = new StringBuilder();
            NameBuilder.Append("CjarSlot_");
            NameBuilder.Append(i + 1);
            NewChar.name = NameBuilder.ToString();

            NewChar.transform.SetParent(CharSlot.transform.parent);
            NewChar.transform.localScale = CharSlot.transform.localScale;

            NewChar.SetActive(true);
            MyCharList.Add(NewChar);
        }
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }

    public void OnClicked(GameObject clickedObj)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("OnClicked : ");
        builder.Append(clickedObj.name);
        print(builder.ToString());

        
    }
}
