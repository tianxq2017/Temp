using System;
using System.Collections.Generic;
using System.Text;

namespace join.pms.dal
{
    public class SQLFilter
    {
         ~SQLFilter() { }

        /// <summary>
        /// ��ȡ���˺�İ�ȫSQL�ַ�
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static string GetFilterSQL(string sqlParams)
        {
            string s = "";

            s = sqlParams.Replace("'", " ");
            s = s.Replace(";", " ");
            s = s.Replace("1=1", " ");
            s = s.Replace("|", " ");
            s = s.Replace("<", " ");
            s = s.Replace(">", " ");
            s = s.Replace(" ", "");

            return s;

        }

        /// <summary>
        /// ����Ƿ����SQL�Ƿ��ַ�
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static bool CheckSQL(string sqlParams)
        {
             int flag = 0;

             flag += sqlParams.IndexOf("'") + 1;
             flag += sqlParams.IndexOf(";") + 1;
             flag += sqlParams.IndexOf("1=1") + 1;
             flag += sqlParams.IndexOf("|") + 1;
             flag += sqlParams.IndexOf("<") + 1;
             flag += sqlParams.IndexOf(">") + 1;
             if (flag != 0)
             {
                 return false;
             }
             else 
             { 
                 return true; 
             }
        }
    }
}
