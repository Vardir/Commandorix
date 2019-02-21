﻿using System;
using Core.Attributes;
using System.Reflection;

namespace Core.Commands
{
    public class Parameter
    {
        private readonly Command container;
        private readonly FieldInfo backingField;

        public bool IsFlag { get; }
        public bool IsOptional { get; }
        public bool IsSet { get; private set; }
        public object DefaultValue { get; }
        public string Id { get; }
        public string Description { get; }
        public IParameterValidation Validation { get; }

        private Parameter(string id, string description, bool isOptional, IParameterValidation validation)
        {
            Id = id;
            IsOptional = isOptional;
            Description = description;
            Validation = validation;
        }
        
        public Parameter(string id, string description, bool isFlag, 
                         Command container, FieldInfo backingField, IParameterValidation validation)
            : this(id, description, false, validation)
        {
            IsOptional = isFlag;
            IsFlag = isFlag;
            this.container = container;
            this.backingField = backingField;
        }
        public Parameter(string id, string description, object defaultValue, 
                         Command container, FieldInfo backingField, IParameterValidation validation)
            : this(id, description, true, validation)
        {
            IsOptional = true;
            this.container = container;
            if (validation != null)
            {
                string error = validation.Validate(defaultValue);
                if (error != null)
                    throw new ArgumentException(error, nameof(defaultValue));
            }
            DefaultValue = defaultValue;
            this.backingField = backingField;
        }

        public void Unset()
        {
            IsSet = false;
            if (IsFlag)
                backingField.SetValue(container, false);
            else if (IsOptional)
                backingField.SetValue(container, DefaultValue);
        }
        public void Set()
        {
            if (!IsOptional && !IsFlag)
                throw new InvalidOperationException("can not set parameter without default value");
            IsSet = true;
            if (IsFlag)
                backingField.SetValue(container, true);
            else
                backingField.SetValue(container, DefaultValue);
        }
        public void SetValue(object value)
        {
            if (IsFlag)
                throw new InvalidOperationException("can no set flag to a custom value");
            IsSet = true;
            backingField.SetValue(container, value);
        }

        public bool CanAssign(object value) => backingField.FieldType == value.GetType();
        public Type GetValueType() => backingField.FieldType;
    }
}