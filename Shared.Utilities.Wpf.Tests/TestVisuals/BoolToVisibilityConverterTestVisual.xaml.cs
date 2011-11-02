using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shared.Utilities.Wpf.Tests.TestVisuals
{
	/// <summary>
	/// Interaction logic for BoolToVisibilityConverterTestVisual.xaml
	/// </summary>
	public partial class BoolToVisibilityConverterTestVisual : UserControl
	{
		public BoolToVisibilityConverterTestVisual()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public void NegateCurrentVisbilities()
		{
			nonInvertingVisibilityControl.Visibility = NegateVisibility(nonInvertingVisibilityControl.Visibility);
			invertingVisibilityControl.Visibility = NegateVisibility(invertingVisibilityControl.Visibility);
		}

		private Visibility NegateVisibility(Visibility visible)
		{
			if (visible == Visibility.Visible)
			{
				return Visibility.Collapsed;
			}
			else
			{
				return Visibility.Visible;
			}
		}

	}
}
