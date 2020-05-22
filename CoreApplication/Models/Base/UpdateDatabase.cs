using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Base
{
    public class ValueChangedArgs
    {
        public string Text { get; set; }
        public ValueChangedArgs(string s) => Text = s;      
    }

    public class UpdateDatabase
    {
        public delegate void UpdateDatabaseHandler(object sender, ValueChangedArgs e);

        public event UpdateDatabaseHandler Value_Changed;

        public ValueChangedArgs ValueChangedArgs
        {
            get => default;
            set
            {
            }
        }

        protected virtual void RaiseUpdateDatabase(string value)
        {
            Value_Changed?.Invoke(this, new ValueChangedArgs(value));
        }
    }

}
