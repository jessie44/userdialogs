﻿using System;
using Splat;

namespace Acr.UserDialogs
{
    public class UserDialogsImpl : AbstractUserDialogs
    {
        public override IDisposable Alert(AlertConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable ActionSheet(ActionSheetConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable Confirm(ConfirmConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable DatePrompt(DatePromptConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable TimePrompt(TimePromptConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable Login(LoginConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable Prompt(PromptConfig config)
        {
            throw new NotImplementedException();
        }

        public override IDisposable PickerPrompt(PickerPromptConfig config)
        {
            throw new NotImplementedException();
        }

        public override void ShowImage(IBitmap image, string message, int timeoutMillis)
        {
            throw new NotImplementedException();
        }

        public override void ShowSuccess(string message, int timeoutMillis)
        {
            throw new NotImplementedException();
        }

        public override IDisposable Toast(ToastConfig config)
        {
            throw new NotImplementedException();
        }

        protected override IProgressDialog CreateDialogInstance(ProgressDialogConfig config)
        {
            throw new NotImplementedException();
        }

        public override void ShowError(string message, int timeoutMillis)
        {
            throw new NotImplementedException();
        }
    }
}