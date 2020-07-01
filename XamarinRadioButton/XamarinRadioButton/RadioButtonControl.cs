using System;
using Xamarin.Forms;

namespace XamarinRadioButton
{
    public class RadioButtonControl : StackLayout
    {
        private bool _isChecked = false;
        private BoxView _innerCircle = new BoxView();
        private Button _radioBtn = null;

        public static readonly BindableProperty CheckedProperty =
        BindableProperty.Create(nameof(Checked), typeof(bool), typeof(RadioButtonControl), false, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UidProperty =
        BindableProperty.Create(nameof(Uid), typeof(int), typeof(RadioButtonControl), 0, defaultBindingMode: BindingMode.TwoWay);

        public EventHandler CheckedChanged;
        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                SetValue(CheckedProperty, value);
                _isChecked = value;
                UpdateRadioBtnState();
            }
        }

        public int Uid
        {
            get
            {
                return (int)GetValue(UidProperty);
            }

            set
            {
                SetValue(UidProperty, value);
            }
        }

        public Color BorderColor
        {
            set
            {
                _radioBtn.BorderColor = value;
            }
        }

        public RadioButtonControl()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            CreateRadioBtn();
            RegisterEvents();
        }

        private void CreateRadioBtn()
        {
            _radioBtn = new Button()
            {
                Padding = 0,
                HeightRequest = 40,
                WidthRequest = 40,
                IsEnabled = false,
                Style = GetDefaultStyle()
            };

            this.Children.Add(_radioBtn);

            _innerCircle = new BoxView()
            {
                CornerRadius = 15,
                BackgroundColor = Color.Transparent,
                HeightRequest = 25,
                WidthRequest = 25,
                Parent = _radioBtn,
                Margin = new Thickness(-32.5, 0.6, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            this.Children.Add(_innerCircle);
        }

        private void RegisterEvents()
        {
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += RadioBtnTapHandler;
            _innerCircle.GestureRecognizers.Add(tap);
        }

        public void RadioBtnTapHandler(object sender, EventArgs e)
        {
            var innerCircle = sender as BoxView;

            if (!_isChecked)
            {
                _isChecked = true;
                _innerCircle.BackgroundColor = Color.ForestGreen;
            }
            else
            {
                _isChecked = false;
                _innerCircle.BackgroundColor = Color.Transparent;
            }
            CheckedChanged?.Invoke(this, e);
        }

        private void UpdateRadioBtnState()
        {
            if (_isChecked)
            {
                _innerCircle.BackgroundColor = Color.ForestGreen;
            }
            else
            {
                _innerCircle.BackgroundColor = Color.White;
            }
        }
        private Style GetDefaultStyle()
        {
            return new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.Transparent },
                    new Setter { Property = Button.BorderColorProperty,   Value = Color.LightGray },
                    new Setter { Property = Button.TextColorProperty,   Value = Color.ForestGreen },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 3 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 }
            }
            };

        }
    }
}