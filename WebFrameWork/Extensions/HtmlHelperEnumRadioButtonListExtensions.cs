/*
 * 这个有些麻烦，先不实现
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DaXia.WebFrameWork
{
    public static class HtmlHelperEnumRadioButtonListExtensions
    {
        public static MvcHtmlString RadioButtonList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectValue, string optionLabel = null, object htmlAttributes = null)
        {
            Type enumType = typeof(TEnum);
            Type underlyingType = Nullable.GetUnderlyingType(enumType);
            if (underlyingType != null)
            {
                enumType = underlyingType;
            }

            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
            
            foreach(var item in values)
            {
                bool selected = false;
                string itemName = Utility.GetEnumDescription<TEnum>(item);
                if(item.Equals(selectValue))
                {
                    selected = true;
                }
                //htmlHelper.RadioButton(name,(int)item,selected);
            }

            //foreach (var item in items)
            //{
            //    result += htmlHelper.RadioButton(name,);
            //}
            return MvcHtmlString.Empty;
        }        
    }
}
