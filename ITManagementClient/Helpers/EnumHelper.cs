using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ITManagementClient.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T e)
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (Convert.ToInt32(e, CultureInfo.InvariantCulture) == val)
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }

                }
            }

            return String.Empty;
        }
    }
}
