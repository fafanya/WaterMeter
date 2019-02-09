using System;
using System.Collections.Generic;
using WaterMeter.Common.Models;
using WaterMeter.MobileAppService.Services;

namespace WaterMeter.Repositories
{
    public class ItemRepository : DBService, IItemRepository
    {
        public IEnumerable<TMeasurement> GetAll()
        {
            DBTableAttribute dbTableAttribute =
                (DBTableAttribute)Attribute.GetCustomAttribute(typeof(BMeasurement), typeof(DBTableAttribute));
            TStorage q = new TStorage
            {
                Table = dbTableAttribute.Table
            };
            var storageItems = LoadList(q);

            List<TMeasurement> measurements = new List<TMeasurement>();
            foreach(var si in storageItems)
            {
                TMeasurement m = new TMeasurement
                {
                    TMeasurementId = (int)(si.Fields[BMeasurement.KeyParm.Key]),
                    Text = (string)(si.Fields[BMeasurement.TextParm.Key]),
                    Description = (string)(si.Fields[BMeasurement.DescriptionParm.Key]),
                    PhotoClientPath = (string)(si.Fields[BMeasurement.PhotoClientPathParm.Key]),
                    PhotoServerPath = (string)(si.Fields[BMeasurement.PhotoServerPathParm.Key])
                };
                measurements.Add(m);
            }
            return measurements;
        }

        public void Add(TMeasurement item)
        {
            DBTableAttribute dbTableAttribute =
                (DBTableAttribute)Attribute.GetCustomAttribute(typeof(BMeasurement), typeof(DBTableAttribute));

            TStorage tStorage = new TStorage
            {
                Table = dbTableAttribute.Table,
                PKField = BMeasurement.KeyParm.Key,
                Fields = new Dictionary<string, object>()
                {
                    { BMeasurement.TextParm.Key, item.Text },
                    { BMeasurement.DescriptionParm.Key, item.Description },
                    { BMeasurement.PhotoClientPathParm.Key, item.PhotoClientPath },
                    { BMeasurement.PhotoServerPathParm.Key, item.PhotoServerPath },
                }
            };

            Insert(tStorage);
        }

        public void Update(TMeasurement item)
        {
            throw new NotImplementedException();
        }

        public TMeasurement Remove(int key)
        {
            throw new NotImplementedException();
        }

        public TMeasurement Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}