using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.MVVM.Models
{
    public static class WindowsHelper
    {
		public static void CloseWindow(object windowContext)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.DataContext == windowContext)
				{
					window.Close();
					return;
				}
			}
		}
		public static void MinimizeWindow(object windowContext)
		{
			foreach(Window window in Application.Current.Windows)
			{
				if (window.DataContext == windowContext)
				{
					window.WindowState = WindowState.Minimized;
					return;
				}
			}
		}
		public static bool IsWindowExists(Type window)
		{
			foreach (Window windowElement in Application.Current.Windows)
			{
				if (windowElement.DataContext is null)
					continue;

				if (windowElement.GetType() == window)
				{
					return true;
				}
			}

			return false;
		}
	}
}
