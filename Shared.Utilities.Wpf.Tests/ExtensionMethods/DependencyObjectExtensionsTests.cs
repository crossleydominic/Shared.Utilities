using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Windows.Markup;
using Shared.Utilities.Wpf.Tests;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading;
using System.Security.Permissions;
using System.Reflection;
using System.Windows.Automation.Peers;
using System.Windows;
using Shared.Utilities.Wpf.Tests.TestVisuals;
using Shared.Utilities.Threading;

namespace Shared.Utilities.Wpf.ExtensionMethods.Tests
{
	[TestFixture]
	public class DependencyObjectExtensionsTests
	{
		#region FindParentControl Tests

		/// <summary>
		/// Tests what happens when trying to find the parent of a null control.
		/// </summary>
		[Test]
		public void FindParentControl_ControlNull()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = null;
				RenderUtility.RenderVisual(visual);

				StackPanel sp = visual.FindParentControl<StackPanel>();

				Assert.IsNull(sp);
			});
		}

		[Test]
		public void FindParentControl_ParentExists()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				StackPanel intermediateStackPanel = visual.IntermediatePanel;

				StackPanel sp = intermediateStackPanel.FindParentControl<StackPanel>();

				Assert.IsTrue(sp != null && sp.Name == "RootPanel");
			});
		}

		[Test]
		public void FindParentControl_ParentDoesNotExists()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				StackPanel intermediateStackPanel = visual.IntermediatePanel;

				TabControl g = intermediateStackPanel.FindParentControl<TabControl>();

				Assert.IsNull(g);
			});
		}

		#endregion

		#region FindChildControl Tests

		/// <summary>
		/// Tests attempting to find a child control when the root control is null
		/// </summary>
		[Test]
		public void FindChildControl_ControlNull()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = null;
				RenderUtility.RenderVisual(visual);

				TextBox tb = visual.FindChildControl<TextBox>();

				Assert.IsNull(tb);
			});
		}

		/// <summary>
		/// Tests attempting to find the first child control of a type
		/// </summary>
		[Test]
		public void FindChildControl_FindFirst()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				//Assume first element
				TextBox tb = visual.FindChildControl<TextBox>();
				Assert.IsTrue(tb != null && tb.Text == "TextBox 1");

				//Explicitly findfirst element
				tb = visual.FindChildControl<TextBox>(0);
				Assert.IsTrue(tb != null && tb.Text == "TextBox 1");
			});
		}

		/// <summary>
		/// Tests attempting to find a control that is not the first occurrence of that control
		/// </summary>
		[Test]
		public void FindChildControl_FindOtherThanFirst()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				TextBox tb = visual.FindChildControl<TextBox>(2);
				Assert.IsTrue(tb != null && tb.Text == "TextBox 3");
			});
		}

		/// <summary>
		/// Tests attempting to find a child control that doesn't exist in the tree
		/// </summary>
		[Test]
		public void FindChildControl_FindNonExistentControl()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				//Attempt to find first, implicit
				TabControl tc = visual.FindChildControl<TabControl>();
				Assert.IsNull(tc);

				//Attempt to find first, explicit
				tc = visual.FindChildControl<TabControl>(0);
				Assert.IsNull(tc);

				//Attempt to find other than first
				tc = visual.FindChildControl<TabControl>(2);
				Assert.IsNull(tc);

			});
		}

		/// <summary>
		/// Tests attempting to find a child control when the supplied index is less than zero
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void FindChildControl_IndexLessThanZero()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				TextBox tb = visual.FindChildControl<TextBox>(-1);
				Assert.IsTrue(tb != null && tb.Text == "TextBox 1");
			});
		}
		
		/// <summary>
		/// Tests attempting to find a child control when the index is greater than all occcurrences
		/// of the control to find.
		/// </summary>
		[Test]
		public void FindChildControl_IndexTooGreat()
		{
			ThreadRunner.RunInSTA(delegate
			{
				DependencyObjectExtensionsTestVisual visual = new DependencyObjectExtensionsTestVisual();
				RenderUtility.RenderVisual(visual);

				TextBox tb = visual.FindChildControl<TextBox>(4);
				Assert.IsNull(tb);
			});
		}

		#endregion

	}
}
