using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CodePractice : MonoBehaviour
{

    public string mString = "Taek";
    public string bString = "blog";
    // Start is called before the first frame update
    void Start()
    {
        PracticeStringAppend();
        StringBuilderInsert();
        RemoveString();
        ReplaceString();
    }
    public void PracticeStringAppend()
    {
        // ��� ���� String �迭�� �߰�
        StringBuilder str = new StringBuilder();
        str.Append(mString);
        str.Append(bString);
        Debug.Log(str.ToString());
    }

    public void StringBuilderInsert()
    {
        // ������ �ڸ��� �ٸ� String ��Ҹ� ���� �ִ´�.
        StringBuilder istr = new StringBuilder(mString);
        istr.Insert(1, "aaaaaa");
        Debug.Log(istr.ToString());
    }

    public void RemoveString()
    {
        //Ư�� �������� ���̸�ŭ �����Ѵ�.
        StringBuilder rstr = new StringBuilder(bString);
        rstr.Remove(1, 2);
        Debug.Log(rstr.ToString());
    }

    public void ReplaceString()
    {
        //Ư�� char ��Ҹ� �����Ѵ�.
        StringBuilder restr = new StringBuilder(mString);
        restr.Replace('T', 'B');
        Debug.Log(restr.ToString());
    }

}
