using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;

namespace YBWH.YJYL.Common
{
	/// <summary>
	/// 页面数据校验类
    /// Copyright (C)  ZYLM 2004-2012
	/// </summary>
	public class PageValidate
	{
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
		private static Regex RegNumber = new Regex("^[0-9]+$");
		private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
		private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
		private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
		private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

		public PageValidate()
		{
		}


		#region 数字字符串检查		
        public static bool IsPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
		/// <summary>
		/// 检查Request查询字符串的键值，是否是数字，最大长度限制
		/// </summary>
		/// <param name="req">Request</param>
		/// <param name="inputKey">Request的键值</param>
		/// <param name="maxLen">最大长度</param>
		/// <returns>返回Request查询字符串</returns>
		public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
		{
			string retVal = string.Empty;
			if(inputKey != null && inputKey != string.Empty)
			{
				retVal = req.QueryString[inputKey];
				if(null == retVal)
					retVal = req.Form[inputKey];
				if(null != retVal)
				{
					retVal = SqlText(retVal, maxLen);
					if(!IsNumber(retVal))
						retVal = string.Empty;
				}
			}
			if(retVal == null)
				retVal = string.Empty;
			return retVal;
		}		
		/// <summary>
		/// 是否数字字符串
		/// </summary>
		/// <param name="inputData">输入字符串</param>
		/// <returns></returns>
		public static bool IsNumber(string inputData)
		{
			Match m = RegNumber.Match(inputData);
			return m.Success;
		}

		/// <summary>
		/// 是否数字字符串 可带正负号
		/// </summary>
		/// <param name="inputData">输入字符串</param>
		/// <returns></returns>
		public static bool IsNumberSign(string inputData)
		{
			Match m = RegNumberSign.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// 是否是浮点数
		/// </summary>
		/// <param name="inputData">输入字符串</param>
		/// <returns></returns>
		public static bool IsDecimal(string inputData)
		{
			Match m = RegDecimal.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// 是否是浮点数 可带正负号
		/// </summary>
		/// <param name="inputData">输入字符串</param>
		/// <returns></returns>
		public static bool IsDecimalSign(string inputData)
		{
			Match m = RegDecimalSign.Match(inputData);
			return m.Success;
		}		

		#endregion

		#region 中文检测

		/// <summary>
		/// 检测是否有中文字符
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsHasCHZN(string inputData)
		{
			Match m = RegCHZN.Match(inputData);
			return m.Success;
		}	

		#endregion

		#region 邮件地址
		/// <summary>
		/// 是否是浮点数 可带正负号
		/// </summary>
		/// <param name="inputData">输入字符串</param>
		/// <returns></returns>
		public static bool IsEmail(string inputData)
		{
			Match m = RegEmail.Match(inputData);
			return m.Success;
		}		

		#endregion

        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region 其他

        /// <summary>
		/// 检查字符串最大长度，返回指定长度的串
		/// </summary>
		/// <param name="sqlInput">输入字符串</param>
		/// <param name="maxLength">最大长度</param>
		/// <returns></returns>			
		public static string SqlText(string sqlInput, int maxLength)
		{			
			if(sqlInput != null && sqlInput != string.Empty)
			{
				sqlInput = sqlInput.Trim();							
				if(sqlInput.Length > maxLength)//按最大长度截取字符串
					sqlInput = sqlInput.Substring(0, maxLength);
			}
			return sqlInput;
		}		
		/// <summary>
		/// 字符串编码
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string HtmlEncode(string inputData)
		{
			return HttpUtility.HtmlEncode(inputData);
		}
		/// <summary>
		/// 设置Label显示Encode的字符串
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="txtInput"></param>
		public static void SetLabel(Label lbl, string txtInput)
		{
			lbl.Text = HtmlEncode(txtInput);
		}
		public static void SetLabel(Label lbl, object inputObj)
		{
			SetLabel(lbl, inputObj.ToString());
		}		
		//字符串清理
		public static string InputText(string inputString, int maxLength) 
		{			
			StringBuilder retVal = new StringBuilder();

			// 检查是否为空
			if ((inputString != null) && (inputString != String.Empty)) 
			{
				inputString = inputString.Trim();
				
				//检查长度
				if (inputString.Length > maxLength)
					inputString = inputString.Substring(0, maxLength);
				
				//替换危险字符
				for (int i = 0; i < inputString.Length; i++) 
				{
					switch (inputString[i]) 
					{
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}				
				retVal.Replace("'", " ");// 替换单引号
			}
			return retVal.ToString();
			
		}
		/// <summary>
		/// 转换成 HTML code
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Encode(string str)
		{			
			str = str.Replace("&","&amp;");
			str = str.Replace("'","''");
			str = str.Replace("\"","&quot;");
			str = str.Replace(" ","&nbsp;");
			str = str.Replace("<","&lt;");
			str = str.Replace(">","&gt;");
			str = str.Replace("\n","<br>");
			return str;
		}
		/// <summary>
		///解析html成 普通文本
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Decode(string str)
		{			
			str = str.Replace("<br>","\n");
			str = str.Replace("&gt;",">");
			str = str.Replace("&lt;","<");
			str = str.Replace("&nbsp;"," ");
			str = str.Replace("&quot;","\"");
			return str;
		}

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//去除,
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("--", "");//去除--
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            sqlText = sqlText.Replace("%", "");//去除%
            sqlText = sqlText.Replace(" ", "");//去除空格
            return sqlText;
        }
		#endregion

        #region 是否由特定字符组成
        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                //Regex RegNumber = new Regex(string.Format("^([{0}]{{1}})+$", charInput,lenInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 替换单行文本框输入的非法字符
        /// </summary>
        /// <returns></returns>
        public static String ReplaceBadChar(string strSource)
        {
            strSource = strSource.Replace("<", "&lt;");
            strSource = strSource.Replace(">", "&gt;").ToString().Replace(">", "&gt;");
            strSource = strSource.Replace("'", "&#39;");
            strSource = strSource.Replace("\"", "&quot;");
            strSource = strSource.Trim();
            return strSource;
        }
        //判断日期格式 如:2008-1-20 或2008-01-20 而且包含了对不同年份2月的天数，闰年的控制等等：
        public static bool isDataTime(string datetime)
        {
            return Regex.IsMatch(datetime, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }
        /// <summary>
        /// 检测提交页面是否含有非法的SQL字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckBadForm()
        {
            if (HttpContext.Current.Request.Form.Count > 0)
            {
                for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
                {
                    if (CheckBadWord(HttpContext.Current.Request.Form[i]))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 清除编辑器中的非法字符串
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string RemoveHTMLForEditor(string strHTML)
        {
            string input = strHTML;
            Regex regex = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            //Regex regex2 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            //Regex regex5 = new Regex(@"<div[\s\S]+</div *>", RegexOptions.IgnoreCase);
            input = regex.Replace(input, "");
            //input = regex2.Replace(input, " _disibledevent=");
            input = regex3.Replace(input, "");
            input = regex4.Replace(input, "");
            //input = regex5.Replace(input, "");
            return input;
        }
        /// <summary>
        /// 过滤JS脚本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string WipeScript(string html)
        {
            Regex[] regex = new Regex[12];
            RegexOptions options;
            options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            regex[0] = new Regex(@"<marquee[\s\S]+</marquee *>", options);
            regex[1] = new Regex(@"<script[\s\S]+</script *>", options);
            regex[2] = new Regex(@"href *= *[\s\S]*script *:", options);
            regex[3] = new Regex(@"<iframe[\s\S]+</iframe *>", options);
            regex[4] = new Regex(@"<frameset[\s\S]+</frameset *>", options);
            regex[5] = new Regex(@"<input[\s\S]+</input *>", options);
            regex[6] = new Regex(@"<button[\s\S]+</button *>", options);
            regex[7] = new Regex(@"<select[\s\S]+</select *>", options);
            regex[8] = new Regex(@"<textarea[\s\S]+</textarea *>", options);
            regex[9] = new Regex(@"<form[\s\S]+</form *>", options);
            regex[10] = new Regex(@"<embed[\s\S]+</embed *>", options);
            regex[11] = new Regex(@"on[/s/S]*=", options);
            for (int i = 0; i < regex.Length - 1; i++)
            {
                foreach (Match match in regex[i].Matches(html))
                {
                    html = html.Replace(match.Groups[0].ToString(), "");
                }
            }
            return html;
        }


        //验证内容长度
        public static bool isContent(string strContent)
        {
            return Regex.IsMatch(strContent, @"^(\s|\S){2,5000}$");
        }

        /// <summary>
        /// 替换SQL关键字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Filter(string str)
        {
            string[] pattern = { "select", "insert", "delete", "from", "count\\(", "drop table", "update", "truncate", "asc\\(", "mid\\(", "char\\(", "xp_cmdshell", "exec   master", "netlocalgroup administrators", "net user", "or", "and" };
            for (int i = 0; i < pattern.Length; i++)
            {
                str = str.Replace(pattern[i].ToString(), "");
            }
            return str;
        }
        /// <summary>
        ///  非法SQL字符检测
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckBadWord(string str)
        {
            string pattern = @"select|insert|delete|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec   master|netlocalgroup administrators|net user|or|and";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
                return true;
            return false;
        }
        /// <summary>
        /// 过滤掉不合格的字符
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string Filtrate(string strSource)
        {
            strSource = strSource.Replace("'", "");
            strSource = strSource.Replace("\"", "");
            strSource = strSource.Replace("<", "");
            strSource = strSource.Replace(">", "");
            strSource = strSource.Replace("=", "");
            strSource = strSource.Replace("or", "");
            strSource = strSource.Replace("select", "");
            strSource = strSource.Trim();
            return strSource;

        }
        //图片上传后判断是否真的是图片
        public static bool UploadFileImg(string picPath)
        {
            StreamReader sr = new StreamReader(picPath, Encoding.Default);
            string strContent = sr.ReadToEnd();
            sr.Close();
            string str = "request|script|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=";
            foreach (string s in str.Split('|'))
                if (strContent.IndexOf(s) != -1)
                {
                    File.Delete(picPath);
                    return false;
                }
            return true;
        }


    }
}
