using System;
using System.Configuration;
using System.Reflection;

namespace Repository.FakeImpl
{
    public class PrivateKeySetter
    {
        public bool IsKeyPrivate(object entity)
        {
            // A bit of convention.
            //
            return IsKeyPrivate(entity, "Id");
        }

        public bool IsKeyPrivate(object entity, string keyName)
        {
            Type type = entity.GetType();
            PropertyInfo propertyInfo = type.GetProperty(keyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            MethodInfo methodInfo = propertyInfo.GetSetMethod();
            if (methodInfo != null && methodInfo.IsPublic)
            {
                return false;
            }
            return true;
        }

        public bool SetKey(object entity, object value)
        {
            // A bit of convention.
            //
            return SetKey(entity, "Id", value);
        }

        public bool SetKey(object entity, string keyName, object value)
        {
            // Get the map for this entity.
            //
            Type type = entity.GetType();
            PropertyInfo propertyInfo = type.GetProperty(keyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if(propertyInfo == null)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }

            Type idType = propertyInfo.PropertyType;
            if (idType != value.GetType())
            {
                throw new SettingsPropertyWrongTypeException();
            }

            MethodInfo methodInfo = propertyInfo.GetSetMethod();
            if (methodInfo != null && methodInfo.IsPublic)
            {
                propertyInfo.SetValue(entity, value, null);
            }
            else
            {
                type.InvokeMember(keyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, entity, new[] { value });
            }
            return true;
        }
    }
}