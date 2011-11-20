using System.Windows;

namespace RealWorldStocks.Client.UI
{
    public partial class ItemDetail
    {
        public ItemDetail()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(ItemDetail),
                                        new PropertyMetadata(default(string), (s, e) =>
                                        {
                                            ((ItemDetail)s).LabelTextBlock.Text = e.NewValue.ToString();
                                        }));


        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (string), typeof (ItemDetail),
                                        new PropertyMetadata(default(string), (s, e) =>
                                                                                  {
                                                                                      ((ItemDetail) s).ValueTextBlock.Text = e.NewValue.ToString();
                                                                                  }));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}