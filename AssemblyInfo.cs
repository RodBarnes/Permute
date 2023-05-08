/*
 * Copyright (c) 2023 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;

namespace Common
{
    public class AssemblyInfo
    {
        // Constructors.
        public AssemblyInfo() : this(Assembly.GetExecutingAssembly()) { }

        public AssemblyInfo(Assembly assembly)
        {

            // Get values from the assembly.
            AssemblyTitleAttribute titleAttr = GetAssemblyAttribute<AssemblyTitleAttribute>(assembly);
            if (titleAttr != null) Title = titleAttr.Title;

            AssemblyDescriptionAttribute assemblyAttr = GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly);
            if (assemblyAttr != null) Description = assemblyAttr.Description;

            AssemblyCompanyAttribute companyAttr = GetAssemblyAttribute<AssemblyCompanyAttribute>(assembly);
            if (companyAttr != null) Company = companyAttr.Company;

            AssemblyProductAttribute productAttr = GetAssemblyAttribute<AssemblyProductAttribute>(assembly);
            if (productAttr != null) Product = productAttr.Product;

            AssemblyCopyrightAttribute copyrightAttr = GetAssemblyAttribute<AssemblyCopyrightAttribute>(assembly);
            if (copyrightAttr != null) Copyright = copyrightAttr.Copyright;

            AssemblyTrademarkAttribute trademarkAttr = GetAssemblyAttribute<AssemblyTrademarkAttribute>(assembly);
            if (trademarkAttr != null) Trademark = trademarkAttr.Trademark;
            AssemblyVersion = assembly.GetName().Version;

            AssemblyFileVersionAttribute fileVersionAttr = GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly);
            if (fileVersionAttr != null) FileVersion = fileVersionAttr.Version;

            GuidAttribute guidAttr = GetAssemblyAttribute<GuidAttribute>(assembly);
            if (guidAttr != null) Guid = guidAttr.Value;

            NeutralResourcesLanguageAttribute languageAttr = GetAssemblyAttribute<NeutralResourcesLanguageAttribute>(assembly);
            if (languageAttr != null) NeutralLanguage = languageAttr.CultureName;

            ComVisibleAttribute comAttr = GetAssemblyAttribute<ComVisibleAttribute>(assembly);
            if (comAttr != null) IsComVisible = comAttr.Value;
        }

        #region Properties

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Company { get; private set; }
        public string Product { get; private set; }
        public string Copyright { get; private set; }
        public string Trademark { get; private set; }
        public Version AssemblyVersion { get; set; }
        public string FileVersion { get; private set; }
        public string Guid { get; private set; }
        public string NeutralLanguage { get; private set; }
        public bool IsComVisible { get; private set; } = false;
        public int MajorVersion => AssemblyVersion.Major;
        public int MinorVersion => AssemblyVersion.Minor;
        public int BuildNumber => AssemblyVersion.Build;
        public int RevisionNumber => AssemblyVersion.Revision;
        public string AssemblyVersionString => $"v{MajorVersion}.{MinorVersion}.{BuildNumber}.{RevisionNumber}";

        #endregion

        #region Methods

        // Return a particular assembly attribute value.
        public static T GetAssemblyAttribute<T>(Assembly assembly)
            where T : Attribute
        {
            // Get attributes of this type.
            object[] attributes =
                assembly.GetCustomAttributes(typeof(T), true);

            // If we didn't get anything, return null.
            if ((attributes == null) || (attributes.Length == 0))
                return null;

            // Convert the first attribute value into
            // the desired type and return it.
            return (T)attributes[0];
        }

        #endregion
    }
}
