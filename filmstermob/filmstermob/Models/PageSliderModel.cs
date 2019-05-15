using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace filmstermob.Models
{
    class PageSliderModel : INotifyPropertyChanged
    {
        private bool _isPresented;

        public List<string> ListViewItems
        {
            get
            {
                return new List<string>(
                    new string[]
                    {
                        "Home",
                        "Calender",
                        "Mail",
                        "Messages"
                    });
            }
        }

        public bool IsPresented
        {
            get { return this._isPresented; }
            set
            {
                if (this._isPresented != value)
                {
                    this._isPresented = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public Command ToggleSlidoutViewCommand
        {
            get
            {
                return new Command(() => { this.IsPresented = !this.IsPresented; });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var ev = this.PropertyChanged;
            if (ev != null)
                ev(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
