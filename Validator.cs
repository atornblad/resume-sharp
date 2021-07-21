using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResumeSharp.Schema;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using System;

namespace ResumeSharp
{
    public static class Validator
    {
        public static bool Validate(Resume resume, out IEnumerable<ValidationResult> results)
        {
            var context = new ValidationContext(resume);
            var checkedObjects = new HashSet<object>();
            var list = new List<ValidationResult>();
            results = list;
            return Validate(resume, checkedObjects, list);
        }

        private static bool Validate(object obj, ISet<object> checkedObjects, ICollection<ValidationResult> results, string prefix = "")
        {
            if (checkedObjects.Contains(obj)) return true;
            checkedObjects.Add(obj);
            if (obj.GetType() == typeof(string)) return true;

            bool returns = true;

            var properties = obj.GetType()
                                .GetProperties()
                                .Select(p => new {
                                    PropertyInfo = p,
                                    ValidationAttributes = p.GetCustomAttributes(typeof(ValidationAttribute), true).OfType<ValidationAttribute>()
                                })
                                .Select(piva => new {
                                    Name = piva.PropertyInfo.Name,
                                    Type = piva.PropertyInfo.PropertyType,
                                    PropertyInfo = piva.PropertyInfo,
                                    ValidationAttributes = piva.ValidationAttributes,
                                    Value = piva.PropertyInfo.GetValue(obj)
                                })
                                .Where(pivav => (null != pivav.Value));
            
            foreach (var pivav in properties)
            {
                Trace.WriteLine($"{pivav.Name}: {pivav.Type.FullName} = {pivav.Value.ToString()}");
                foreach (var validationAttribute in pivav.ValidationAttributes)
                {
                    if (pivav.Type != typeof(string) && pivav.Value is IEnumerable enumerable)
                    {
                        int index = 0;
                        foreach (var enumeratedItem in enumerable)
                        {
                            if (!validationAttribute.IsValid(enumeratedItem))
                            {
                                returns = false;
                                results.Add(new ValidationResult(string.Format(validationAttribute.ErrorMessage, $"{prefix}{pivav.Name}[{index}]"), new [] { $"{prefix}{pivav.Name}[{index}]"}));
                            }
                            ++index;
                        }
                    }
                    else if (!validationAttribute.IsValid(pivav.Value))
                    {
                        returns = false;
                        results.Add(new ValidationResult(string.Format(validationAttribute.ErrorMessage, prefix + pivav.Name), new [] { $"{prefix}{pivav.Name}"}));
                    }
                }
                if (!pivav.ValidationAttributes.Any() && pivav.Type != typeof(string) && !pivav.Type.IsValueType)
                {
                    if (pivav.Value is IEnumerable enumerable)
                    {
                        int index = 0;
                        foreach (var enumeratedItem in enumerable)
                        {
                            returns &= Validate(enumeratedItem, checkedObjects, results, $"{prefix}{pivav.Name}[{index}].");
                            ++index;
                        }
                    }
                    else
                    {
                        returns &= Validate(pivav.Value, checkedObjects, results, $"{prefix}{pivav.Name}.");
                    }
                }
            }
            return returns;
        }

        private static bool CheckValidationAttribute(ValidationAttribute validationAttribute, object value)
        {
            if (value.GetType() != typeof(string) && value is IEnumerable enumerable)
            {
                foreach (var enumeratedItem in enumerable)
                {
                    if (!validationAttribute.IsValid(enumeratedItem))
                    {
                        return false;
                    }
                }
                return true;
            }
            return validationAttribute.IsValid(value);
        }
    }
}