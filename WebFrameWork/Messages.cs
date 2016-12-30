using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaXia.WebFrameWork
{
    /// <summary>
    /// 辅助传输StatusMessage数据
    /// </summary>
    [Serializable]
    public sealed class StatusMessageData
    {
        private StatusMessageType messageType;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messageContent">消息内容</param>
        public StatusMessageData(StatusMessageType messageType, string messageContent)
        {
            this.messageType = messageType;
            this.messageContent = messageContent;
        }

        /// <summary>
        /// 提示消息类别
        /// </summary>
        public StatusMessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private string messageContent = string.Empty;
        /// <summary>
        /// 信息内容
        /// </summary>
        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }        
    }


    /// <summary>
    /// 提示消息类别
    /// </summary>
    public enum StatusMessageType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,

        /// <summary>
        /// 提示信息
        /// </summary>
        Hint = 0
    }

    /// <summary>
    /// 界面上需要展现的消息
    /// </summary>
    public sealed class UIMessage
    {
        public static string ShowDialogAndRedirct(string message, string redirectUrl)
        {
            //return string.Format("window.onload = function () {showDialog('{0}','{1}')};", message, redirectUrl);
            return "window.onload = function () {showDialog('" + message + "','" + redirectUrl + "')};";
        }

        public static string AlertDialog(string message)
        {
            return "window.onload = function () {AlertDialog('" + message + "')};";
        }
    }
}
