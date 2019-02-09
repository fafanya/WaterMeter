using System;
using System.Collections.Generic;
using System.Linq;

namespace WaterMeter.MobileAppService.Services
{
    public class TObject
    {
        public Dictionary<TField, object> Values { get; set; }

        public TObject()
        {
            Values = new Dictionary<TField, object>();
            var tFields = GetType().GetFields().Where(x => x.FieldType == typeof(TField));
            foreach (var tField in tFields)
            {
                TField field = tField.GetValue(this) as TField;
                if (field != null)
                {
                    Values.Add(field, null);
                }
            }
        }

        public Dictionary<string, object> GetKeyValuePairs()
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (var f in Values)
            {
                keyValuePairs.Add(f.Key.Key, f.Value);
            }
            return keyValuePairs;
        }

        public void SetKeyValuePairs(Dictionary<string, object> keyValuePairs)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                var f = Values.ElementAt(i);
                Values[f.Key] = keyValuePairs[f.Key.Key];
            }
        }

        public object GetKeyValue()
        {
            return Values.FirstOrDefault(x => x.Key.FieldType == TFieldType.PK).Value;
        }

        public void SetKeyValue(object value)
        {
            var pkField = Values.FirstOrDefault(x => x.Key.FieldType == TFieldType.PK);
            Values[pkField.Key] = value;
        }

        public string GetKeyField()
        {
            return Values.FirstOrDefault(x => x.Key.FieldType == TFieldType.PK).Key.Key;
        }
    }

    public class TField
    {
        public string Key { get; set; }
        public string Header { get; set; }
        public TFieldType FieldType { get; set; }

        public TField(string key, string header, TFieldType fieldType)
        {
            Key = key;
            Header = header;
            FieldType = fieldType;
        }
    }

    public enum TFieldType
    {
        PK = 1,
        Name = 2,
        None = 3
    }

    [AttributeUsage(AttributeTargets.All)]
    public class DBTableAttribute : Attribute
    {
        public string Table { get; private set; }

        public DBTableAttribute(string table)
        {
            Table = table;
        }
    }
}