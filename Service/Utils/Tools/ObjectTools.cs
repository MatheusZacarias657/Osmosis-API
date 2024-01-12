using System.Reflection;

namespace Service.Utils.Tools
{
    public static class ObjectTools
    {
        public static void CopyProperties<T, U>(T copyObject, U copiedObject, bool isWrite)
        {
            PropertyInfo[] copyProperties = copyObject.GetType().GetProperties();
            PropertyInfo[] copiedProperties = copiedObject.GetType().GetProperties();
            U emptyObject = (U)Activator.CreateInstance(typeof(U));

            foreach (PropertyInfo copiedProp in copiedProperties)
            {
                if (copiedProp.Name.Equals("id") && isWrite)
                    continue;

                object copiedValue = copiedProp.GetValue(copiedObject);
                object emptyValue = copiedProp.GetValue(emptyObject);

                if (copiedValue == null || copiedValue.Equals(emptyValue))
                    continue;

                foreach (PropertyInfo copyProp in copyProperties)
                {
                    if (copyProp.Name.Equals("id") && isWrite)
                        continue;

                    if (copyProp.Name == copiedProp.Name)
                    {
                        copyProp.SetValue(copyObject, copiedProp.GetValue(copiedObject));
                        break;
                    }
                }
            }
        }
    }
}
