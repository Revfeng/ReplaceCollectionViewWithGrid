using CollectionViewExample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CollectionViewExample.ViewModels
{
    public class Page1ViewModel : INotifyPropertyChanged
    {
        public Page1ViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                monkeys.Add(new Monkey("Bamboo","Red",10));
                monkeys.Add(new Monkey("Haha", "Blue Monkey", 10));
                monkeys.Add(new Monkey("LOL", "Squirrel Monkey", 20));                     
            }

            Monkeys = monkeys;

            InitCommands();
        }

        private void InitCommands()
        {
            // This command is binding to the Custom SelectCommand
            //SelectMonkeyButtonCommand = new Command(async () => await SelectMonkey()); // this is for the none Command Parameter

            SelectMonkeyButtonCommand = new Command<Monkey>((m) => SelectMonkey(m)); // this is for with Command Parameter
        }

        //private async Task SelectMonkey()
        //{
        //    // do what ever you want when click the button
        //    await Application.Current.MainPage.DisplayAlert("monkey button", "This is a monkey", "Cancel");
        //}

        private void SelectMonkey(Monkey monkey)
        {
            // do what ever you want when click the button
            Debug.WriteLine(monkey.Name);
        }

        private List<Monkey> monkeys = new List<Monkey>();
        public List<Monkey> Monkeys
        {
            get { return monkeys; }
            set
            {
                if (monkeys == value) return;
                monkeys = value; 
                OnPropertyChanged(nameof(Monkeys));
            }
        }

        public ICommand SelectMonkeyButtonCommand { get; set; }      

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
