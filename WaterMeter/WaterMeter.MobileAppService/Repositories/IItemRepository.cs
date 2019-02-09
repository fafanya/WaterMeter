using System;
using System.Collections.Generic;
using WaterMeter.Common.Models;

namespace WaterMeter.Repositories
{
    public interface IItemRepository
    {
        void Add(TMeasurement item);
        void Update(TMeasurement item);
        TMeasurement Remove(int key);
        TMeasurement Get(int id);
        IEnumerable<TMeasurement> GetAll();
    }
}
