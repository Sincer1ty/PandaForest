using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

[System.Serializable]
public class CharInfo
{
    public string charName;
}

public class Panel_Inventory : MonoBehaviour
{
    GameObject CharSlot = null;
    Dictionary<string, GameObject> MyChilderen = new Dictionary<string, GameObject>();
    public List<CharInfo> CharInfoList = new List<CharInfo>();
    List<GameObject> MyCharList = new List<GameObject>();

    public TMP_Text ExplanationTxt;

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
        CreateCharList(CharInfoList);
    }

    void CreateCharList(List<CharInfo> _CharInfoList)
    {
        if (CharSlot == null)
        {
            CharSlot = MyChilderen["CharSlot"];
            CharSlot.SetActive(false);
        }

        if (MyCharList.Count <= 0)
        {
            for (int i = 0; i < _CharInfoList.Count; ++i)
            {
                Debug.Log("Create Char");

                GameObject NewChar;

                //슬롯 생성
                NewChar = Instantiate<GameObject>(CharSlot);

                //슬롯 이름 설정
                StringBuilder NameBuilder = new StringBuilder();
                NameBuilder.Append("CharSlot_");
                NameBuilder.Append(i + 1);
                NewChar.name = NameBuilder.ToString();

                //슬롯 위치 및 크기 설정
                NewChar.transform.SetParent(CharSlot.transform.parent);
                NewChar.transform.localScale = CharSlot.transform.localScale;

                NewChar.SetActive(true);
                MyCharList.Add(NewChar);
            }
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

        //ExplanationTxt.text = CharInfoList[idx].charName;
    }
}
