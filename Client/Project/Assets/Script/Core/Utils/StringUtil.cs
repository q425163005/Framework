using UnityEngine;

public class StringUtil
{
    /// <summary>
    /// 字符串截取
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="c">截取字符</param>
    /// <param name="cIndex">从c出现的的次数索引开始截 -1不截取, 0第一次,1第二次截</param>
    /// <returns></returns>
    public static string SubstringIndexOf(string str,char c,int cIndex)
    {
        if (cIndex < 0)
            return str;
        int sum = 0;
        int lastIndex = -1;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == c)
            {
                if (cIndex == sum)
                    return str.Remove(i);
                sum++;
                lastIndex = i;
            }          
        }
        if (lastIndex == -1)
            return str;
        return str.Remove(lastIndex);
    }
}