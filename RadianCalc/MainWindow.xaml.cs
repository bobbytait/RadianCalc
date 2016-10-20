using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace RadianCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double RADIANS_PER_CIRCLE = Math.PI * 2;

        public MainWindow() { InitializeComponent(); }

        #region Event handlers

        private void textRadians_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DiscardSpaceKeyPress(sender as TextBox, e);
        }

        private void textRadians_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void textRadians_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textDegrees.Text = "";
                textNormalized.Text = "";
                return;
            }

            if (textBox.Text == "-")
            {
                return;
            }

            double radians = Double.Parse(textBox.Text);
            if (!textDegrees.IsFocused)
            {
                textDegrees.Text = RadiansToDegrees(radians).ToString();
            }
            textNormalized.Text = GetNormalizedRadians(radians).ToString();
        }

        private void textDegrees_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DiscardSpaceKeyPress(sender as TextBox, e);
        }

        private void textDegrees_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void textDegrees_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textRadians.Text = "";
                textNormalized.Text = "";
                return;
            }

            if (textBox.Text == "-")
            {
                return;
            }

            double degrees = Double.Parse(textBox.Text);
            double radians = DegreesToRadians(degrees);
            if (!textRadians.IsFocused)
            {
                textRadians.Text = radians.ToString();
            }
            //textNormalized.Text = GetNormalizedRadians(radians).ToString();
        }

        private void DiscardSpaceKeyPress(TextBox textBox, KeyEventArgs e)
        {
            if (e.Key == Key.Space && textBox.IsFocused == true)
            {
                e.Handled = true;
            }
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "." || e.Text == "-"))
            {
                e.Handled = true;
            }
        }

        #endregion Event handlers

        #region Other helpful methods

        private double GetNormalizedRadians(double degrees)
        {
            return Wrap(degrees, RADIANS_PER_CIRCLE, 0);
        }

        #endregion Other helpful methods

        #region lordofduct code

        // Source: http://www.dreamincode.net/forums/topic/277514-normalize-angle-and-radians/

        private const double DEG_TO_RAD = 0.0174532925199433;
        private const double RAD_TO_DEG = 57.2957795130823;

        /// <summary>
        /// Wraps a value around some significant range.
        ///
        /// Similar to modulo, but works in a unary direction over any range (including negative values).
        ///
        /// ex:
        /// Wrap(8,6,2) == 4
        /// Wrap(4,2,0) == 0
        /// Wrap(4,2,-2) == -2
        /// </summary>
        /// <param name="value">value to wrap</param>
        /// <param name="max">max in range</param>
        /// <param name="min">min in range</param>
        /// <returns>A value wrapped around min to max</returns>
        /// <remarks></remarks>
        public static double Wrap(double value, double max, double min = 0)
        {
            value -= min;
            max -= min;
            if (max == 0) { return min; }

            value = value % max;
            value += min;
            while (value < min)
            {
                value += max;
            }

            return value;
        }

        /// <summary>
        /// convert radians to degrees
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double RadiansToDegrees(double angle)
        {
            return angle * RAD_TO_DEG;
        }

        /// <summary>
        /// convert degrees to radians
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double DegreesToRadians(double angle)
        {
            return angle * DEG_TO_RAD;
        }

        #endregion lordofduct code
    }
}
