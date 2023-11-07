using System;

namespace Scriptingo.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    
    public class FastApiTableAttribute : Attribute
    {
        public FastApiTableAttribute(string ConnectionName,string DbType)
        {
            ConnectionName = ConnectionName;
            DbType = DbType;
        }
        public string ConnectionName { get; set; }

        public string DbType { get; set; }
    }
}
