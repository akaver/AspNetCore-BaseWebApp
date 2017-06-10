using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Rest.Helpers
{

    /// <summary>
    /// For stroring info about possible primary keys in entity
    /// </summary>
    public class EntityKeyInfo
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public int Order { get; set; }

        public EntityKeyInfo()
        {
        }

        public EntityKeyInfo(string propertyName, object value = null, int order = 0)
        {
            PropertyName = propertyName;
            Value = value;
            Order = order;
        }
    }
}
