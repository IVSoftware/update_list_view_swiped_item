using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;

namespace update_list_view_swiped_item.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageBinding();
            InitializeComponent();
            Record.ListView = listView;
        }
    }

    internal class MainPageBinding
    {
        public MainPageBinding()
        {
            Source.Add(new Record { Title = "Item " });
        }
        public ObservableCollection<Record> Source { get; } = new ObservableCollection<Record>();
    }

    internal class Record : INotifyPropertyChanged
    {
        internal static RadListView ListView { get; set; }
        public Record() 
        {
            ClickCommand = new Command<Record>(OnClick);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand ClickCommand { get; }
        private void OnClick(Record record) => onToggle(record);

        bool _toggled = false;
        private void onToggle(Record record)
        {
            _toggled = !_toggled;
            if(_toggled)
            {
                Title = "Toggled";
                BackgroundColor = Color.LightSalmon;
            }
            else
            {
                Title = "Normal";
                BackgroundColor = Color.Transparent;
            }
            // Comment/Uncomment this line:
            ListView.EndItemSwipe();
        }

        string _title = "Normal";
        public string Title
        {
            get => _title;
            set
            {
                if(_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
        Color _BackgroundColor = Color.AliceBlue;
        public Color BackgroundColor
        {
            get => _BackgroundColor;
            set
            {
                if(!BackgroundColor.Equals(value))
                {
                    _BackgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
