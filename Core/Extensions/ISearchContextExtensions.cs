﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Infrastructure.Extensions
{
    public static class ISearchContextExtensions
    {
        public static IWebElement FindElement(this ISearchContext searchContext, 
            string cssSelector, int seconds = 20)
        {
            if (seconds <= 0)
            {
                return searchContext.FindElement(By.CssSelector(cssSelector));
            }
            else
            {
                DefaultWait<ISearchContext> defaultWait = new DefaultWait<ISearchContext>(searchContext);
                defaultWait.Timeout = TimeSpan.FromSeconds(seconds);
                defaultWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException),
                    typeof(StaleElementReferenceException), typeof(NoSuchElementException));
                return defaultWait.Until(sc =>
                    {
                        var element = sc.FindElement(By.CssSelector(cssSelector));
                        if(element.Enabled)
                        {
                            return element;
                        }

                        return null;
                    });
            }
        }

        public static IEnumerable<IWebElement> FindElements(this ISearchContext searchContext,
            string cssSelector, int seconds = 20)
        {
            if (seconds <= 0)
            {
                return searchContext.FindElements(By.CssSelector(cssSelector));
            }
            else
            {
                DefaultWait<ISearchContext> defaultWait = new DefaultWait<ISearchContext>(searchContext);
                defaultWait.Timeout = TimeSpan.FromSeconds(seconds);
                defaultWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException),
                    typeof(StaleElementReferenceException));
                return defaultWait.Until<IEnumerable<IWebElement>>(sc =>
                    {
                        var elements = sc.FindElements(By.CssSelector(cssSelector));
                        if (elements.All(element => element.Enabled))
                        {
                            return elements;
                        }

                        return null;
                    });
            }
        }

        //public static T WaitUntilAttributeEquals<T>(this T componentBase, string attribute
        //    , string expectedValue, int seconds = 20)
        //     where T : ComponentBase
        //{
        //    DefaultWait<T> defaultWait = new DefaultWait<T>(componentBase);
        //    defaultWait.Timeout = TimeSpan.FromSeconds(seconds);
        //    defaultWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException),
        //        typeof(StaleElementReferenceException));

        //    return defaultWait.Until(component =>
        //    {
        //        if (component.GetValueByAttribute(attribute) == expectedValue)
        //        {
        //            return component;
        //        }

        //        return null;
        //    });
        //}
    }
}
