using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Wpf.ExtensionMethods
{
	public static class DependencyObjectExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(DependencyObjectExtensions));

		#endregion

		/// <summary>
		/// Find a parent of the current element up the visual tree.
		/// </summary>
		public static T FindParentControl<T>(this DependencyObject root) where T : DependencyObject
		{
			_logger.DebugMethodCalled(false, root);

			#region Input Validation

			if (root == null)
			{
				return default(T);
			}

			#endregion

			DependencyObject currentObj = root;

			while (currentObj != null && 
					(
						((currentObj is T) == false)) ||
						(object.ReferenceEquals(root, currentObj) == true)
					)
			{
				currentObj = VisualTreeHelper.GetParent(currentObj);
			}

			if (currentObj != null &&
				currentObj is T)
			{
				return (T)currentObj;
			}

			return default(T);
		}

		/// <summary>
		///  Finds the first control nested within another control by type.
		/// </summary>
		/// <typeparam name="TControl">The type of control to look for.</typeparam>
		/// <param name="obj">The dependency object to search in.</param>
		/// <returns>The matching dependency object if found; null otherwise.</returns>
		public static TControl FindChildControl<TControl>(this DependencyObject obj) where TControl : DependencyObject
		{
			_logger.DebugMethodCalled(false, obj);

			return FindChildControl<TControl>(obj, 0);
		}

        /// <summary>
        ///  Finds a control nested within another control by type and index (e.g. "find the 3rd data grid on a page").
        /// </summary>
        /// <typeparam name="TControl">The type of control to look for.</typeparam>
        /// <param name="obj">The dependency object to search in.</param>
        /// <param name="index">The index of the control to find.</param>
        /// <returns>The matching dependency object if found; null otherwise.</returns>
        public static TControl FindChildControl<TControl>(this DependencyObject obj, int index) where TControl : DependencyObject
		{
			_logger.DebugMethodCalled(false, obj, index);

			#region Input Validation

			if (index < 0)
			{
				throw new ArgumentException("The index must be greater than 0", "index");
			}

			if (obj == null)
			{
				return default(TControl);
			}

			#endregion

			TControl control = null;

            int currentIndex = 0;
            int childCount = VisualTreeHelper.GetChildrenCount(obj);

            for(int i = 0; i < childCount; i++) {

                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                //Is this control the one we want?
                if(child is TControl) {                    

                    if(currentIndex == index) {
                        control = (TControl)child;
                        break;
                    }

                    currentIndex++;
                }

                //Evidently not, so let's look inside the child
                child = child.FindChildControl<TControl>(index - currentIndex);

                //Found one!
                if(child != null) {
                    control = (TControl)child;
                    break;
                }
            }

            return control;
        }
		
	}
}
