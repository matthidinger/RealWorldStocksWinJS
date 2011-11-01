using System.Windows;
using System.Windows.Controls;

namespace RealWorldStocks.Client.UI.Controls
{
    [TemplateVisualState(Name = "Positive", GroupName = "PriceStates")]
    [TemplateVisualState(Name = "Negative", GroupName = "PriceStates")]
    public class PriceChangeArrow : Control
    {
        public PriceChangeArrow()
        {
            DefaultStyleKey = typeof(PriceChangeArrow);
        }

        public override void OnApplyTemplate()
        {
            LayoutUpdated += PriceChangeArrow_LayoutUpdated;
            base.OnApplyTemplate();
        }

        void PriceChangeArrow_LayoutUpdated(object sender, System.EventArgs e)
        {
            ChangeVisualState(true);
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(decimal?), typeof(PriceChangeArrow), new PropertyMetadata(PriceChanged));


        private static void PriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PriceChangeArrow) d).ChangeVisualState(true);
        }

        public decimal? Price
        {
            get { return (decimal?)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }


        private string CurrentPriceVisualState
        {
            get
            {
                if (Price == null || Price >= 0)
                    return "Positive";
                
                return "Negative";
            }
        }

        private void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, CurrentPriceVisualState, useTransitions);
        }
    }
}