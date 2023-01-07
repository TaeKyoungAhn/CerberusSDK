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
        // 요소 끝에 String 배열을 추가
        StringBuilder str = new StringBuilder();
        str.Append(mString);
        str.Append(bString);
        Debug.Log(str.ToString());
    }

    public void StringBuilderInsert()
    {
        // 선택한 자리에 다른 String 요소를 집어 넣는다.
        StringBuilder istr = new StringBuilder(mString);
        istr.Insert(1, "aaaaaa");
        Debug.Log(istr.ToString());
    }

    public void RemoveString()
    {
        //특정 지점에서 길이만큼 삭제한다.
        StringBuilder rstr = new StringBuilder(bString);
        rstr.Remove(1, 2);
        Debug.Log(rstr.ToString());
    }

    public void ReplaceString()
    {
        //특정 char 요소를 변경한다.
        StringBuilder restr = new StringBuilder(mString);
        restr.Replace('T', 'B');
        Debug.Log(restr.ToString());
    }

}
