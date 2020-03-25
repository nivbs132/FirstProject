﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core
{
    public abstract class DriverUser
    {
        protected IWebDriver Driver { get; private set; }

        public DriverUser(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}