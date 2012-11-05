﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bicikli_Admin.CommonClasses
{
    class DoubleModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDouble(valueResult.AttemptedValue, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("A következő mező nem valós számot tartalmaz: " + bindingContext.ModelMetadata.DisplayName);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion
    }

    class DecimalModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDecimal(valueResult.AttemptedValue, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("A következő mező nem valós számot tartalmaz: " + bindingContext.ModelMetadata.DisplayName);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion
    }

    public class CustomModelBinder : DefaultModelBinder
    {
        public CustomModelBinder()
            : base()
        {
        }

        public override object BindModel(ControllerContext controllerContext,
          ModelBindingContext bindingContext)
        {

            object result = null;

            // Don't do this here!
            // It might do bindingContext.ModelState.AddModelError
            // and there is no RemoveModelError!
            // 
            // result = base.BindModel(controllerContext, bindingContext);

            if (bindingContext.ModelType == typeof(double))
            {

                string modelName = bindingContext.ModelName;
                string attemptedValue = bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

                // Depending on cultureinfo the NumberDecimalSeparator can be "," or "."
                // Both "." and "," should be accepted, but aren't.
                string wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string alternateSeperator = (wantedSeperator == "," ? "." : ",");

                if (attemptedValue.IndexOf(wantedSeperator) == -1
                  && attemptedValue.IndexOf(alternateSeperator) != -1)
                {
                    attemptedValue = attemptedValue.Replace(alternateSeperator, wantedSeperator);
                }

                try
                {
                    result = double.Parse(attemptedValue, NumberStyles.Any);
                }
                catch (FormatException e)
                {
                    bindingContext.ModelState.AddModelError(modelName, e);
                }

            }
            else
            {
                result = base.BindModel(controllerContext, bindingContext);
            }

            return result;
        }
    }

}