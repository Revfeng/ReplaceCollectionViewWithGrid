using CollectionViewExample.Model;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectionViewExample.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomScrollView : ScrollView
    {
        public CustomScrollView()
        {
            InitializeComponent();
        }

        private List<Button> monkeyButtons;

        // This bindable Property is used to act like the CollectionView's ItemsSource
        public static readonly BindableProperty MonkeysProperty =
                 BindableProperty.Create("Monkeys", typeof(List<Monkey>), typeof(CustomScrollView), null, BindingMode.TwoWay);

        public List<Monkey> Monkeys
        {
            get { return (List<Monkey>)GetValue(MonkeysProperty); }
            set { SetValue(MonkeysProperty, value); }
        }

        // This bindable Property is used to act like the CollectionView's SelectionChangeCommand
        public static readonly BindableProperty SelectionCommandProperty =
                  BindableProperty.Create("SelectionCommand", typeof(ICommand), typeof(CustomScrollView), null, BindingMode.TwoWay);

        public ICommand SelectionCommand
        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            // the below code makes sure if SelectionCommand is null and Monkeys is null, it won't create the button which guarantees each button is bound to the right command
            if(propertyName == MonkeysProperty.PropertyName)
            {
                if (SelectionCommand != null)
                    InitMonkeyButtons();
            }
            else if(propertyName == SelectionCommandProperty.PropertyName)
            {
                if (Monkeys != null)
                    InitMonkeyButtons();
            }
        }

        private void InitMonkeyButtons()
        {
            // 4 is the cutomized column you want to put in a row, like Span in the Grid, you also could customize it to any number you want.
            //<CollectionView.ItemsLayout>
            // < GridItemsLayout Orientation = "Vertical" Span = "4" />
            //</ CollectionView.ItemsLayout >
            monkeyButtons = CreateMonkeyButtons(Monkeys, 4);
        }

        // Create a list of Monkey Type Buttons
        private List<Button> CreateMonkeyButtons(List<Monkey> monkeys, int col) // when create buttons, could also customize how many rows you would like in this method
        {
            List<Button> paymentTypeButtons = new List<Button>();

            InitGridRowDefinitions(monkeys.Count, col);

            if (monkeys != null)
            {
                for (int i = 0; i < monkeys.Count; i++)
                {
                    int curRow = i / col;
                    int curCol = i % col;
                    var button = CreateMonkeyButton(monkeys[i]);
                    Grid.SetRow(button, curRow);
                    Grid.SetColumn(button, curCol);
                    // Add each monkey button to the Grid View
                    MonkeyTypesGridView.Children.Add(button);
                    paymentTypeButtons.Add(button);
                }
            }

            return paymentTypeButtons;
        }

        private void InitGridRowDefinitions(int paymentTypesCount, int column)
        {
            int rowCount = paymentTypesCount % column > 0 ? paymentTypesCount / column + 1 : paymentTypesCount / column;

            for (int i = 0; i < rowCount; i++)
            {
                MonkeyTypesGridView.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }
        }

        // You could customized the button look and feel in the below function
        private Button CreateMonkeyButton(Monkey monkey)
        {
            var button = new Button();

            button.Text = monkey.Name;       
            button.Command = SelectionCommand;
            // add command parameter if you want to have something
            button.CommandParameter = monkey;                
            return button;
        }
    }
}