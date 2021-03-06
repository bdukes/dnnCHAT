﻿/*
' Copyright (c) 2013  Christoc.com Software Solutions
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
' and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
' SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
' ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
' OR OTHER DEALINGS IN THE SOFTWARE.
' 
*/

namespace Christoc.Modules.DnnChat
{
    using System;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DnnChatModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DnnChatModuleBase, IActionable
    {
        public string StartMessage = string.Empty;

        public string ChatNick
        {
            get
            {
                if (string.IsNullOrEmpty(UserInfo.DisplayName))
                {
                    //guest user... TODO bind nick here or something
                    return "phantom";
                }

                return UserInfo.DisplayName;
            }
        }

        override protected void OnInit(EventArgs e)
        {
            DotNetNuke.Framework.jQuery.RequestUIRegistration();
            ClientResourceManager.RegisterScript(Parent.Page, "/Resources/Shared/scripts/knockout.js");
            ClientResourceManager.RegisterScript(Parent.Page, "/desktopmodules/DnnChat/scripts/moment.min.js");
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                StartMessage = Settings.Contains("StartMessage") ? Settings["StartMessage"].ToString() : Localization.GetString("DefaultStartMessage", LocalResourceFile);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection();
                //we don't have any actions currently
                //{
                //    {
                //        GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                //        EditUrl(), false, SecurityAccessLevel.Edit, true, false
                //    }
                //};
                return actions;
            }
        }
    }
}