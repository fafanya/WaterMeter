using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterMeter.MobileAppService.Services
{
    [DBTable("meas")]
    public class BMeasurement : TObject
    {
        public static TField KeyParm = new TField("id", "ID", TFieldType.PK);
        public static TField TextParm = new TField("text", "Штрихкод", TFieldType.Name);
        public static TField DescriptionParm = new TField("description", "Дата", TFieldType.None);
        public static TField PhotoClientPathParm = new TField("client_path", "Точка учёта", TFieldType.None);
        public static TField PhotoServerPathParm = new TField("server_path", "Точка учёта", TFieldType.None);

        public object Key
        {
            get
            {
                return Values[KeyParm];
            }
            set
            {
                Values[KeyParm] = value;
            }
        }
        public string Text
        {
            get
            {
                return Values[TextParm] as string;
            }
            set
            {
                Values[TextParm] = value;
            }
        }
        public string Description
        {
            get
            {
                return Values[DescriptionParm] as string;
            }
            set
            {
                Values[DescriptionParm] = value;
            }
        }
        public string PhotoClientPath
        {
            get
            {
                return Values[PhotoClientPathParm] as string;
            }
            set
            {
                Values[PhotoClientPathParm] = value;
            }
        }
        public string PhotoServerPath
        {
            get
            {
                return Values[PhotoServerPathParm] as string;
            }
            set
            {
                Values[PhotoServerPathParm] = value;
            }
        }
    }
}