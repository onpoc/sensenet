﻿using System;
using System.Collections.Generic;
using System.Linq;
using SenseNet.ContentRepository.Storage.Schema;

// ReSharper disable once CheckNamespace
namespace SenseNet.ContentRepository.Storage.DataModel
{
    public class DynamicPropertyData
    {
        private SchemaEditor _editor;

        public int VersionId { get; set; }
        public List<PropertyType> PropertyTypes { get; set; } = new List<PropertyType>();
        public IDictionary<PropertyType, object> DynamicProperties { get; set; }
        public IDictionary<PropertyType, object> ContentListProperties { get; set; }
        public IDictionary<PropertyType, BinaryDataValue> BinaryProperties { get; set; } = new Dictionary<PropertyType, BinaryDataValue>();
        public IDictionary<PropertyType, List<int>> ReferenceProperties { get; set; } = new Dictionary<PropertyType, List<int>>();
        public IDictionary<PropertyType, string> LongTextProperties { get; set; } = new Dictionary<PropertyType, string>();

        public PropertyType EnsurePropertyType(string name, DataType dataType)
        {
            if(_editor == null)
                _editor = new SchemaEditor();

            var existing = PropertyTypes.FirstOrDefault(x => x.Name == name);
            if (existing != null)
            {
                if (existing.DataType != dataType)
                    throw new ApplicationException($"DataType mismatch {existing.DataType} <-> {dataType}. PropertyType name: {name}, ");
                return existing;
            }
            var pt = _editor.CreatePropertyType(name, dataType);
            PropertyTypes.Add(pt);
            return pt;
        }
    }
}
