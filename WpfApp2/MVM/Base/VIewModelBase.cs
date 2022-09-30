using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Markup;
using System.Windows.Threading;

namespace App.ViewModels.Base
{
    public abstract class ViewModelBase: MarkupExtension, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            var handlers = PropertyChanged;
            if (handlers is null) return;

            var invoсation_list = handlers.GetInvocationList();


            foreach (var action in invoсation_list)
            {
                if (action.Target is DispatcherObject disp_object)
                {
                    disp_object.Dispatcher.Invoke(action, this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    action.DynamicInvoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);


            return true;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

    }

}
