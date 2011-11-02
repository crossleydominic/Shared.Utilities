using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Shared.Utilities.Wpf.Tests.TestVisuals
{
	public static class RenderUtility
	{
		/// <summary>
		/// Renders the control tree for a single control
		/// </summary>
		public static void RenderVisual(UIElement visual)
		{
			List<UIElement> elements = new List<UIElement>();
			elements.Add(visual);
			RenderVisuals(elements);
		}

		/// <summary>
		/// Renders the control tree for the supplied list of visuals
		/// </summary>
		public static void RenderVisuals(IEnumerable<UIElement> visuals)
		{
			//Go through each control and force WPF to render it and
			//create it's control tree.
			foreach (UIElement element in visuals)
			{
				if (element != null)
				{
					element.Measure(new System.Windows.Size(800, 800));
					element.Arrange(new Rect(0, 0, 800, 800));
					element.UpdateLayout();
				}
			}
		}
	}

}
