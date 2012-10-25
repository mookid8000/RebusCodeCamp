using System;
using System.Text;
using Rebus.Logging;

namespace DrugLord
{
    public class WindowLogger : ILog
    {
        readonly StringBuilder builder = new StringBuilder();

        public void Debug(string message, params object[] objs)
        {
            Write("DEBUG", message, objs);
        }

        public void Info(string message, params object[] objs)
        {
            Write("INFO", message, objs);
        }

        public void Warn(string message, params object[] objs)
        {
            Write("WARN", message, objs);
        }

        public void Error(Exception exception, string message, params object[] objs)
        {
            Write("ERROR", string.Format(message, objs) + ": " + exception);
        }

        public void Error(string message, params object[] objs)
        {
            Write("ERROR", message, objs);
        }

        void Write(string level, string message, params object[] objs)
        {
            builder.AppendLine(level + ": " + string.Format(message, objs));
        }

        public string GetText()
        {
            return builder.ToString();
        }

        public void Clear()
        {
            builder.Clear();
        }
    }
}